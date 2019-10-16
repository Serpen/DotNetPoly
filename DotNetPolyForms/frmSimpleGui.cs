using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System;
using DotNetPoly;

namespace DotNetPolyForms
{
    partial class frmSimpleGui
    {
        private System.Threading.ManualResetEvent oSignalEvent = new System.Threading.ManualResetEvent(false);

        private DotNetPoly.GameBoard _gb;

        private DotNetPoly.GameBoard gb
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
                    _gb.GameOver -= gb_GameOver;
                    _gb.NextPlayer -= gb_NextPlayer;
                }

                _gb = value;
                if (_gb != null)
                {
                    _gb.PlayerRankChanged += PlayerRankChanged;
                    _gb.FieldOwnerChange += FieldOwnerChange;
                    _gb.GameOver += gb_GameOver;
                    _gb.NextPlayer += gb_NextPlayer;
                }
            }
        }

        private System.Threading.Thread thGame;

        private Size fieldsize;
        // Private cornersize As Size

        private PictureBox[] pBox;
        private Label[] fieldLabels;


        internal frmSimpleGui()
        {

            // Dieser Aufruf ist für den Designer erforderlich.
            InitializeComponent();

            // Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            try
            {
                txtGameBoardFile.Text = Environment.GetCommandLineArgs()[1];
            }
            catch 
            {
            }

            thGame = new System.Threading.Thread(GameThread);


            Form.CheckForIllegalCrossThreadCalls = false; // HACK: FixThreading Delegates
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            System.Collections.Generic.List<DotNetPoly.Player> players = new System.Collections.Generic.List<DotNetPoly.Player>();
            for (var i = 1; i <= 4; i++)
            {
                switch (TableLayoutPanel1.Controls.Find("lstType" + i, false)[0].Text)
                {
                    case "Human":
                        {
                            players.Add(new DotNetPoly.HumanPlayer(TableLayoutPanel1.Controls.Find("txtPlayer" + i, true)[0].Text, TableLayoutPanel1.Controls.Find("btnColor" + i, true)[0].BackColor));
                            break;
                        }

                    case "Bot":
                        {
                            players.Add(new DotNetPoly.BotPlayer(TableLayoutPanel1.Controls.Find("txtPlayer" + i, true)[0].Text, TableLayoutPanel1.Controls.Find("btnColor" + i, true)[0].BackColor));
                            break;
                        }

                    case "(none)":
                        {
                            break;
                        }
                }
            }

            gb = new GameBoard(this.txtGameBoardFile.Text, players.ToArray());

            generateMap();
            var loopTo = gb.Settings.DICE_COUNT - 1;
            for (var i = 0; i <= loopTo; i++)
                SplitContainer1.Panel2.Controls.Find($"txtDice{i}", true)[0].Visible = true;

            foreach (var p in gb.PlayerRank)
            {
                if (p.GetType() == typeof(HumanPlayer))
                    ((HumanPlayer)p).DelegateControl += DelegateControl;

                p.RollDice += RollDice;
                p.Bankrupt += Bankrupt;
                p.MoneyTransfered += MoneyTransfered;
                p.MoveOn += MoveOn;
                p.MoveOver += MoveOver;
                p.Jail += Jail;
                p.Chance += Chance;
                p.DoublesToMuch += DoublesToMuch;
                p.PayRent += PayRent;
                p.UpgradeHouse += UpgradeHouse;
            }
            gb.BANK.MoneyTransfered += MoneyTransfered;

            btnStartGame.Enabled = false;
            thGame.Start();
        }

        void GameThread()
        {
            gb.Initialize();

            foreach (var p in gb.PlayerRank)
            {
                TableLayoutPanel1.Controls.Find($"txtPlayer{p.Index + 1}", true)[0].Text = p.Name;
                TableLayoutPanel1.Controls.Find($"txtCash{p.Index + 1}", true)[0].Text = p.Cash.ToString();
                TableLayoutPanel1.Controls.Find($"btnColor{p.Index + 1}", true)[0].BackColor = p.Color;
                if (p.GetType() == typeof(HumanPlayer))
                    TableLayoutPanel1.Controls.Find($"lstType{p.Index + 1}", true)[0].Text = "Human";
                else
                    TableLayoutPanel1.Controls.Find($"lstType{p.Index + 1}", true)[0].Text = "Bot";

                pBox[p.Index].BackColor = p.Color;
                pBox[p.Index].Tag = p.Index;
            }

            while (!gb.IsGameOver)
                gb.NextPlayersTurn();
        }

        public void generateMap()
        {
            SplitContainer1.Panel1.Controls.Clear();

            fieldLabels = new Label[gb.FieldCount - 1 + 1];
            pBox = new PictureBox[gb.PlayerRank.Length - 1 + 1];

            int counterField = 0;

            if (gb.FieldCount % 2 != 0)
                throw new NotSupportedException("Gameboard has wrong field count");

            int fieldsPerSide = (int)(gb.FieldCount / (double)4 + 1);
            int shortestSide;
            if (this.SplitContainer1.Panel1.Width > this.SplitContainer1.Panel1.Height)
                shortestSide = this.SplitContainer1.Panel1.Height - 10;
            else
                shortestSide = this.SplitContainer1.Panel1.Width - 10;
            fieldsize = new Size((int)(shortestSide / (double)fieldsPerSide), (int)(shortestSide / (double)fieldsPerSide));
            var loopTo = fieldsPerSide - 1;
            for (var x = 0; x <= loopTo; x++)
            {
                Label l = new Label();
                l.AutoSize = false;
                l.BorderStyle = BorderStyle.FixedSingle;
                l.Size = fieldsize;
                l.TextAlign = ContentAlignment.MiddleCenter;

                l.Top = 0;
                l.Left = x * fieldsize.Width;
                l.Text = gb.GetField(x).ToString() + " " + x;

                SplitContainer1.Panel1.Controls.Add(l);

                fieldLabels[counterField] = l;
                counterField += 1;
            }

            var loopTo1 = fieldsPerSide - 1;
            for (var y = 1; y <= loopTo1; y++)
            {
                Label l = new Label();
                l.AutoSize = false;
                l.BorderStyle = BorderStyle.FixedSingle;
                l.Size = fieldsize;
                l.TextAlign = ContentAlignment.MiddleCenter;

                l.Top = y * fieldsize.Width;
                l.Left = fieldsize.Width * (fieldsPerSide - 1);
                l.Text = gb.GetField(y + fieldsPerSide - 1).ToString() + " " + y.ToString() + (fieldsPerSide - 1).ToString();

                SplitContainer1.Panel1.Controls.Add(l);

                fieldLabels[counterField] = l;
                counterField += 1;
            }

            for (var x = fieldsPerSide - 1; x >= 1; x += -1)
            {
                Label l = new Label();
                l.AutoSize = false;
                l.BorderStyle = BorderStyle.FixedSingle;
                l.Size = fieldsize;
                l.TextAlign = ContentAlignment.MiddleCenter;

                l.Top = fieldsize.Height * (fieldsPerSide - 1);
                l.Left = ((x - 1) * fieldsize.Width);
                l.Text = gb.GetField((fieldsPerSide - x) + fieldsPerSide * 2 - 2).ToString() + " " + (fieldsPerSide - x) + (fieldsPerSide * 2 - 2);

                SplitContainer1.Panel1.Controls.Add(l);

                fieldLabels[counterField] = l;
                counterField += 1;
            }

            for (var y = fieldsPerSide - 1; y >= 2; y += -1)
            {
                Label l = new Label();
                l.AutoSize = false;
                l.BorderStyle = BorderStyle.FixedSingle;
                l.Size = fieldsize;
                l.TextAlign = ContentAlignment.MiddleCenter;

                l.Top = ((y - 1) * fieldsize.Height);
                l.Left = 0;
                l.Text = gb.GetField(gb.FieldCount - y + 1).ToString() + " " + (gb.FieldCount - y + 1);

                SplitContainer1.Panel1.Controls.Add(l);

                fieldLabels[counterField] = l;
                counterField += 1;
            }

            int pcounter=0; // Player Index not initialized
            foreach (var p in gb.PlayerRank)
            {
                pBox[pcounter] = new PictureBox();
                pBox[pcounter].BorderStyle = BorderStyle.FixedSingle;
                pBox[pcounter].Size = new Size(10, 10);

                SplitContainer1.Panel1.Controls.Add(pBox[pcounter]);
                pcounter += 1;
            }

            // Try
            Image img = Image.FromFile(Environment.ExpandEnvironmentVariables(@"%OneDrive%\VS_Projects\DotNetPoly\PICS\Background.png"));
            Graphics g = this.SplitContainer1.Panel1.CreateGraphics();
            Rectangle r = new Rectangle(fieldsize.Width, fieldsize.Height, (fieldsPerSide - 2) * fieldsize.Width, (fieldsPerSide - 2) * fieldsize.Height);
            g.DrawImage(img, r);
        }

        void SetPlayerLocation(int pField, Player pPlayer)
        {
            int x, y;

            x = fieldLabels[pField].Location.X + 10 + pPlayer.Index * 11;
            y = fieldLabels[pField].Location.Y + 10;

            pBox[pPlayer.Index].Location = new Point(x, y);
            pBox[pPlayer.Index].BringToFront();
        }



        void DelegateControl(Player pPlayer, ref DelegateEventArgs e)
        {
            int counter = 1;

            foreach (Control btn in gbActions.Controls)
            {
                if (btn.GetType() == typeof(Button))
                    btn.Visible = false;
            }

            gbActions.Tag = e;
            foreach (var a in e.Actions)
            {
                Button btnAction = gbActions.Controls.Find("btnAction" + counter, false)[0] as Button;
                btnAction.Visible = true;
                btnAction.Tag = a;
                switch (a)
                {
                    case  eActionType.BuyHouse:
                        {
                            e.ChoosenAction = a;
                            btnAction.Text = string.Format("{0} - {1} ({2} {3})", counter, a.ToString(), e.Fields[0], ((DotNetPoly.Fields.HouseField)e.Fields[0]).Cost);
                            break;
                        }

                    case eActionType.PayRent:
                        {
                            btnAction.Text = string.Format("{0} - {1} {2} für ({3})", counter, a.ToString(), ((DotNetPoly.Fields.HouseField)e.Fields[0]).Rent, e.Fields[0]);
                            break;
                        }

                    case  eActionType.UpgradeHouse:
                        {
                            btnAction.Text = string.Format("{0} - {1} {2} für ({3})", counter, a.ToString(), e.Fields[0], ((DotNetPoly.Fields.HouseField)e.Fields[0]).UpgradeCost);
                            break;
                        }

                    case eActionType.CashFromBank:
                    case eActionType.CashFromPlayer:
                    case eActionType.CollectStack:
                        {
                            btnAction.Text = string.Format("{0} - {1} {2} von ({3})", counter, a.ToString(), e.Cash, e.Players[0]);
                            break;
                        }

                    case eActionType.CashToBank:
                    case eActionType.CashToPlayer:
                    case eActionType.PayToStack:
                        {
                            btnAction.Text = string.Format("{0} - {1} {2} an ({3})", counter, a.ToString(), e.Cash, e.Players[0]);
                            break;
                        }

                    default:
                        {
                            btnAction.Text = counter + " - " + a.ToString();
                            break;
                        }
                }

                counter += 1;
            }
        }

        public void Bankrupt(Player sender)
        {
            TableLayoutPanel1.Controls.Find("txtPlayer" + sender.Index + 1, false)[0].Enabled = false;
            TableLayoutPanel1.Controls.Find("txtCash" + sender.Index + 1, false)[0].Enabled = false;
            TableLayoutPanel1.Controls.Find("lstType" + sender.Index + 1, false)[0].Enabled = false;
            TableLayoutPanel1.Controls.Find("lstType" + sender.Index + 1, false)[0].Text = "Bankrott";
            MessageBox.Show(this, string.Format("Spieler '{0}' ist bankrott", sender), $"Bankrott Spieler {sender}", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void RollDice(Player sender, DiceRollResult pDiceRollResult)
        {
            var loopTo = pDiceRollResult.Dices.Length - 1;
            for (var i = 0; i <= loopTo; i++)
                Controls.Find("txtDice" + i, true)[0].Text = pDiceRollResult.Dices[i].ToString();
        }

        public void MoneyTransfered(Entity pFromPlayer, Entity pToPlayer, uint pAmount)
        {
            lblActionText.Text = string.Format("Spieler '{0}' gibt Spieler '{1}' '{2}'", pFromPlayer, pToPlayer, pAmount);
            if (pFromPlayer.GetType().IsSubclassOf(typeof(Player)))
                TableLayoutPanel1.Controls.Find("txtCash" + ((Player)pFromPlayer).Index + 1, false)[0].Text = pFromPlayer.Cash.ToString();
            if (pToPlayer.GetType().IsSubclassOf(typeof(Player)))
                TableLayoutPanel1.Controls.Find("txtCash" + ((Player)pToPlayer).Index + 1, false)[0].Text = pToPlayer.Cash.ToString();
        }

        public void MoveOn(Player pPlayer, DotNetPoly.Fields.Field pField)
        {
            SetPlayerLocation(pField.Index, pPlayer);
        }

        public void MoveOver(Player pPlayer, DotNetPoly.Fields.Field pField)
        {
            if (pField.GetType() == typeof(DotNetPoly.Fields.Startfield))
                MessageBox.Show(this, string.Format("Spieler '{0}' zieht über {1}", pPlayer, pField), $"Los Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PlayerRankChanged(Player[] pPlayerRank)
        {
            MessageBox.Show(this, string.Format("Spielerreihenfolge ist: {0}", string.Join(",", (object[])pPlayerRank)), "Spielerreihenfolge", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Jail(Player pPlayer, eActionType pPrisonEvent)
        {
            if (pPrisonEvent == eActionType.JumpToJail)
            {
                MessageBox.Show(this, string.Format("Spieler {0} geht in {1}", pPlayer, "Jail"), $"Event Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetPlayerLocation(pPlayer.Position, pPlayer);
            }
            else if (pPrisonEvent == eActionType.LoseTurn)
                MessageBox.Show(this, string.Format("Spieler {0} setzt eine Runde aus", pPlayer), $"Event Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void Chance(Player pPlayer, eActionType pEvent, object[] pObject)
        {
            MessageBox.Show(this, $"Spieler {pPlayer} löst Event {pEvent.ToString()} mit {string.Join(", ", pObject)} aus", $"Event Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void FieldOwnerChange(DotNetPoly.Fields.HouseField pField, Entity pOwner)
        {
            MessageBox.Show(this, $"Spieler {pOwner} erwirbt {pField}", $"Kaufen Spieler {pOwner}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            fieldLabels[pField.Index].BackColor = pOwner.Color;
        }

        void UpgradeHouse(Player pPlayer, DotNetPoly.Fields.HouseField pHouse)
        {
            MessageBox.Show(this, $"Spieler {pPlayer} baut das Haus {pHouse} auf Stufe {pHouse.UpgradeLevel + 1} aus", $"Ausbau Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void DoublesToMuch(Player pPlayer)
        {
            MessageBox.Show(this, $"Spieler {pPlayer} hat einen Pash zuviel und muss in {gb.JailField} ", $"Aussetzen Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        void PayRent(DotNetPoly.Fields.Field pfield, Entity pPlayer, int pCost)
        {
            MessageBox.Show(this, $"Spieler {pPlayer} zahlt {pCost} für  {pfield} ", $"Miete zahlen Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void gb_GameOver()
        {
            foreach (Control btn in gbActions.Controls)
            {
                if (btn.GetType() == typeof(Button))
                    btn.Visible = false;
            }
            lblActivePlayer.Text = gb.ActivePlayer.ToString();
            MessageBox.Show(this, $"Spieler {gb.ActivePlayer} hat gewonnen", $"Sieg Spieler {gb.ActivePlayer}", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void gb_NextPlayer(Player pPlayer)
        {
            this.lblActivePlayer.Text = pPlayer.ToString();
        }

        private void frmSimpleGui_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                thGame.Abort();
            }
            catch 
            {
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ColorDialog colorChooser = new System.Windows.Forms.ColorDialog();
            colorChooser.Color = ((Button)sender).BackColor;
            if (colorChooser.ShowDialog() == DialogResult.OK)
                ((Button)sender).BackColor = colorChooser.Color;
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            int obj;
            DelegateEventArgs e2 = (DelegateEventArgs)gbActions.Tag;
            e2.ChoosenAction = (eActionType)((Button)sender).Tag;
            switch (e2.ChoosenAction)
            {
                case eActionType.CreditForHouse:
                    {
                        if (int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Welches Haus soll beliehen werden?", "Kredit aufnehmen"), out obj)) {
                            e2.Fields = new DotNetPoly.Fields.Field[] { gb.GetField(obj) };
                            gb.ActivePlayer.SubmitChoosenAction(e2);
                        }
                        break;
                    }

                case eActionType.IncreaseFixedDeposite:
                    {
                        if (int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Wieviel festgeld?", "Festgeld"), out obj)) {
                            e2.Cash = (uint)obj;
                            gb.ActivePlayer.SubmitChoosenAction(e2);
                        }
                        break;
                    }

                default:
                    {
                        gb.ActivePlayer.SubmitChoosenAction(e2);
                        break;
                    }
            }
        }
    }
}
