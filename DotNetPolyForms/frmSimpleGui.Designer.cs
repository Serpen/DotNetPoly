using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System;
using System.Runtime.CompilerServices;

namespace DotNetPolyForms
{
    partial class frmSimpleGui : System.Windows.Forms.Form
    {

        // Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                    components.Dispose();
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Wird vom Windows Form-Designer benötigt.
        private System.ComponentModel.IContainer components;

        // Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        // Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
        // Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblActionText = new System.Windows.Forms.Label();
            this.txtDice2 = new System.Windows.Forms.TextBox();
            this.txtDice1 = new System.Windows.Forms.TextBox();
            this.txtDice0 = new System.Windows.Forms.TextBox();
            this.gbGame = new System.Windows.Forms.GroupBox();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.txtGameBoardFile = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.gbActions = new System.Windows.Forms.GroupBox();
            this.lblActivePlayer = new System.Windows.Forms.Label();
            this.btnAction5 = new System.Windows.Forms.Button();
            this.btnAction4 = new System.Windows.Forms.Button();
            this.btnAction3 = new System.Windows.Forms.Button();
            this.btnAction2 = new System.Windows.Forms.Button();
            this.btnAction1 = new System.Windows.Forms.Button();
            this.lblActivePlayerDesc = new System.Windows.Forms.Label();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnColor4 = new System.Windows.Forms.Button();
            this.btnColor3 = new System.Windows.Forms.Button();
            this.btnColor2 = new System.Windows.Forms.Button();
            this.txtPlayer1 = new System.Windows.Forms.TextBox();
            this.txtCash1 = new System.Windows.Forms.TextBox();
            this.txtPlayer2 = new System.Windows.Forms.TextBox();
            this.txtCash2 = new System.Windows.Forms.TextBox();
            this.txtPlayer3 = new System.Windows.Forms.TextBox();
            this.txtCash3 = new System.Windows.Forms.TextBox();
            this.txtPlayer4 = new System.Windows.Forms.TextBox();
            this.txtCash4 = new System.Windows.Forms.TextBox();
            this.lstType1 = new System.Windows.Forms.ComboBox();
            this.lstType2 = new System.Windows.Forms.ComboBox();
            this.lstType3 = new System.Windows.Forms.ComboBox();
            this.lstType4 = new System.Windows.Forms.ComboBox();
            this.btnColor1 = new System.Windows.Forms.Button();
            this.btnAction6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.gbGame.SuspendLayout();
            this.gbActions.SuspendLayout();
            this.TableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.lblActionText);
            this.SplitContainer1.Panel2.Controls.Add(this.txtDice2);
            this.SplitContainer1.Panel2.Controls.Add(this.txtDice1);
            this.SplitContainer1.Panel2.Controls.Add(this.txtDice0);
            this.SplitContainer1.Panel2.Controls.Add(this.gbGame);
            this.SplitContainer1.Panel2.Controls.Add(this.gbActions);
            this.SplitContainer1.Panel2.Controls.Add(this.TableLayoutPanel1);
            this.SplitContainer1.Size = new System.Drawing.Size(834, 565);
            this.SplitContainer1.SplitterDistance = 607;
            this.SplitContainer1.TabIndex = 0;
            // 
            // lblActionText
            // 
            this.lblActionText.Location = new System.Drawing.Point(9, 384);
            this.lblActionText.Name = "lblActionText";
            this.lblActionText.Size = new System.Drawing.Size(197, 53);
            this.lblActionText.TabIndex = 4;
            this.lblActionText.Text = "lblActionText";
            // 
            // txtDice2
            // 
            this.txtDice2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtDice2.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
            this.txtDice2.Location = new System.Drawing.Point(84, 350);
            this.txtDice2.Name = "txtDice2";
            this.txtDice2.ReadOnly = true;
            this.txtDice2.Size = new System.Drawing.Size(31, 31);
            this.txtDice2.TabIndex = 3;
            this.txtDice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDice2.Visible = false;
            // 
            // txtDice1
            // 
            this.txtDice1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtDice1.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
            this.txtDice1.Location = new System.Drawing.Point(49, 350);
            this.txtDice1.Name = "txtDice1";
            this.txtDice1.ReadOnly = true;
            this.txtDice1.Size = new System.Drawing.Size(31, 31);
            this.txtDice1.TabIndex = 3;
            this.txtDice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDice1.Visible = false;
            // 
            // txtDice0
            // 
            this.txtDice0.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtDice0.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
            this.txtDice0.Location = new System.Drawing.Point(12, 350);
            this.txtDice0.Name = "txtDice0";
            this.txtDice0.ReadOnly = true;
            this.txtDice0.Size = new System.Drawing.Size(31, 31);
            this.txtDice0.TabIndex = 3;
            this.txtDice0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDice0.Visible = false;
            // 
            // gbGame
            // 
            this.gbGame.Controls.Add(this.btnStartGame);
            this.gbGame.Controls.Add(this.txtGameBoardFile);
            this.gbGame.Controls.Add(this.Label1);
            this.gbGame.Location = new System.Drawing.Point(6, 440);
            this.gbGame.Name = "gbGame";
            this.gbGame.Size = new System.Drawing.Size(208, 113);
            this.gbGame.TabIndex = 2;
            this.gbGame.TabStop = false;
            this.gbGame.Text = "Spiel starten";
            // 
            // btnStartGame
            // 
            this.btnStartGame.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            this.btnStartGame.Location = new System.Drawing.Point(9, 43);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(191, 59);
            this.btnStartGame.TabIndex = 2;
            this.btnStartGame.Text = "Spiel starten";
            this.btnStartGame.UseVisualStyleBackColor = true;
            // 
            // txtGameBoardFile
            // 
            this.txtGameBoardFile.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            this.txtGameBoardFile.Location = new System.Drawing.Point(69, 17);
            this.txtGameBoardFile.Name = "txtGameBoardFile";
            this.txtGameBoardFile.Size = new System.Drawing.Size(131, 20);
            this.txtGameBoardFile.TabIndex = 1;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(7, 20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(54, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Spielbrett:";
            // 
            // gbActions
            // 
            this.gbActions.Controls.Add(this.lblActivePlayer);
            this.gbActions.Controls.Add(this.btnAction6);
            this.gbActions.Controls.Add(this.btnAction5);
            this.gbActions.Controls.Add(this.btnAction4);
            this.gbActions.Controls.Add(this.btnAction3);
            this.gbActions.Controls.Add(this.btnAction2);
            this.gbActions.Controls.Add(this.btnAction1);
            this.gbActions.Controls.Add(this.lblActivePlayerDesc);
            this.gbActions.Location = new System.Drawing.Point(6, 139);
            this.gbActions.Name = "gbActions";
            this.gbActions.Size = new System.Drawing.Size(214, 205);
            this.gbActions.TabIndex = 1;
            this.gbActions.TabStop = false;
            this.gbActions.Text = "Spiel Aktionen:";
            // 
            // lblActivePlayer
            // 
            this.lblActivePlayer.AutoSize = true;
            this.lblActivePlayer.Location = new System.Drawing.Point(93, 16);
            this.lblActivePlayer.Name = "lblActivePlayer";
            this.lblActivePlayer.Size = new System.Drawing.Size(0, 13);
            this.lblActivePlayer.TabIndex = 6;
            // 
            // btnAction5
            // 
            this.btnAction5.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            this.btnAction5.Location = new System.Drawing.Point(6, 146);
            this.btnAction5.Name = "btnAction5";
            this.btnAction5.Size = new System.Drawing.Size(202, 23);
            this.btnAction5.TabIndex = 5;
            this.btnAction5.Text = "Button5";
            this.btnAction5.UseVisualStyleBackColor = true;
            // 
            // btnAction4
            // 
            this.btnAction4.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            this.btnAction4.Location = new System.Drawing.Point(6, 117);
            this.btnAction4.Name = "btnAction4";
            this.btnAction4.Size = new System.Drawing.Size(202, 23);
            this.btnAction4.TabIndex = 4;
            this.btnAction4.Text = "Button4";
            this.btnAction4.UseVisualStyleBackColor = true;
            // 
            // btnAction3
            // 
            this.btnAction3.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            this.btnAction3.Location = new System.Drawing.Point(6, 88);
            this.btnAction3.Name = "btnAction3";
            this.btnAction3.Size = new System.Drawing.Size(202, 23);
            this.btnAction3.TabIndex = 3;
            this.btnAction3.Text = "Button3";
            this.btnAction3.UseVisualStyleBackColor = true;
            // 
            // btnAction2
            // 
            this.btnAction2.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            this.btnAction2.Location = new System.Drawing.Point(6, 59);
            this.btnAction2.Name = "btnAction2";
            this.btnAction2.Size = new System.Drawing.Size(202, 23);
            this.btnAction2.TabIndex = 2;
            this.btnAction2.Text = "Button2";
            this.btnAction2.UseVisualStyleBackColor = true;
            // 
            // btnAction1
            // 
            this.btnAction1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            this.btnAction1.Location = new System.Drawing.Point(6, 32);
            this.btnAction1.Name = "btnAction1";
            this.btnAction1.Size = new System.Drawing.Size(202, 21);
            this.btnAction1.TabIndex = 1;
            this.btnAction1.Text = "Button1";
            this.btnAction1.UseVisualStyleBackColor = true;
            // 
            // lblActivePlayerDesc
            // 
            this.lblActivePlayerDesc.AutoSize = true;
            this.lblActivePlayerDesc.Location = new System.Drawing.Point(6, 16);
            this.lblActivePlayerDesc.Name = "lblActivePlayerDesc";
            this.lblActivePlayerDesc.Size = new System.Drawing.Size(81, 13);
            this.lblActivePlayerDesc.TabIndex = 0;
            this.lblActivePlayerDesc.Text = "Aktiver Spieler: ";
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.ColumnCount = 4;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69.0F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48.0F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66.0F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43.0F));
            this.TableLayoutPanel1.Controls.Add(this.btnColor4, 3, 3);
            this.TableLayoutPanel1.Controls.Add(this.btnColor3, 3, 2);
            this.TableLayoutPanel1.Controls.Add(this.btnColor2, 3, 1);
            this.TableLayoutPanel1.Controls.Add(this.txtPlayer1, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.txtCash1, 1, 0);
            this.TableLayoutPanel1.Controls.Add(this.txtPlayer2, 0, 1);
            this.TableLayoutPanel1.Controls.Add(this.txtCash2, 1, 1);
            this.TableLayoutPanel1.Controls.Add(this.txtPlayer3, 0, 2);
            this.TableLayoutPanel1.Controls.Add(this.txtCash3, 1, 2);
            this.TableLayoutPanel1.Controls.Add(this.txtPlayer4, 0, 3);
            this.TableLayoutPanel1.Controls.Add(this.txtCash4, 1, 3);
            this.TableLayoutPanel1.Controls.Add(this.lstType1, 2, 0);
            this.TableLayoutPanel1.Controls.Add(this.lstType2, 2, 1);
            this.TableLayoutPanel1.Controls.Add(this.lstType3, 2, 2);
            this.TableLayoutPanel1.Controls.Add(this.lstType4, 2, 3);
            this.TableLayoutPanel1.Controls.Add(this.btnColor1, 3, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 4;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(217, 130);
            this.TableLayoutPanel1.TabIndex = 0;
            // 
            // btnColor4
            // 
            this.btnColor4.AutoSize = true;
            this.btnColor4.BackColor = System.Drawing.Color.Yellow;
            this.btnColor4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor4.Location = new System.Drawing.Point(186, 93);
            this.btnColor4.Name = "btnColor4";
            this.btnColor4.Size = new System.Drawing.Size(27, 23);
            this.btnColor4.TabIndex = 15;
            this.btnColor4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnColor4.UseVisualStyleBackColor = false;
            // 
            // btnColor3
            // 
            this.btnColor3.AutoSize = true;
            this.btnColor3.BackColor = System.Drawing.Color.Green;
            this.btnColor3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor3.Location = new System.Drawing.Point(186, 63);
            this.btnColor3.Name = "btnColor3";
            this.btnColor3.Size = new System.Drawing.Size(27, 23);
            this.btnColor3.TabIndex = 14;
            this.btnColor3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnColor3.UseVisualStyleBackColor = false;
            // 
            // btnColor2
            // 
            this.btnColor2.AutoSize = true;
            this.btnColor2.BackColor = System.Drawing.Color.Cyan;
            this.btnColor2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor2.Location = new System.Drawing.Point(186, 33);
            this.btnColor2.Name = "btnColor2";
            this.btnColor2.Size = new System.Drawing.Size(27, 23);
            this.btnColor2.TabIndex = 13;
            this.btnColor2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnColor2.UseVisualStyleBackColor = false;
            // 
            // txtPlayer1
            // 
            this.txtPlayer1.Location = new System.Drawing.Point(3, 3);
            this.txtPlayer1.Name = "txtPlayer1";
            this.txtPlayer1.Size = new System.Drawing.Size(63, 20);
            this.txtPlayer1.TabIndex = 0;
            this.txtPlayer1.Text = "Marco";
            // 
            // txtCash1
            // 
            this.txtCash1.Enabled = false;
            this.txtCash1.Location = new System.Drawing.Point(72, 3);
            this.txtCash1.Name = "txtCash1";
            this.txtCash1.Size = new System.Drawing.Size(40, 20);
            this.txtCash1.TabIndex = 1;
            this.txtCash1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPlayer2
            // 
            this.txtPlayer2.Location = new System.Drawing.Point(3, 33);
            this.txtPlayer2.Name = "txtPlayer2";
            this.txtPlayer2.Size = new System.Drawing.Size(63, 20);
            this.txtPlayer2.TabIndex = 2;
            this.txtPlayer2.Text = "Malte";
            // 
            // txtCash2
            // 
            this.txtCash2.Enabled = false;
            this.txtCash2.Location = new System.Drawing.Point(72, 33);
            this.txtCash2.Name = "txtCash2";
            this.txtCash2.Size = new System.Drawing.Size(40, 20);
            this.txtCash2.TabIndex = 3;
            this.txtCash2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPlayer3
            // 
            this.txtPlayer3.Location = new System.Drawing.Point(3, 63);
            this.txtPlayer3.Name = "txtPlayer3";
            this.txtPlayer3.Size = new System.Drawing.Size(63, 20);
            this.txtPlayer3.TabIndex = 4;
            this.txtPlayer3.Text = "Manfred";
            // 
            // txtCash3
            // 
            this.txtCash3.Enabled = false;
            this.txtCash3.Location = new System.Drawing.Point(72, 63);
            this.txtCash3.Name = "txtCash3";
            this.txtCash3.Size = new System.Drawing.Size(40, 20);
            this.txtCash3.TabIndex = 5;
            this.txtCash3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPlayer4
            // 
            this.txtPlayer4.Location = new System.Drawing.Point(3, 93);
            this.txtPlayer4.Name = "txtPlayer4";
            this.txtPlayer4.Size = new System.Drawing.Size(63, 20);
            this.txtPlayer4.TabIndex = 6;
            // 
            // txtCash4
            // 
            this.txtCash4.Enabled = false;
            this.txtCash4.Location = new System.Drawing.Point(72, 93);
            this.txtCash4.Name = "txtCash4";
            this.txtCash4.Size = new System.Drawing.Size(40, 20);
            this.txtCash4.TabIndex = 7;
            this.txtCash4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lstType1
            // 
            this.lstType1.FormattingEnabled = true;
            this.lstType1.Items.AddRange(new object[] { "(none)", "Human", "Bot" });
            this.lstType1.Location = new System.Drawing.Point(120, 3);
            this.lstType1.Name = "lstType1";
            this.lstType1.Size = new System.Drawing.Size(60, 21);
            this.lstType1.TabIndex = 8;
            this.lstType1.Text = "Human";
            // 
            // lstType2
            // 
            this.lstType2.FormattingEnabled = true;
            this.lstType2.Items.AddRange(new object[] { "(none)", "Human", "Bot" });
            this.lstType2.Location = new System.Drawing.Point(120, 33);
            this.lstType2.Name = "lstType2";
            this.lstType2.Size = new System.Drawing.Size(60, 21);
            this.lstType2.TabIndex = 9;
            this.lstType2.Text = "Human";
            // 
            // lstType3
            // 
            this.lstType3.FormattingEnabled = true;
            this.lstType3.Items.AddRange(new object[] { "(none)", "Human", "Bot" });
            this.lstType3.Location = new System.Drawing.Point(120, 63);
            this.lstType3.Name = "lstType3";
            this.lstType3.Size = new System.Drawing.Size(60, 21);
            this.lstType3.TabIndex = 10;
            // 
            // lstType4
            // 
            this.lstType4.FormattingEnabled = true;
            this.lstType4.Items.AddRange(new object[] { "(none)", "Human", "Bot" });
            this.lstType4.Location = new System.Drawing.Point(120, 93);
            this.lstType4.Name = "lstType4";
            this.lstType4.Size = new System.Drawing.Size(60, 21);
            this.lstType4.TabIndex = 11;
            // 
            // btnColor1
            // 
            this.btnColor1.AutoSize = true;
            this.btnColor1.BackColor = System.Drawing.Color.Red;
            this.btnColor1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor1.Location = new System.Drawing.Point(186, 3);
            this.btnColor1.Name = "btnColor1";
            this.btnColor1.Size = new System.Drawing.Size(27, 23);
            this.btnColor1.TabIndex = 12;
            this.btnColor1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnColor1.UseVisualStyleBackColor = false;
            // 
            // btnAction6
            // 
            this.btnAction6.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            this.btnAction6.Location = new System.Drawing.Point(6, 175);
            this.btnAction6.Name = "btnAction6";
            this.btnAction6.Size = new System.Drawing.Size(202, 23);
            this.btnAction6.TabIndex = 5;
            this.btnAction6.Text = "Button5";
            this.btnAction6.UseVisualStyleBackColor = true;
            // 
            // frmSimpleGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 565);
            this.Controls.Add(this.SplitContainer1);
            this.Name = "frmSimpleGui";
            this.Text = "DotNetPoly-SimpleGUI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            this.gbGame.ResumeLayout(false);
            this.gbGame.PerformLayout();
            this.gbActions.ResumeLayout(false);
            this.gbActions.PerformLayout();
            this.TableLayoutPanel1.ResumeLayout(false);
            this.TableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
        }

