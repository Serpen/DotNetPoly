using DotNetPoly.Fields;
using System.Linq;

namespace DotNetPoly
{
    public abstract class Player : Entity
    {
        protected System.Threading.ManualResetEvent WaitChoosenAction = new System.Threading.ManualResetEvent(false);

        protected DelegateEventArgs choosenEventArgs;

        public Player(string pName, System.Drawing.Color pColor) : base(pName, pColor) { }

        internal void Initialize(GameBoard pGameBoard, int pIndex)
        {
            GameBoard = pGameBoard;
            GameBoard.BANK.TransferMoney(this, pGameBoard.Settings.START_CAPITAL);
            Position = 0;
            IsPlayering = true;
            Index = (ushort)pIndex;

            if (pGameBoard.Settings.FixedDepositePercentage > 0)
                _Festgeld = new Entity($"Festfeld {Name}", System.Drawing.Color.White);

            NextAction.Enqueue(eActionType.WaitingForTurn);
            NextAction.Enqueue(eActionType.Move);
        }


        internal GameBoard GameBoard { get; private set; }

        public ushort Position { get; internal set; }

        public Field PositionField => GameBoard.GetField(Position);

        internal void RaiseEventHouseUpgrade(HouseField houseField)
        {
            UpgradeHouse?.Invoke(this, houseField);
        }
        internal void RaisePayRent(HouseField pStreetField)
        {
            PayRent?.Invoke(pStreetField, pStreetField.PayRent(this), pStreetField.Rent);
        }

        //Doubles Count since last reset
        internal int DoublesCount { get; set; }

        internal bool IsPlayering { get; private set; }

        public byte RentPercentMultiplier { get; internal set; } = 100;

        internal System.Collections.Generic.Queue<eActionType> NextAction { get; set; } = new System.Collections.Generic.Queue<eActionType>();

        public ushort Index { get; internal set; }

        protected internal void OnBankrupt()
        {
            IsPlayering = false;
            NextAction.Clear();

            // Transfer cash back to BANK (when manually raised)
            if (Cash > 0)
                TransferMoney(GameBoard.BANK, System.Convert.ToUInt32(Cash));

            // Give all Fields back to BANK
            foreach (var f in OwnFields.Values)
                f.ChangeOwner(GameBoard.BANK);

            Bankrupt?.Invoke(this);

            GameBoard.CheckGameOver();
        }

        internal DiceRollResult onRollDice()
        {
            DiceRollResult diceRoll = new DiceRollResult(GameBoard);

            RollDice?.Invoke(this, diceRoll);

            return diceRoll;
        }

        public abstract DiceRollResult DelegateDiceRoll();

        internal abstract void onDelegateControl(DelegateEventArgs e);

