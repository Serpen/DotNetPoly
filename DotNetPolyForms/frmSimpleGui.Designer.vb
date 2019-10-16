<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSimpleGui
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblActionText = New System.Windows.Forms.Label()
        Me.txtDice2 = New System.Windows.Forms.TextBox()
        Me.txtDice1 = New System.Windows.Forms.TextBox()
        Me.txtDice0 = New System.Windows.Forms.TextBox()
        Me.gbGame = New System.Windows.Forms.GroupBox()
        Me.btnStartGame = New System.Windows.Forms.Button()
        Me.txtGameBoardFile = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbActions = New System.Windows.Forms.GroupBox()
        Me.lblActivePlayer = New System.Windows.Forms.Label()
        Me.btnAction5 = New System.Windows.Forms.Button()
        Me.btnAction4 = New System.Windows.Forms.Button()
        Me.btnAction3 = New System.Windows.Forms.Button()
        Me.btnAction2 = New System.Windows.Forms.Button()
        Me.btnAction1 = New System.Windows.Forms.Button()
        Me.lblActivePlayerDesc = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnColor4 = New System.Windows.Forms.Button()
        Me.btnColor3 = New System.Windows.Forms.Button()
        Me.btnColor2 = New System.Windows.Forms.Button()
        Me.txtPlayer1 = New System.Windows.Forms.TextBox()
        Me.txtCash1 = New System.Windows.Forms.TextBox()
        Me.txtPlayer2 = New System.Windows.Forms.TextBox()
        Me.txtCash2 = New System.Windows.Forms.TextBox()
        Me.txtPlayer3 = New System.Windows.Forms.TextBox()
        Me.txtCash3 = New System.Windows.Forms.TextBox()
        Me.txtPlayer4 = New System.Windows.Forms.TextBox()
        Me.txtCash4 = New System.Windows.Forms.TextBox()
        Me.lstType1 = New System.Windows.Forms.ComboBox()
        Me.lstType2 = New System.Windows.Forms.ComboBox()
        Me.lstType3 = New System.Windows.Forms.ComboBox()
        Me.lstType4 = New System.Windows.Forms.ComboBox()
        Me.btnColor1 = New System.Windows.Forms.Button()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.gbGame.SuspendLayout()
        Me.gbActions.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblActionText)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtDice2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtDice1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtDice0)
        Me.SplitContainer1.Panel2.Controls.Add(Me.gbGame)
        Me.SplitContainer1.Panel2.Controls.Add(Me.gbActions)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(834, 565)
        Me.SplitContainer1.SplitterDistance = 607
        Me.SplitContainer1.TabIndex = 0
        '
        'lblActionText
        '
        Me.lblActionText.Location = New System.Drawing.Point(9, 384)
        Me.lblActionText.Name = "lblActionText"
        Me.lblActionText.Size = New System.Drawing.Size(197, 53)
        Me.lblActionText.TabIndex = 4
        Me.lblActionText.Text = "lblActionText"
        '
        'txtDice2
        '
        Me.txtDice2.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtDice2.Font = New System.Drawing.Font("Courier New", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDice2.Location = New System.Drawing.Point(84, 340)
        Me.txtDice2.Name = "txtDice2"
        Me.txtDice2.ReadOnly = True
        Me.txtDice2.Size = New System.Drawing.Size(31, 31)
        Me.txtDice2.TabIndex = 3
        Me.txtDice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtDice2.Visible = False
        '
        'txtDice1
        '
        Me.txtDice1.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtDice1.Font = New System.Drawing.Font("Courier New", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDice1.Location = New System.Drawing.Point(49, 340)
        Me.txtDice1.Name = "txtDice1"
        Me.txtDice1.ReadOnly = True
        Me.txtDice1.Size = New System.Drawing.Size(31, 31)
        Me.txtDice1.TabIndex = 3
        Me.txtDice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtDice1.Visible = False
        '
        'txtDice0
        '
        Me.txtDice0.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtDice0.Font = New System.Drawing.Font("Courier New", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDice0.Location = New System.Drawing.Point(12, 340)
        Me.txtDice0.Name = "txtDice0"
        Me.txtDice0.ReadOnly = True
        Me.txtDice0.Size = New System.Drawing.Size(31, 31)
        Me.txtDice0.TabIndex = 3
        Me.txtDice0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtDice0.Visible = False
        '
        'gbGame
        '
        Me.gbGame.Controls.Add(Me.btnStartGame)
        Me.gbGame.Controls.Add(Me.txtGameBoardFile)
        Me.gbGame.Controls.Add(Me.Label1)
        Me.gbGame.Location = New System.Drawing.Point(6, 440)
        Me.gbGame.Name = "gbGame"
        Me.gbGame.Size = New System.Drawing.Size(208, 113)
        Me.gbGame.TabIndex = 2
        Me.gbGame.TabStop = False
        Me.gbGame.Text = "Spiel starten"
        '
        'btnStartGame
        '
        Me.btnStartGame.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStartGame.Location = New System.Drawing.Point(9, 43)
        Me.btnStartGame.Name = "btnStartGame"
        Me.btnStartGame.Size = New System.Drawing.Size(191, 59)
        Me.btnStartGame.TabIndex = 2
        Me.btnStartGame.Text = "Spiel starten"
        Me.btnStartGame.UseVisualStyleBackColor = True
        '
        'txtGameBoardFile
        '
        Me.txtGameBoardFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGameBoardFile.Location = New System.Drawing.Point(69, 17)
        Me.txtGameBoardFile.Name = "txtGameBoardFile"
        Me.txtGameBoardFile.Size = New System.Drawing.Size(131, 20)
        Me.txtGameBoardFile.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Spielbrett:"
        '
        'gbActions
        '
        Me.gbActions.Controls.Add(Me.lblActivePlayer)
        Me.gbActions.Controls.Add(Me.btnAction5)
        Me.gbActions.Controls.Add(Me.btnAction4)
        Me.gbActions.Controls.Add(Me.btnAction3)
        Me.gbActions.Controls.Add(Me.btnAction2)
        Me.gbActions.Controls.Add(Me.btnAction1)
        Me.gbActions.Controls.Add(Me.lblActivePlayerDesc)
        Me.gbActions.Location = New System.Drawing.Point(6, 139)
        Me.gbActions.Name = "gbActions"
        Me.gbActions.Size = New System.Drawing.Size(214, 195)
        Me.gbActions.TabIndex = 1
        Me.gbActions.TabStop = False
        Me.gbActions.Text = "Spiel Aktionen:"
        '
        'lblActivePlayer
        '
        Me.lblActivePlayer.AutoSize = True
        Me.lblActivePlayer.Location = New System.Drawing.Point(93, 16)
        Me.lblActivePlayer.Name = "lblActivePlayer"
        Me.lblActivePlayer.Size = New System.Drawing.Size(0, 13)
        Me.lblActivePlayer.TabIndex = 6
        '
        'btnAction5
        '
        Me.btnAction5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAction5.Location = New System.Drawing.Point(6, 148)
        Me.btnAction5.Name = "btnAction5"
        Me.btnAction5.Size = New System.Drawing.Size(202, 23)
        Me.btnAction5.TabIndex = 5
        Me.btnAction5.Text = "Button5"
        Me.btnAction5.UseVisualStyleBackColor = True
        '
        'btnAction4
        '
        Me.btnAction4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAction4.Location = New System.Drawing.Point(6, 119)
        Me.btnAction4.Name = "btnAction4"
        Me.btnAction4.Size = New System.Drawing.Size(202, 23)
        Me.btnAction4.TabIndex = 4
        Me.btnAction4.Text = "Button4"
        Me.btnAction4.UseVisualStyleBackColor = True
        '
        'btnAction3
        '
        Me.btnAction3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAction3.Location = New System.Drawing.Point(6, 90)
        Me.btnAction3.Name = "btnAction3"
        Me.btnAction3.Size = New System.Drawing.Size(202, 23)
        Me.btnAction3.TabIndex = 3
        Me.btnAction3.Text = "Button3"
        Me.btnAction3.UseVisualStyleBackColor = True
        '
        'btnAction2
        '
        Me.btnAction2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAction2.Location = New System.Drawing.Point(6, 61)
        Me.btnAction2.Name = "btnAction2"
        Me.btnAction2.Size = New System.Drawing.Size(202, 23)
        Me.btnAction2.TabIndex = 2
        Me.btnAction2.Text = "Button2"
        Me.btnAction2.UseVisualStyleBackColor = True
        '
        'btnAction1
        '
        Me.btnAction1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAction1.Location = New System.Drawing.Point(6, 32)
        Me.btnAction1.Name = "btnAction1"
        Me.btnAction1.Size = New System.Drawing.Size(202, 23)
        Me.btnAction1.TabIndex = 1
        Me.btnAction1.Text = "Button1"
        Me.btnAction1.UseVisualStyleBackColor = True
        '
        'lblActivePlayerDesc
        '
        Me.lblActivePlayerDesc.AutoSize = True
        Me.lblActivePlayerDesc.Location = New System.Drawing.Point(6, 16)
        Me.lblActivePlayerDesc.Name = "lblActivePlayerDesc"
        Me.lblActivePlayerDesc.Size = New System.Drawing.Size(81, 13)
        Me.lblActivePlayerDesc.TabIndex = 0
        Me.lblActivePlayerDesc.Text = "Aktiver Spieler: "
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnColor4, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btnColor3, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btnColor2, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPlayer1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCash1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPlayer2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCash2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPlayer3, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCash3, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPlayer4, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCash4, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lstType1, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lstType2, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lstType3, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lstType4, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btnColor1, 3, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(217, 130)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'btnColor4
        '
        Me.btnColor4.AutoSize = True
        Me.btnColor4.BackColor = System.Drawing.Color.Yellow
        Me.btnColor4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnColor4.Location = New System.Drawing.Point(186, 93)
        Me.btnColor4.Name = "btnColor4"
        Me.btnColor4.Size = New System.Drawing.Size(27, 23)
        Me.btnColor4.TabIndex = 15
        Me.btnColor4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnColor4.UseVisualStyleBackColor = False
        '
        'btnColor3
        '
        Me.btnColor3.AutoSize = True
        Me.btnColor3.BackColor = System.Drawing.Color.Green
        Me.btnColor3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnColor3.Location = New System.Drawing.Point(186, 63)
        Me.btnColor3.Name = "btnColor3"
        Me.btnColor3.Size = New System.Drawing.Size(27, 23)
        Me.btnColor3.TabIndex = 14
        Me.btnColor3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnColor3.UseVisualStyleBackColor = False
        '
        'btnColor2
        '
        Me.btnColor2.AutoSize = True
        Me.btnColor2.BackColor = System.Drawing.Color.Cyan
        Me.btnColor2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnColor2.Location = New System.Drawing.Point(186, 33)
        Me.btnColor2.Name = "btnColor2"
        Me.btnColor2.Size = New System.Drawing.Size(27, 23)
        Me.btnColor2.TabIndex = 13
        Me.btnColor2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnColor2.UseVisualStyleBackColor = False
        '
        'txtPlayer1
        '
        Me.txtPlayer1.Location = New System.Drawing.Point(3, 3)
        Me.txtPlayer1.Name = "txtPlayer1"
        Me.txtPlayer1.Size = New System.Drawing.Size(63, 20)
        Me.txtPlayer1.TabIndex = 0
        Me.txtPlayer1.Text = "Marco"
        '
        'txtCash1
        '
        Me.txtCash1.Enabled = False
        Me.txtCash1.Location = New System.Drawing.Point(72, 3)
        Me.txtCash1.Name = "txtCash1"
        Me.txtCash1.Size = New System.Drawing.Size(40, 20)
        Me.txtCash1.TabIndex = 1
        Me.txtCash1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPlayer2
        '
        Me.txtPlayer2.Location = New System.Drawing.Point(3, 33)
        Me.txtPlayer2.Name = "txtPlayer2"
        Me.txtPlayer2.Size = New System.Drawing.Size(63, 20)
        Me.txtPlayer2.TabIndex = 2
        Me.txtPlayer2.Text = "Malte"
        '
        'txtCash2
        '
        Me.txtCash2.Enabled = False
        Me.txtCash2.Location = New System.Drawing.Point(72, 33)
        Me.txtCash2.Name = "txtCash2"
        Me.txtCash2.Size = New System.Drawing.Size(40, 20)
        Me.txtCash2.TabIndex = 3
        Me.txtCash2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPlayer3
        '
        Me.txtPlayer3.Location = New System.Drawing.Point(3, 63)
        Me.txtPlayer3.Name = "txtPlayer3"
        Me.txtPlayer3.Size = New System.Drawing.Size(63, 20)
        Me.txtPlayer3.TabIndex = 4
        Me.txtPlayer3.Text = "Manfred"
        '
        'txtCash3
        '
        Me.txtCash3.Enabled = False
        Me.txtCash3.Location = New System.Drawing.Point(72, 63)
        Me.txtCash3.Name = "txtCash3"
        Me.txtCash3.Size = New System.Drawing.Size(40, 20)
        Me.txtCash3.TabIndex = 5
        Me.txtCash3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPlayer4
        '
        Me.txtPlayer4.Location = New System.Drawing.Point(3, 93)
        Me.txtPlayer4.Name = "txtPlayer4"
        Me.txtPlayer4.Size = New System.Drawing.Size(63, 20)
        Me.txtPlayer4.TabIndex = 6
        '
        'txtCash4
        '
        Me.txtCash4.Enabled = False
        Me.txtCash4.Location = New System.Drawing.Point(72, 93)
        Me.txtCash4.Name = "txtCash4"
        Me.txtCash4.Size = New System.Drawing.Size(40, 20)
        Me.txtCash4.TabIndex = 7
        Me.txtCash4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lstType1
        '
        Me.lstType1.FormattingEnabled = True
        Me.lstType1.Items.AddRange(New Object() {"(none)", "Human", "Bot"})
        Me.lstType1.Location = New System.Drawing.Point(120, 3)
        Me.lstType1.Name = "lstType1"
        Me.lstType1.Size = New System.Drawing.Size(60, 21)
        Me.lstType1.TabIndex = 8
        Me.lstType1.Text = "Human"
        '
        'lstType2
        '
        Me.lstType2.FormattingEnabled = True
        Me.lstType2.Items.AddRange(New Object() {"(none)", "Human", "Bot"})
        Me.lstType2.Location = New System.Drawing.Point(120, 33)
        Me.lstType2.Name = "lstType2"
        Me.lstType2.Size = New System.Drawing.Size(60, 21)
        Me.lstType2.TabIndex = 9
        Me.lstType2.Text = "Human"
        '
        'lstType3
        '
        Me.lstType3.FormattingEnabled = True
        Me.lstType3.Items.AddRange(New Object() {"(none)", "Human", "Bot"})
        Me.lstType3.Location = New System.Drawing.Point(120, 63)
        Me.lstType3.Name = "lstType3"
        Me.lstType3.Size = New System.Drawing.Size(60, 21)
        Me.lstType3.TabIndex = 10
        '
        'lstType4
        '
        Me.lstType4.FormattingEnabled = True
        Me.lstType4.Items.AddRange(New Object() {"(none)", "Human", "Bot"})
        Me.lstType4.Location = New System.Drawing.Point(120, 93)
        Me.lstType4.Name = "lstType4"
        Me.lstType4.Size = New System.Drawing.Size(60, 21)
        Me.lstType4.TabIndex = 11
        '
        'btnColor1
        '
        Me.btnColor1.AutoSize = True
        Me.btnColor1.BackColor = System.Drawing.Color.Red
        Me.btnColor1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnColor1.Location = New System.Drawing.Point(186, 3)
        Me.btnColor1.Name = "btnColor1"
        Me.btnColor1.Size = New System.Drawing.Size(27, 23)
        Me.btnColor1.TabIndex = 12
        Me.btnColor1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnColor1.UseVisualStyleBackColor = False
        '
        'frmSimpleGui
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 565)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmSimpleGui"
        Me.Text = "DotNetPoly-SimpleGUI"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.gbGame.ResumeLayout(False)
        Me.gbGame.PerformLayout()
        Me.gbActions.ResumeLayout(False)
        Me.gbActions.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents txtPlayer1 As TextBox
    Friend WithEvents txtCash1 As TextBox
    Friend WithEvents txtPlayer2 As TextBox
    Friend WithEvents txtCash2 As TextBox
    Friend WithEvents txtPlayer3 As TextBox
    Friend WithEvents txtCash3 As TextBox
    Friend WithEvents txtPlayer4 As TextBox
    Friend WithEvents txtCash4 As TextBox
    Friend WithEvents lstType1 As ComboBox
    Friend WithEvents lstType2 As ComboBox
    Friend WithEvents lstType3 As ComboBox
    Friend WithEvents lstType4 As ComboBox
    Friend WithEvents gbActions As GroupBox
    Friend WithEvents lblActivePlayer As Label
    Friend WithEvents btnAction5 As Button
    Friend WithEvents btnAction4 As Button
    Friend WithEvents btnAction3 As Button
    Friend WithEvents btnAction2 As Button
    Friend WithEvents btnAction1 As Button
    Friend WithEvents lblActivePlayerDesc As Label
    Friend WithEvents gbGame As GroupBox
    Friend WithEvents txtGameBoardFile As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnStartGame As Button
    Friend WithEvents txtDice1 As TextBox
    Friend WithEvents txtDice0 As TextBox
    Friend WithEvents lblActionText As Label
    Friend WithEvents btnColor4 As Button
    Friend WithEvents btnColor3 As Button
    Friend WithEvents btnColor2 As Button
    Friend WithEvents btnColor1 As Button
    Friend WithEvents txtDice2 As TextBox
End Class
