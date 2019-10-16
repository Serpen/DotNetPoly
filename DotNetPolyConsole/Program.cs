using System;
using DotNetPoly;
using DotNetPoly.Fields;

namespace DotNetPolyConsole
{
    static class Program
    {
        private static GameBoard _gb;

        private static GameBoard gb
        {

            get
            {
                return _gb;
            }


            set
            {
                if (_gb != null)
                {
                    _gb.PlayerRankChanged -= PlayerRankChanged;
                    _gb.FieldOwnerChange -= FieldOwnerChange;
                }

                _gb = value;
                if (_gb != null)
                {
                    _gb.PlayerRankChanged += PlayerRankChanged;
                    _gb.FieldOwnerChange += FieldOwnerChange;
                }
            }
        }

        public static void Main(string[] args)
        {
            string gamefile;

            if (args.Length == 0)
            {
                Console.Write("Spielbrettdatei: ");
                gamefile = Console.ReadLine();
                Console.WriteLine();
            }
            else
                gamefile = args[0];
            Console.ForegroundColor = ConsoleColor.White;
            Player[] players = new Player[] { new HumanPlayer("Marco", System.Drawing.Color.Red), new BotPlayer("Malte", System.Drawing.Color.Cyan) };


            gb = new GameBoard(gamefile, players);

            // Set Event Handlers for Players
            foreach (var p in gb.PlayerRank)
            {
                p.RollDice += RollDice;
                if (p.GetType() == typeof(HumanPlayer))
                    ((HumanPlayer)p).DelegateControl += DelegateControl;

                p.Bankrupt += Bankrupt;
                p.MoneyTransfered += MoneyTransfered;
                p.MoveOn += MoveOn;
                p.MoveOver += MoveOver;
                p.Jail += Prison;
                p.Chance += Chance;
                p.UpgradeHouse += UpgradeHouse;
            }
            gb.BANK.MoneyTransfered += MoneyTransfered;

            gb.Initialize();
            // // DiceRollOnly sollte mit in die Gameschleife um kein Sonderelement zu sein

            while (!gb.IsGameOver)
                gb.NextPlayersTurn();

            Console.WriteLine("Spiel ist vorbei, gewonnen hat: '{0}'", gb.ActivePlayer);

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        private static void WriteLineInPlayerColor(string pLine, Entity pPlayer, params object[] format)
        {
            var preColor = Console.ForegroundColor;
            Console.ForegroundColor = ColorConverter.ClosestConsoleColor(pPlayer.Color);
            if (Console.BackgroundColor == Console.ForegroundColor)
                Console.ForegroundColor = preColor;

            Console.WriteLine(pLine, format);
            Console.ForegroundColor = preColor;
        }

        public static void DelegateControl(Player pPlayer, ref DelegateEventArgs e)
        {
            int counter = 1;
            string ergStr;

            System.Collections.Generic.List<DotNetPoly.Fields.Field> choosenFields = new System.Collections.Generic.List<DotNetPoly.Fields.Field>(2);

            WriteLineInPlayerColor("Spieler {0} auf Feld {1} ist am Zug und hat folgende Möglichkeiten:", pPlayer, pPlayer, pPlayer.PositionField);

            // Todo: Durch For Schleife ersetzen
            foreach (var a in e.Actions)
            {
                switch (a)
                {
                    case eActionType.BuyHouse:
                        {
                            WriteLineInPlayerColor("{0} - {1} ({2} {3})", pPlayer, counter, a.ToString(), e.Fields[0], ((DotNetPoly.Fields.HouseField)e.Fields[0]).Cost);
                            break;
                        }

                    case eActionType.PayRent:
                        {
                            WriteLineInPlayerColor("{0} - {1} {2} für ({3})", pPlayer, counter, a.ToString(), ((HouseField)e.Fields[0]).Rent, e.Fields[0]);
                            break;
                        }

                    case eActionType.UpgradeHouse:
                        {
                            WriteLineInPlayerColor("{0} - {1} {2} für {3}", pPlayer, counter, a.ToString(), e.Fields[0], ((DotNetPoly.Fields.HouseField)e.Fields[0]).UpgradeCost);
                            break;
                        }

                    case eActionType.CashFromBank:
                    case eActionType.CashFromPlayer:
                    case eActionType.CollectStack:
                        {
                            WriteLineInPlayerColor("{0} - {1} {2} von ({3})", pPlayer, counter, a.ToString(), e.Cash, e.Players[0]);
                            break;
                        }

                    case eActionType.CashToBank:
                    case eActionType.CashToPlayer:
                    case eActionType.PayToStack:
                        {
                            WriteLineInPlayerColor("{0} - {1} {2} an ({3})", pPlayer, counter, a.ToString(), e.Cash, e.Players[0]);
                            break;
                        }

                    default:
                        {
                            WriteLineInPlayerColor(counter + " - " + a.ToString(), pPlayer);
                            break;
                        }
                }

                counter += 1;
            }

            ergStr = Console.ReadLine();

            try
            {
                e.ChoosenAction = e.Actions[System.Convert.ToInt32(ergStr) - 1];
            }
            catch 
            {
                e.ChoosenAction = e.Actions[0];
            }

            if (e.ChoosenAction == eActionType.CreditForHouse | e.ChoosenAction == eActionType.CreditReleaseHouse)
            {
                Console.WriteLine("Choose House for Credit:");
                int hcounter = 1;
                int choosenHouse;

                foreach (var h in pPlayer.OwnFields)
                {
                    if (e.ChoosenAction == eActionType.CreditForHouse && !h.Value.HasCredit)
                        Console.WriteLine($"{hcounter} - {h.Value.Name} ({h.Value.Cost})");
                    else if (e.ChoosenAction == eActionType.CreditReleaseHouse && h.Value.HasCredit)
                        Console.WriteLine($"{hcounter} - {h.Value.Name} ({h.Value.Cost})");
                    hcounter += 1;
                }
                choosenHouse = System.Convert.ToInt32(Console.ReadLine());
                e.Fields = new DotNetPoly.Fields.Field[] { pPlayer.OwnFields[choosenHouse - 1] };
            }
            else if (e.ChoosenAction == eActionType.IncreaseFixedDeposite)
            {
                Console.Write("Wieviel Festgeld hinzufügen: ");
                e.Cash = System.Convert.ToUInt32(Console.ReadLine());
            }


            pPlayer.SubmitChoosenAction(e);
        }

        public static void Bankrupt(Player sender)
        {
            var pre = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            WriteLineInPlayerColor("Spieler '{0}' ist bankrott", sender, sender);
            Console.BackgroundColor = pre;
        }

        public static void RollDice(Player sender, DiceRollResult pDiceRollResult)
        {
            WriteLineInPlayerColor("Spieler '{0}' würfelt {1}", sender, sender, pDiceRollResult);
        }

        public static void MoneyTransfered(Entity pFromPlayer, Entity pToPlayer, uint pAmount)
        {
            var precolor = Console.ForegroundColor;
            Console.ForegroundColor = ColorConverter.ClosestConsoleColor(pFromPlayer.Color);
            Console.Write("Spieler '{0}' gibt Spieler ", pFromPlayer);
            Console.ForegroundColor = ColorConverter.ClosestConsoleColor(pToPlayer.Color);
            Console.Write("'{1}' ", pFromPlayer, pToPlayer, pAmount);
            Console.ForegroundColor = ColorConverter.ClosestConsoleColor(pFromPlayer.Color);
            Console.WriteLine(pAmount);
            Console.ForegroundColor = precolor;
        }

        public static void MoveOn(Player pPlayer, Field pField)
        {
            var precolor = Console.ForegroundColor;
            Console.ForegroundColor = ColorConverter.ClosestConsoleColor(pPlayer.Color);
            Console.Write("Spieler '{0}' zieht auf ", pPlayer, pPlayer, pField);
            Console.ForegroundColor = ColorConverter.ClosestConsoleColor(pField.Owner.Color);
            Console.WriteLine(pField);
            Console.ForegroundColor = precolor;
        }

        public static void MoveOver(Player pPlayer, Field pField)
        {
            if (pField.GetType() == typeof(Startfield))
                WriteLineInPlayerColor("Spieler '{0}' zieht über {1}", pPlayer, pPlayer, pField);
        }

        private static void PlayerRankChanged(Player[] pPlayerRank)
        {
            Console.WriteLine("Spielerreihenfolge ist: {0}", string.Join(", ", (object[])pPlayerRank));
        }

        private static void Prison(Player pPlayer, eActionType pPrisonEvent)
        {
            if (pPrisonEvent == eActionType.JumpToJail)
                WriteLineInPlayerColor("Spieler {0} geht in {1}", pPlayer, pPlayer, "Jail");
            else if (pPrisonEvent == eActionType.LoseTurn)
                WriteLineInPlayerColor("Spieler {0} setzt eine Runde aus", pPlayer, pPlayer);
        }

        public static void Chance(Player pPlayer, eActionType pChance, object[] pObject)
        {
            WriteLineInPlayerColor("Spieler {0} löst Event {1} mit {2} aus", pPlayer, pPlayer, pChance.ToString(), string.Join(", ", pObject));
        }

        public static void UpgradeHouse(Player pPlayer, HouseField pHouse)
        {
            WriteLineInPlayerColor("Spieler {0} baut {1} aus", pPlayer, pPlayer, pHouse);
        }

        public static void AuctionBid(Player pPlayer, ref uint pCash, DotNetPoly.Fields.HouseField pHouse)
        {
            WriteLineInPlayerColor("Spieler {0} kann auf Haus {1} mit minimal {2} bieten", pPlayer, pPlayer, pHouse, pCash);
            pCash = System.Convert.ToUInt32(Console.ReadLine());
        }

        private static void FieldOwnerChange(HouseField pField, Entity pOwner)
        {
            WriteLineInPlayerColor("Spieler {0} kauft {1}", pOwner, pOwner, pField);
        }
    }
}