        public void SubmitChoosenAction(DelegateEventArgs pEventArgs)
        {
            choosenEventArgs = pEventArgs;

            WaitChoosenAction.Set();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns>BUGGY: true if redo last action</returns>
        protected bool invokeAction(DelegateEventArgs e)
        {
            GameBoard.Statistics.ActionChoosen[e.ChoosenAction] = System.Convert.ToInt32(GameBoard.Statistics.ActionChoosen[e.ChoosenAction]) + 1;

            switch (e.ChoosenAction)
            {
                case eActionType.Move:
                    {
                        OnMove();
                        break;
                    }

                case eActionType.BuyHouse:
                    {
                        ((HouseField)e.Fields[0]).Buy(this);
                        break;
                    }

                case eActionType.PayRent:
                    {
                        ((HouseField)e.Fields[0]).PayRent(this);
                        break;
                    }

                case eActionType.GiveUp:
                    {
                        OnBankrupt();
                        break;
                    }

                case eActionType.UpgradeHouse:
                    {
                        ((HouseField)e.Fields[0]).Upgrade();
                        break;
                    }

                case eActionType.LoseTurn:
                    {
                        RaiseJail(eActionType.LoseTurn);
                        break;
                    }

                case eActionType.GoToField:
                    {
                        MoveTo(e.Fields[0], false);
                        break;
                    }

                case eActionType.JumpToField:
                case eActionType.JumpToRelativField:
                    {
                        MoveTo(e.Fields[0], true);
                        break;
                    }

                case eActionType.JumpToJail:
                    {
                        GameBoard.JailField.GoToJail(this);
                        RaiseJail(eActionType.JumpToJail);
                        break;
                    }

                case eActionType.PayToStack:
                    {
                        TransferMoney(GameBoard.Wundertuete, e.Cash);
                        break;
                    }

                case eActionType.CollectStack:
                    {
                        GameBoard.Wundertuete.TransferMoney(this, e.Cash);
                        break;
                    }

                case eActionType.CashToBank:
                    {
                        TransferMoney(GameBoard.BANK, e.Cash);
                        break;
                    }

                case eActionType.CashFromBank:
                    {
                        GameBoard.BANK.TransferMoney(this, e.Cash);
                        break;
                    }

                case eActionType.CashToPlayer:
                    {
                        TransferMoney(e.Players[0], e.Cash);
                        break;
                    }

                case eActionType.CashFromPlayer:
                    {
                        e.Players[0].TransferMoney(this, e.Cash);
                        break;
                    }

                case eActionType.CashFromAll:
                    {
                        foreach (var p in GameBoard.PlayerRank)
                        {
                            if (p.IsPlayering)
                                p.TransferMoney(this, e.Cash);
                        }

                        break;
                    }

                case eActionType.PayPerHouse:
                    {
                        foreach (var house in OwnFields)
                            TransferMoney(GameBoard.BANK, e.Cash);
                        break;
                    }

                case eActionType.AddMove:
                    {
                        //BUGGY!
                        NextAction.Clear();
                        NextAction.Enqueue(eActionType.Move);
                        break;
                    }

                case eActionType.Card_UseFreeParking:
                    {
                        RaiseChance(eActionType.Card_UseFreeParking, new object[] { e.Fields[0] });
                        if (!CheatAlwaysRentFree)
                            GameBoard.FreeParkingOwner = GameBoard.BANK;
                        break;
                    }

                case eActionType.Card_GetFreeParking:
                    {
                        GameBoard.FreeParkingOwner = this;
                        break;
                    }

                case eActionType.Card_UseFreeJail:
                    {
                        RaiseChance(eActionType.Card_UseFreeJail, new object[] { GameBoard.JailField });
                        if (!CheatAlwaysJailFree)
                            GameBoard.FreeParkingOwner = GameBoard.BANK;
                        break;
                    }

                case eActionType.Card_GetFreeJail:
                    {
                        GameBoard.FreeJailOwner = this;
                        break;
                    }

                case eActionType.Pass:
                case eActionType.None:
                    {
                        break;
                    }

                case eActionType.CreditForHouse:
                    {
                        var h = (HouseField)e.Fields[0];
                        if (h.Owner.Equals(this))
                            h.TakeForCredit();

                        // Redo Last Action!
                        return true;
                    }

                case eActionType.CreditReleaseHouse:
                    {
                        var h = (HouseField)e.Fields[0];
                        if (h.Owner.Equals(this))
                            h.RemoveCredit();

                        // Redo Last Action!
                        return true;
                    }

                case eActionType.IncreaseFixedDeposite:
                    {
                        RaiseIncreaseFixedDeposite(e.Cash);
                        return true;
                    }

                case eActionType.TerminateFixedDeposite:
                    {
                        RaiseTerminateFixedDeposite();
                        return true;
                    }

                default:
                    {
                        throw new System.NotImplementedException(e.ChoosenAction.ToString());
                    }
            }
            return false;
        }


        internal void OnMove()
        {
            var diceRoll = onRollDice();

            if (diceRoll.IsDoubles)
                DoublesCount += 1;

            // Doubles Limit?
            if (DoublesCount >= GameBoard.Settings.MAX_DOUBLES)
            {
                NextAction.Clear();
                NextAction.Enqueue(eActionType.JumpToJail);
                RaiseDoublesLimit();
            }
            else
            {
                // Move over every field in path, an move on the last

                // TODO: Should call MoveTo with overload
                for (var i = 1; i < diceRoll.DiceSum; i++)
                {
                    Position = (ushort)((Position + 1) % (GameBoard.FieldCount));
                    GameBoard.GetField(Position).onMoveOver(this);
                }
                Position = (ushort)((Position + 1) % GameBoard.FieldCount);
                PositionField.onMoveOn(this);
                
                if (IsPlayering && (diceRoll.IsDoubles) && this.Position != GameBoard.JailField.Index)
                {
                    // Doubles Limit no reached, so add another move
                    NextAction.Enqueue(eActionType.Move);
                    // TODO: AddMove needs own event
                    RaiseChance(eActionType.AddMove, new object[] { diceRoll });
                }
            }
        }

        internal void MoveTo(Field pFields, bool pJump)
        {
            if (pJump)
            {
                Position = pFields.Index;
                PositionField.onMoveOn(this);
            }
            else
            {
                Position = (ushort)((Position + 1) % (GameBoard.FieldCount));
                while (!PositionField.Equals(pFields))
                {
                    PositionField.onMoveOver(this);
                    Position = (ushort)((Position + 1) % (GameBoard.FieldCount));
                }
                PositionField.onMoveOn(this);
            }
        }

        private Entity _Festgeld;

        public uint Festgeld
        {
            get
            {
                return (uint)_Festgeld.Cash;
            }
        }


        internal void RaiseMoveOn(Field pField)
        {
            MoveOn?.Invoke(this, pField);
        }

        internal void RaiseMoveOver(Field pField)
        {
            MoveOver?.Invoke(this, pField);
        }

        // TODO: Protected
        internal void RaiseChance(eActionType pEvent, object[] pObject)
        {
            Chance?.Invoke(this, pEvent, pObject);
        }

        protected void RaiseJail(eActionType pEvent)
        {
            Jail?.Invoke(this, pEvent);
        }

        protected void RaiseDoublesLimit()
        {
            DoublesToMuch?.Invoke(this);
        }

        private void RaiseTerminateFixedDeposite()
        {
            _Festgeld.TransferMoney(this, Festgeld);
            TerminateFixedDeposite?.Invoke(this, Festgeld);
        }

        private void RaiseIncreaseFixedDeposite(uint pCash)
        {
            TransferMoney(_Festgeld, pCash);
            IncreaseFixedDeposite?.Invoke(this, pCash);
        }

        public event BankruptEventHandler Bankrupt;

        public delegate void BankruptEventHandler(Player sender);

        public event RollDiceEventHandler RollDice;

        public delegate void RollDiceEventHandler(Player sender, DiceRollResult pDiceRollResult);

        public event MoveOnEventHandler MoveOn;

        public delegate void MoveOnEventHandler(Player pPlayer, Field pField);

        public event MoveOverEventHandler MoveOver;

        public delegate void MoveOverEventHandler(Player pPlayer, Field pField);

        public event PayRentEventHandler PayRent;

        public delegate void PayRentEventHandler(Field pfield, Entity pPlayer, int pCost);

        public event JailEventHandler Jail;

        public delegate void JailEventHandler(Player pPlayer, eActionType pPrisonEvent);

        public event ChanceEventHandler Chance;

        public delegate void ChanceEventHandler(Player pPlayer, eActionType pEvent, object[] pObject);

        public event DoublesToMuchEventHandler DoublesToMuch;

        public delegate void DoublesToMuchEventHandler(Player pPlayer);

        public event UpgradeHouseEventHandler UpgradeHouse;

        public delegate void UpgradeHouseEventHandler(Player pPlayer, HouseField pHouse);

        public event TerminateFixedDepositeEventHandler TerminateFixedDeposite;

        public delegate void TerminateFixedDepositeEventHandler(Player pPlayer, uint pCash);

        public event IncreaseFixedDepositeEventHandler IncreaseFixedDeposite;

        public delegate void IncreaseFixedDepositeEventHandler(Player pPlayer, uint pCash);

        public bool CheatAlwaysJailFree = false;
        public bool CheatAlwaysRentFree = false;
        public int CheatMoneyFactor = 1;
    }
}