        private SplitContainer _SplitContainer1;

        internal SplitContainer SplitContainer1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _SplitContainer1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SplitContainer1 != null)
                {
                }

                _SplitContainer1 = value;
                if (_SplitContainer1 != null)
                {
                }
            }
        }

        private TableLayoutPanel _TableLayoutPanel1;

        internal TableLayoutPanel TableLayoutPanel1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _TableLayoutPanel1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TableLayoutPanel1 != null)
                {
                }

                _TableLayoutPanel1 = value;
                if (_TableLayoutPanel1 != null)
                {
                }
            }
        }

        private TextBox _txtPlayer1;

        internal TextBox txtPlayer1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtPlayer1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtPlayer1 != null)
                {
                }

                _txtPlayer1 = value;
                if (_txtPlayer1 != null)
                {
                }
            }
        }

        private TextBox _txtCash1;

        internal TextBox txtCash1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtCash1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtCash1 != null)
                {
                }

                _txtCash1 = value;
                if (_txtCash1 != null)
                {
                }
            }
        }

        private TextBox _txtPlayer2;

        internal TextBox txtPlayer2
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtPlayer2;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtPlayer2 != null)
                {
                }

                _txtPlayer2 = value;
                if (_txtPlayer2 != null)
                {
                }
            }
        }

        private TextBox _txtCash2;

        internal TextBox txtCash2
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtCash2;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtCash2 != null)
                {
                }

                _txtCash2 = value;
                if (_txtCash2 != null)
                {
                }
            }
        }

        private TextBox _txtPlayer3;

        internal TextBox txtPlayer3
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtPlayer3;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtPlayer3 != null)
                {
                }

                _txtPlayer3 = value;
                if (_txtPlayer3 != null)
                {
                }
            }
        }

        private TextBox _txtCash3;

        internal TextBox txtCash3
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtCash3;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtCash3 != null)
                {
                }

                _txtCash3 = value;
                if (_txtCash3 != null)
                {
                }
            }
        }

        private TextBox _txtPlayer4;

        internal TextBox txtPlayer4
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtPlayer4;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtPlayer4 != null)
                {
                }

                _txtPlayer4 = value;
                if (_txtPlayer4 != null)
                {
                }
            }
        }

        private TextBox _txtCash4;

        internal TextBox txtCash4
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtCash4;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtCash4 != null)
                {
                }

                _txtCash4 = value;
                if (_txtCash4 != null)
                {
                }
            }
        }

        private ComboBox _lstType1;

        internal ComboBox lstType1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _lstType1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_lstType1 != null)
                {
                }

                _lstType1 = value;
                if (_lstType1 != null)
                {
                }
            }
        }

        private ComboBox _lstType2;

        internal ComboBox lstType2
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _lstType2;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_lstType2 != null)
                {
                }

                _lstType2 = value;
                if (_lstType2 != null)
                {
                }
            }
        }

        private ComboBox _lstType3;

        internal ComboBox lstType3
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _lstType3;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_lstType3 != null)
                {
                }

                _lstType3 = value;
                if (_lstType3 != null)
                {
                }
            }
        }

        private ComboBox _lstType4;

        internal ComboBox lstType4
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _lstType4;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_lstType4 != null)
                {
                }

                _lstType4 = value;
                if (_lstType4 != null)
                {
                }
            }
        }

        private GroupBox _gbActions;

        internal GroupBox gbActions
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _gbActions;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_gbActions != null)
                {
                }

                _gbActions = value;
                if (_gbActions != null)
                {
                }
            }
        }

        private Label _lblActivePlayer;

        internal Label lblActivePlayer
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _lblActivePlayer;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_lblActivePlayer != null)
                {
                }

                _lblActivePlayer = value;
                if (_lblActivePlayer != null)
                {
                }
            }
        }

        private Button _btnAction5;

        internal Button btnAction5
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _btnAction5;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_btnAction5 != null)
                {
                }

                _btnAction5 = value;
                if (_btnAction5 != null)
                {
                }
            }
        }

        private Button _btnAction4;

        internal Button btnAction4
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _btnAction4;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_btnAction4 != null)
                {
                }

                _btnAction4 = value;
                if (_btnAction4 != null)
                {
                }
            }
        }

        private Button _btnAction3;

        internal Button btnAction3
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _btnAction3;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_btnAction3 != null)
                {
                }

                _btnAction3 = value;
                if (_btnAction3 != null)
                {
                }
            }
        }

        private Button _btnAction2;

        internal Button btnAction2
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _btnAction2;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_btnAction2 != null)
                {
                }

                _btnAction2 = value;
                if (_btnAction2 != null)
                {
                }
            }
        }

        private Button _btnAction1;

        internal Button btnAction1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _btnAction1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_btnAction1 != null)
                {
                }

                _btnAction1 = value;
                if (_btnAction1 != null)
                {
                }
            }
        }

        private Label _lblActivePlayerDesc;

        internal Label lblActivePlayerDesc
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _lblActivePlayerDesc;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_lblActivePlayerDesc != null)
                {
                }

                _lblActivePlayerDesc = value;
                if (_lblActivePlayerDesc != null)
                {
                }
            }
        }

        private GroupBox _gbGame;

        internal GroupBox gbGame
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _gbGame;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_gbGame != null)
                {
                }

                _gbGame = value;
                if (_gbGame != null)
                {
                }
            }
        }

        private TextBox _txtGameBoardFile;

        internal TextBox txtGameBoardFile
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtGameBoardFile;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtGameBoardFile != null)
                {
                }

                _txtGameBoardFile = value;
                if (_txtGameBoardFile != null)
                {
                }
            }
        }

        private Label _Label1;

        internal Label Label1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Label1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Label1 != null)
                {
                }

                _Label1 = value;
                if (_Label1 != null)
                {
                }
            }
        }

        private Button _btnStartGame;

        internal Button btnStartGame
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _btnStartGame;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_btnStartGame != null)
                {
                }

                _btnStartGame = value;
                if (_btnStartGame != null)
                {
                }
            }
        }

        private TextBox _txtDice1;

        internal TextBox txtDice1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtDice1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtDice1 != null)
                {
                }

                _txtDice1 = value;
                if (_txtDice1 != null)
                {
                }
            }
        }

        private TextBox _txtDice0;

        internal TextBox txtDice0
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtDice0;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtDice0 != null)
                {
                }

                _txtDice0 = value;
                if (_txtDice0 != null)
                {
                }
            }
        }

        private Label _lblActionText;

        internal Label lblActionText
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _lblActionText;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_lblActionText != null)
                {
                }

                _lblActionText = value;
                if (_lblActionText != null)
                {
                }
            }
        }

        private Button _btnColor4;

        internal Button btnColor4
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _btnColor4;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_btnColor4 != null)
                {
                }

                _btnColor4 = value;
                if (_btnColor4 != null)
                {
                }
            }
        }

        private Button _btnColor3;

        internal Button btnColor3
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _btnColor3;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_btnColor3 != null)
                {
                }

                _btnColor3 = value;
                if (_btnColor3 != null)
                {
                }
            }
        }

        private Button _btnColor2;

        internal Button btnColor2
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _btnColor2;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_btnColor2 != null)
                {
                }

                _btnColor2 = value;
                if (_btnColor2 != null)
                {
                }
            }
        }

        private Button _btnColor1;

        internal Button btnColor1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _btnColor1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_btnColor1 != null)
                {
                }

                _btnColor1 = value;
                if (_btnColor1 != null)
                {
                }
            }
        }

        private TextBox _txtDice2;

        internal TextBox txtDice2
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txtDice2;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txtDice2 != null)
                {
                }

                _txtDice2 = value;
                if (_txtDice2 != null)
                {
                }
            }
        }

        private Button _btnAction6;

        internal Button btnAction6
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _btnAction6;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_btnAction6 != null)
                {
                }

                _btnAction6 = value;
                if (_btnAction6 != null)
                {
                }
            }
        }
    }
}
