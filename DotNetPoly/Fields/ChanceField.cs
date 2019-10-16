namespace DotNetPoly
{
    namespace Fields
    {
        public class ChanceField : Field
        {
            private readonly static System.Random ChanceRandomizer = new System.Random();
            private readonly eActionType _chance;
            private readonly ushort _chanceCash = 0;

            public ChanceField(string pName, eActionType pChanceType, ushort pCash) : base(pName)
            {
                _chance = pChanceType;
                _chanceCash = pCash;
            }

            internal override void onMoveOn(Player pPlayer)
            {
                base.onMoveOn(pPlayer);

                    eActionType contextChance = _chance;
                    ushort contextCash = _chanceCash;
                    Field contextField;
                    Entity contextPlayer;

                    if (_chanceCash == 0)
                        contextCash = System.Convert.ToUInt16(ChanceRandomizer.Next(1, GameBoard.Settings.DICE_COUNT * GameBoard.Settings.DICE_EYES));

                    if (_chance == eActionType.Random)
                        contextChance = GameBoard.Settings.Chances[ChanceRandomizer.Next(0, GameBoard.Settings.Chances.Length-1)];
                    
                    // TODO: define exactly who performs action, sub chance or sub Delegate

                    pPlayer.RaiseChance(contextChance, null);

                    switch (contextChance)
                    {
                        case eActionType.JumpToJail:
                            {
                                eActionType[] posActions;

                                if (GameBoard.FreeJailOwner.Equals(pPlayer))
                                {
                                    posActions = new eActionType[2];
                                    posActions[0] = contextChance;
                                    posActions[1] = eActionType.Card_UseFreeJail;
                                }
                                else
                                {
                                    posActions = new eActionType[1];
                                    posActions[0] = contextChance;
                                }

                                pPlayer.onDelegateControl(new DelegateEventArgs(posActions, new Entity[] { pPlayer }, new Field[] { GameBoard.JailField }, 0));
                                break;
                            }

                        case eActionType.GoToField:
                            {
                                do
                                    contextField = GameBoard.GetField(ChanceRandomizer.Next(0, GameBoard.FieldCount));
                                while (!(contextField is JailField));
                                pPlayer.RaiseChance(contextChance, new object[] { contextField });

                                // If contextField.Equals(GameBoard.JailField) Then Stop
                                pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.GoToField, contextField));
                                break;
                            }

                        case eActionType.JumpToField:
                            {
                                do
                                    contextField = GameBoard.GetField(ChanceRandomizer.Next(0, GameBoard.FieldCount));
                                while (!(contextField is JailField));

                                contextField = GameBoard.GetField(ChanceRandomizer.Next(0, GameBoard.FieldCount));

                                pPlayer.RaiseChance(contextChance, new object[] { contextField });

                                pPlayer.onDelegateControl(new DelegateEventArgs(contextChance, contextField));
                                break;
                            }

                        case eActionType.JumpToRelativField:
                            {
                                int relativJump = ChanceRandomizer.Next(-GameBoard.Settings.DICE_COUNT * GameBoard.Settings.DICE_EYES, GameBoard.Settings.DICE_COUNT * GameBoard.Settings.DICE_EYES);
                                relativJump = pPlayer.Position + relativJump;
                                if (relativJump < 0)
                                    relativJump = GameBoard.FieldCount + 1 - relativJump;
                                contextField = GameBoard.GetField(relativJump);

                                pPlayer.RaiseChance(contextChance, new object[] { contextField });
                                pPlayer.onDelegateControl(new DelegateEventArgs(contextChance, contextField));
                                break;
                            }

                        case eActionType.PayToStack:
                            {
                                pPlayer.RaiseChance(contextChance, new object[] { contextCash });
                                pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.PayToStack, GameBoard.Wundertuete, contextCash));
                                break;
                            }

                        case eActionType.CollectStack:
                            {
                                var wundertuete = System.Convert.ToUInt16(GameBoard.Wundertuete.Cash);
                                pPlayer.RaiseChance(contextChance, new object[] { wundertuete });

                                pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.CollectStack, GameBoard.Wundertuete, wundertuete));
                                break;
                            }

                        case eActionType.CashToBank:
                            {
                                pPlayer.RaiseChance(contextChance, new object[] { contextCash });
                                pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.CashToBank, GameBoard.BANK, contextCash));
                                break;
                            }

                        case eActionType.CashFromBank:
                            {
                                pPlayer.RaiseChance(contextChance, new object[] { contextCash });
                                pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.CashFromBank, GameBoard.BANK, contextCash));
                                break;
                            }

                        case eActionType.CashToPlayer:
                            {
                                do
                                    contextPlayer = GameBoard.PlayerRank[ChanceRandomizer.Next(0, GameBoard.PlayerRank.Length)];
                                while (!((Player)contextPlayer).IsPlayering && !pPlayer.Equals(contextPlayer)) // to Player
      ; // Todo: MoveTo Player/Gameboard

                                pPlayer.RaiseChance(contextChance, new object[] { contextCash });

                                pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.CashToPlayer, contextPlayer, contextCash));
                                break;
                            }

                        case eActionType.CashFromPlayer:
                            {
                                do
                                    contextPlayer = GameBoard.PlayerRank[ChanceRandomizer.Next(0, GameBoard.PlayerRank.Length)];
                                while (!((Player)contextPlayer).IsPlayering && !pPlayer.Equals(contextPlayer)) // to Player
      ; // Todo: MoveTo Player/Gameboard

                                pPlayer.RaiseChance(contextChance, new object[] { contextCash });

                                pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.CashFromPlayer, contextPlayer, contextCash));
                                break;
                            }

                        case eActionType.CashFromAll:
                            {
                                pPlayer.RaiseChance(contextChance, new object[] { contextCash });

                                pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.CashFromAll, contextCash));
                                break;
                            }

                        case eActionType.PayPerHouse:
                            {
                                pPlayer.RaiseChance(contextChance, new object[] { contextCash });

                                contextCash = System.Convert.ToUInt16(contextCash * pPlayer._ownfields.Count);
                                pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.CashToBank, GameBoard.BANK, contextCash));
                                break;
                            }

                        case eActionType.AddMove:
                        case eActionType.Move // TODO: move handled?
                 :
                            {
                                pPlayer.RaiseChance(contextChance, new object[] { });

                                pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.Move));
                                break;
                            }

                        case eActionType.None:
                        case eActionType.DiceOnly:
                        case eActionType.WaitingForTurn:
                        case eActionType.Pass:
                        case eActionType.LoseTurn:
                        case eActionType.GiveUp:
                        case eActionType.EndTurn:
                            {
                                pPlayer.RaiseChance(contextChance, new object[] { });

                                pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.Pass));
                                break;
                            }

                        case eActionType.UpgradeHouse:
                            {
                                int houseindex;

                                if (pPlayer.OwnFields.Count > 0)
                                {
                                    houseindex = ChanceRandomizer.Next(0, pPlayer.OwnFields.Count );
                                    pPlayer.RaiseChance(contextChance, new object[] { pPlayer.OwnFields[houseindex] });
                                    pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.UpgradeHouse, pPlayer.OwnFields[houseindex]));
                                }

                                break;
                            }

                        case eActionType.BuyHouse:
                            {
                                int houseindex;
                                int housecounter=0, fieldcounter=0;

                                if (GameBoard.Statistics.AvailableHouseFields > 0)
                                {
                                    houseindex = ChanceRandomizer.Next(0, GameBoard.Statistics.AvailableHouseFields - 1);
                                    do
                                    {
                                        if (GameBoard.HouseFields(fieldcounter).Owner.Equals(GameBoard.BANK))
                                        {
                                            if (housecounter == houseindex)
                                            {
                                                pPlayer.RaiseChance(contextChance, new object[] { GameBoard.HouseFields(fieldcounter) });
                                                // pPlayer.onDelegateControl(New DelegateEventArgs(eActionType.BuyHouse, GameBoard.HouseFields(fieldcounter)))
                                                pPlayer.onDelegateControl(new DelegateEventArgs(new eActionType[] { eActionType.BuyHouse, eActionType.Pass }, GameBoard.HouseFields(fieldcounter)));
                                            }

                                            housecounter += 1;
                                        }

                                        fieldcounter += 1;
                                    }
                                    while (!(housecounter > houseindex));
                                }

                                break;
                            }

                            //macht nur gemeinsam mit moveON sinn, wieso bezahlen wenn man nicht dfa ist
                        case eActionType.PayRent:
                            {
                                int houseindex;
                                int housecounter = 0;
                                int fieldcounter = 0;
                                eActionType[] posActions;

                                if (GameBoard.Statistics.SoldHouseFields > 0)
                                {
                                    houseindex = ChanceRandomizer.Next(0, GameBoard.Statistics.SoldHouseFields - 1);
                                    if (GameBoard.FreeParkingOwner.Equals(pPlayer))
                                    {
                                        posActions = new eActionType[2];
                                        posActions[0] = contextChance;
                                        posActions[1] = eActionType.Card_UseFreeParking;
                                    }
                                    else
                                    {
                                        posActions = new eActionType[1];
                                        posActions[0] = contextChance;
                                    }
                                    do
                                    {
                                        if (!GameBoard.HouseFields(fieldcounter).Owner.Equals(GameBoard.BANK))
                                        {
                                            if (housecounter == houseindex)
                                            {
                                                contextField = GameBoard.HouseFields(fieldcounter);
                                                pPlayer.RaiseChance(contextChance, new object[] { GameBoard.GetField(fieldcounter) });
                                                pPlayer.onDelegateControl(new DelegateEventArgs(posActions, new Player[] { pPlayer }, new Field[] { contextField }, ((HouseField)contextField).Rent));
                                            }

                                            housecounter += 1;
                                        }

                                        fieldcounter += 1;
                                    }
                                    while (!(housecounter > houseindex));
                                }

                                break;
                            }

                        case eActionType.Card_GetFreeParking:
                            {
                                pPlayer.RaiseChance(contextChance, new object[] { GameBoard.FreeParkingOwner });
                                pPlayer.onDelegateControl(new DelegateEventArgs(contextChance));
                                break;
                            }

                        case eActionType.Card_GetFreeJail:
                            {
                                pPlayer.RaiseChance(contextChance, new object[] { GameBoard.FreeJailOwner });
                                pPlayer.onDelegateControl(new DelegateEventArgs(contextChance));
                                break;
                            }

                        default:
                            {
                                pPlayer.RaiseChance(contextChance, new object[] { });
                                System.Diagnostics.Debug.Print("Spieler {0} auf Feld {1} bekommt Chance {2}", pPlayer, pPlayer.PositionField, contextChance);
                                break;
                            }
                    }

                    GameBoard.Statistics.ChanceOccured[contextChance] = System.Convert.ToInt32(GameBoard.Statistics.ChanceOccured[contextChance]) + 1;
                }
            }
        }
    }
}
