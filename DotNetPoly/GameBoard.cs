using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetPoly
{
    //Partial because of Statistics internal class
    public partial class GameBoard
    {
        public GameBoard(string pGameBoardFile, Player[] pPlayers)
        {
            System.Xml.XmlDocument xmlfile = new System.Xml.XmlDocument();
            
            eActionType contextAction;

            Version fileVersion = new Version("0.0");

            FreeParkingOwner = BANK;
            FreeJailOwner = BANK;

            xmlfile.Load(System.Environment.ExpandEnvironmentVariables(pGameBoardFile));

            if (!Version.TryParse(xmlfile.SelectSingleNode("monopoly").Attributes["version"]?.InnerText, out fileVersion))
                fileVersion = new Version("0.0");

            if (fileVersion.Minor < 6)
                throw new NotSupportedException("Gameboard Version to low");

            Settings = new Settings(xmlfile.SelectSingleNode("monopoly"));

            foreach (System.Xml.XmlNode xmlfield in xmlfile.SelectNodes("monopoly/fields/*"))
            {
                ushort contextCash;
                string contextName;
                contextName = xmlfield.Attributes.GetNamedItem("name")?.InnerText;

                ushort.TryParse(xmlfield.Attributes.GetNamedItem("cash")?.InnerText, out contextCash);

                switch (xmlfield.Name.ToLower())
                {
                    case "start":
                        {
                            FieldCol.Add(contextName, new Fields.Startfield(contextCash, contextName));
                            break;
                        }

                    case "house":
                        {
                            FieldCol.Add(contextName, new Fields.HouseField(xmlfield, Settings));
                            break;
                        }

                    case "chance":
                        {
                            var actionText = xmlfield.Attributes.GetNamedItem("action")?.InnerText.Split(',');
                            eActionType[] actions = new eActionType[actionText.Length - 1 + 1];
                            for (int i = 0; i < actionText.Length; i++)
                            {
                                if (!(Enum.TryParse(actionText[i], out actions[i])))
                                    System.Diagnostics.Debug.WriteLine($"field {contextName} has unknown action {xmlfield.Attributes.GetNamedItem("action")?.InnerText}");
                            }

                            FieldCol.Add(contextName, new Fields.ChanceField(contextName, actions, contextCash));
                            break;
                        }

                    case "station":
                        {
                            FieldCol.Add(contextName, new Fields.StationField(contextName, contextCash));
                            break;
                        }

                    case "jail":
                        {
                            if (!Enum.TryParse(xmlfield.Attributes.GetNamedItem("action")?.InnerText, out contextAction))
                                System.Diagnostics.Debug.WriteLine($"field {contextName} has unknown action {xmlfield.Attributes.GetNamedItem("action")?.InnerText}");
                            FieldCol.Add(contextName, new Fields.JailField(contextName, contextAction));
                            break;
                        }

                    default:
                        {
                            FieldCol.Add(contextName, new Fields.ChanceField(contextName, eActionType.None, 0));
                            throw new NotSupportedException($"Fieldtype {nameof(xmlfield.Name)}={xmlfield.Name} not supported");
                        }
                }
                
            }

            if (Settings.UseWundertuete)
                Wundertuete = new Entity("Wundertuete", System.Drawing.Color.White);

            PlayerRank = pPlayers;
            Statistics = new StatisticsClass(this);
        }

        public Entity BANK { get; } = new Entity("BANK", System.Drawing.Color.White);

        // Todo: nur wenn Setting gesetzt
        public Entity Wundertuete { get; }

        internal Entity FreeParkingOwner;
        internal Entity FreeJailOwner;

        private Fields.FieldCollection FieldCol { get; set; } = new Fields.FieldCollection();

        public int FieldCount => FieldCol.Count;

        public Settings Settings { get; }

        public Player[] PlayerRank { get; }

        public Entity[] PlayerFestgeld { get; private set; }

        public Fields.JailField JailField { get; private set; }

        internal List<Fields.HouseField>[] Groups { get; private set; }

        internal Fields.FieldCollection _Housefields { get; } = new Fields.FieldCollection();
        public Fields.HouseField HouseFields (int index)
        {
            return (Fields.HouseField)_Housefields.Values.ElementAt(index);
        }


        private int _activePlayer = -1;

        public Fields.Field GetField(int index)
        {

                return FieldCol.Values.ElementAt(index);
            
        }

        public Player ActivePlayer => PlayerRank[_activePlayer];

        public bool IsGameOver
        {
            get
            {
                int activePlayersCount = 0; // How many activePlayers
                int lastActivePlayer = -1; //ohoho
                for (var i = 0; i < PlayerRank.GetLength(0); i++)
                {
                    if (PlayerRank[i].IsPlayering)
                    {
                        activePlayersCount += 1;
                        lastActivePlayer = i;
                    }
                } // i
                if (activePlayersCount < 2)
                {
                    _activePlayer = lastActivePlayer;
                    GameOver?.Invoke();
                    return true;
                }
                else
                    return false;
            }
        }

        public bool CheckGameOver()
        {
            return IsGameOver;
        }

        public void Initialize()
        {
            InitFields();
            InitPlayers();

            if (Settings.FixedDepositePercentage > 0)
                PlayerFestgeld = new Entity[PlayerRank.Length - 1 + 1];
        }

        private void InitFields()
        {
            int housfieldcounter = 0;

            Dictionary<int, System.Collections.Generic.List<Fields.HouseField>> grps = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<Fields.HouseField>>();

            // Fields init (Index, ToCollections)
            for (ushort i = 0; i <= FieldCol.Count - 1; i++)
            {
                GetField(i).Initialize(this, i);

                if (GetField(i) is Fields.JailField jfield)
                    JailField = jfield;
                else if (GetField(i) is Fields.HouseField hfield)
                {
                    _Housefields.Add(hfield.ToString(), hfield);
                    housfieldcounter += 1;
                    if (!grps.ContainsKey(hfield._Street))
                        grps.Add(hfield._Street, new List<Fields.HouseField>());
                    grps[hfield._Street].Add(hfield);
                }
            }

            Groups = new List<Fields.HouseField>[grps.Count - 1 + 1];

            foreach (var itm in grps)
                Groups[itm.Key] = itm.Value;
        }

        private void InitPlayers()
        {
            System.Collections.SortedList playerRankSort = new System.Collections.SortedList();
            int playerSortKey;

            // Initialize without rank
            for (var i = 0; i < PlayerRank.Length; i++)
                PlayerRank[i].Initialize(this, i);

            // Give every Player On dial for rank
            foreach (var p in PlayerRank)
            {
                _activePlayer = p.Index;
                NextPlayer?.Invoke(ActivePlayer);
                DiceRollResult result = p.DelegateDiceRoll();
                playerSortKey = result.DiceSum * 10;
                while (playerRankSort.ContainsKey(-playerSortKey))
                    playerSortKey += 1;
                playerRankSort.Add(-playerSortKey, p); // negativ for sorting
            }


            playerRankSort.Values.CopyTo(PlayerRank, 0);

            // Initialize Part 2 with rank
            for (ushort i = 0; i < PlayerRank.Length; i++)
                PlayerRank[i].Index = i;

            _activePlayer = -1;

            RaisePlayerRankChanged();
        }

        public void NextPlayersTurn()
        {
            List<eActionType> playerActions = new List<eActionType>(); // Stores possible player actions

            // Loop until isPlaying player who waits for turn
            do
                _activePlayer = (_activePlayer + 1) % (PlayerRank.GetLength(0));
            while (!ActivePlayer.IsPlayering && ActivePlayer.NextAction.Peek() == eActionType.WaitingForTurn);

            if (_activePlayer == 0)
                this.Statistics.Lap += 1;

            NextPlayer?.Invoke(ActivePlayer);

            // Should only dequeue waiting!?
            ActivePlayer.NextAction.Dequeue();

            // Find possible actions till next turn or NIL
            do
            {
                playerActions.Clear();
                do
                    playerActions.Add(ActivePlayer.NextAction.Dequeue());
                while (!(ActivePlayer.NextAction.Count == 0 || ActivePlayer.NextAction.Peek() == eActionType.WaitingForTurn));
                ActivePlayer.onDelegateControl(new DelegateEventArgs(playerActions.ToArray()));
            }
            while (!(ActivePlayer.NextAction.Count == 0 || ActivePlayer.NextAction.Peek() == eActionType.WaitingForTurn));

            // Enqueue next default action, isnt't needed
            if (ActivePlayer.IsPlayering)
            {
                if (ActivePlayer.NextAction.Count == 0)
                {
                    ActivePlayer.NextAction.Enqueue(eActionType.WaitingForTurn);
                    ActivePlayer.NextAction.Enqueue(eActionType.Move);
                }

                // Should notify end turn
                if (Settings.END_TURN_EVENT)
                    ActivePlayer.onDelegateControl(new DelegateEventArgs(eActionType.EndTurn));
            }

            ActivePlayer.DoublesCount = 0;
        }

        public void RaiseChangeOwner(Fields.HouseField pField, Entity pPlayer)
        {
            FieldOwnerChange?.Invoke(pField, pPlayer);
        }

        public void RaisePlayerRankChanged()
        {
            PlayerRankChanged?.Invoke(PlayerRank);
        }

        public event GameOverEventHandler GameOver;

        public delegate void GameOverEventHandler();

        public event FieldOwnerChangeEventHandler FieldOwnerChange;

        public delegate void FieldOwnerChangeEventHandler(Fields.HouseField pField, Entity pOwner);

        public event PlayerRankChangedEventHandler PlayerRankChanged;

        public delegate void PlayerRankChangedEventHandler(Player[] pPlayerRank);

        public event NextPlayerEventHandler NextPlayer;

        public delegate void NextPlayerEventHandler(Player pPlayer);
    }
}
