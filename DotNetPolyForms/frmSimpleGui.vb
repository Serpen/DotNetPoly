Imports DotNetPoly

Public Class frmSimpleGui

    Private oSignalEvent As Threading.ManualResetEvent = New Threading.ManualResetEvent(False)

    Private WithEvents gb As DotNetPoly.GameBoard

    Private thGame As New Threading.Thread(AddressOf GameThread)

    Private fieldsize As Size
    'Private cornersize As Size

    Private pBox() As PictureBox
    Private fieldLabels() As Label


    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Try
            txtGameBoardFile.Text = Environment.GetCommandLineArgs(1)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnStartGame_Click(sender As Object, e As EventArgs) Handles btnStartGame.Click
        Form.CheckForIllegalCrossThreadCalls = False 'HACK: FixThreading Delegates
        Dim players As New Collections.Generic.List(Of DotNetPoly.BasePlayer)
        For i = 1 To 4
            Select Case TableLayoutPanel1.Controls.Find("lstType" & i, False)(0).Text
                Case "Human"
                    players.Add(New DotNetPoly.HumanPlayer(TableLayoutPanel1.Controls.Find("txtPlayer" & i, True)(0).Text, TableLayoutPanel1.Controls.Find("btnColor" & i, True)(0).BackColor))
                Case "Bot"
                    players.Add(New DotNetPoly.BotPlayer(TableLayoutPanel1.Controls.Find("txtPlayer" & i, True)(0).Text, TableLayoutPanel1.Controls.Find("btnColor" & i, True)(0).BackColor))
                Case "(none)"
            End Select
        Next

        gb = New GameBoard(Me.txtGameBoardFile.Text, players.ToArray())

        generateMap()

        For i = 0 To gb.Settings.DICE_COUNT - 1
            SplitContainer1.Panel2.Controls.Find($"txtDice{i}", True)(0).Visible = True
        Next

        For Each p In gb.PlayerRank

            If p.GetType() Is GetType(HumanPlayer) Then
                AddHandler CType(p, HumanPlayer).DelegateControl, AddressOf DelegateControl
            End If

            AddHandler p.RollDice, AddressOf RollDice
            AddHandler p.Bankrupt, AddressOf Bankrupt
            AddHandler p.MoneyTransfered, AddressOf MoneyTransfered
            AddHandler p.MoveOn, AddressOf MoveOn
            AddHandler p.MoveOver, AddressOf MoveOver
            AddHandler p.Jail, AddressOf Jail
            AddHandler p.Chance, AddressOf Chance
            AddHandler p.DoublesToMuch, AddressOf DoublesToMuch
            AddHandler p.PayRent, AddressOf PayRent
            AddHandler p.UpgradeHouse, AddressOf UpgradeHouse
        Next
        AddHandler gb.BANK.MoneyTransfered, AddressOf MoneyTransfered

        btnStartGame.Enabled = False
        thGame.Start()
    End Sub

    Sub GameThread()
        gb.Initialize()

        For Each p In gb.PlayerRank
            TableLayoutPanel1.Controls.Find($"txtPlayer{p.Index + 1}", True)(0).Text = p.Name
            TableLayoutPanel1.Controls.Find($"txtCash{p.Index + 1}", True)(0).Text = p.Cash
            TableLayoutPanel1.Controls.Find($"btnColor{p.Index + 1}", True)(0).BackColor = p.Color
            If p.GetType() Is GetType(HumanPlayer) Then
                TableLayoutPanel1.Controls.Find($"lstType{p.Index + 1}", True)(0).Text = "Human"
            Else
                TableLayoutPanel1.Controls.Find($"lstType{p.Index + 1}", True)(0).Text = "Bot"
            End If

            pBox(p.Index).BackColor = p.Color
            pBox(p.Index).Tag = p.Index
        Next

        Do Until gb.IsGameOver
            gb.NextPlayersTurn()
        Loop

    End Sub

    Sub generateMap()
        SplitContainer1.Panel1.Controls.Clear()

        ReDim fieldLabels(gb.FieldCount - 1)
        ReDim pBox(gb.PlayerRank.Length - 1)

        Dim counterField As Integer = 0

        If gb.FieldCount Mod 4 <> 0 Then
            Throw New NotSupportedException("Gameboard has wrong field count")
        End If

        Dim fieldsPerSide As Integer = gb.FieldCount / 4 + 1
        Dim shortestSide As Integer
        If Me.SplitContainer1.Panel1.Width > Me.SplitContainer1.Panel1.Height Then
            shortestSide = Me.SplitContainer1.Panel1.Height - 10
        Else
            shortestSide = Me.SplitContainer1.Panel1.Width - 10
        End If
        fieldsize = New Size(shortestSide / fieldsPerSide, shortestSide / fieldsPerSide)

        For x = 0 To fieldsPerSide - 1
            Dim l As New Label()
            l.AutoSize = False
            l.BorderStyle = BorderStyle.FixedSingle
            l.Size = fieldsize
            l.TextAlign = ContentAlignment.MiddleCenter

            l.Top = 0
            l.Left = x * fieldsize.Width
            l.Text = gb.Fields(x).ToString & " " & x

            SplitContainer1.Panel1.Controls.Add(l)

            fieldLabels(counterField) = l
            counterField += 1
        Next

        For y = 1 To fieldsPerSide - 1
            Dim l As New Label()
            l.AutoSize = False
            l.BorderStyle = BorderStyle.FixedSingle
            l.Size = fieldsize
            l.TextAlign = ContentAlignment.MiddleCenter

            l.Top = y * fieldsize.Width
            l.Left = fieldsize.Width * (fieldsPerSide - 1)
            l.Text = gb.Fields(y + fieldsPerSide - 1).ToString & " " & y + fieldsPerSide - 1

            SplitContainer1.Panel1.Controls.Add(l)

            fieldLabels(counterField) = l
            counterField += 1
        Next

        For x = fieldsPerSide - 1 To 1 Step -1
            Dim l As New Label()
            l.AutoSize = False
            l.BorderStyle = BorderStyle.FixedSingle
            l.Size = fieldsize
            l.TextAlign = ContentAlignment.MiddleCenter

            l.Top = fieldsize.Height * (fieldsPerSide - 1)
            l.Left = ((x - 1) * fieldsize.Width)
            l.Text = gb.Fields((fieldsPerSide - x) + fieldsPerSide * 2 - 2).ToString & " " & (fieldsPerSide - x) + fieldsPerSide * 2 - 2

            SplitContainer1.Panel1.Controls.Add(l)

            fieldLabels(counterField) = l
            counterField += 1
        Next

        For y = fieldsPerSide - 1 To 2 Step -1
            Dim l As New Label()
            l.AutoSize = False
            l.BorderStyle = BorderStyle.FixedSingle
            l.Size = fieldsize
            l.TextAlign = ContentAlignment.MiddleCenter

            l.Top = ((y - 1) * fieldsize.Height)
            l.Left = 0
            l.Text = gb.Fields(gb.FieldCount - y + 1).ToString & " " & gb.FieldCount - y + 1

            SplitContainer1.Panel1.Controls.Add(l)

            fieldLabels(counterField) = l
            counterField += 1
        Next

        Dim pcounter As Integer 'Player Index not initialized
        For Each p In gb.PlayerRank
            pBox(pcounter) = New PictureBox
            pBox(pcounter).BorderStyle = BorderStyle.FixedSingle
            pBox(pcounter).Size = New Size(10, 10)

            SplitContainer1.Panel1.Controls.Add(pBox(pcounter))
            pcounter += 1
        Next

    End Sub

    Sub SetPlayerLocation(pField As Integer, pPlayer As BasePlayer)
        Dim x, y As Integer

        x = fieldLabels(pField).Location.X + 10 + pPlayer.Index * 11
        y = fieldLabels(pField).Location.Y + 10

        pBox(pPlayer.Index).Location = New Point(x, y)
        pBox(pPlayer.Index).BringToFront()

    End Sub

#Region "Handlers "


    Sub DelegateControl(pPlayer As BasePlayer, pPossibleActions() As eActionType, pFields As Fields.BaseField(), pPlayers() As Entity, pCash As Integer)
        Dim counter As Integer = 1

        For Each btn As Control In gbActions.Controls
            If btn.GetType() Is GetType(Button) Then
                btn.Visible = False
            End If
        Next

        For Each a In pPossibleActions
            Dim btnAction As Button = gbActions.Controls.Find("btnAction" & counter, False)(0)
            Select Case a
                Case eActionType.BuyHouse
                    btnAction.Text = String.Format("{0} - {1} ({2})", counter, a.ToString(), pFields(0))
                    btnAction.Tag = counter
                    btnAction.Visible = True
                Case eActionType.PayRent
                    btnAction.Text = String.Format("{0} - {1} {2} für ({3})", counter, a.ToString(), DirectCast(pFields(0), Fields.HouseField).Rent, pFields(0))
                    btnAction.Tag = counter
                    btnAction.Visible = True
                Case eActionType.UpgradeHouse
                    btnAction.Text = String.Format("{0} - {1} {2} für ({3})", counter, a.ToString(), pFields(0), DirectCast(pFields(0), Fields.HouseField).UpgradeCost)
                    btnAction.Tag = counter
                    btnAction.Visible = True
                Case eActionType.CashFromBank, eActionType.CashFromPlayer, eActionType.CollectStack
                    btnAction.Text = String.Format("{0} - {1} {2} von ({3})", counter, a.ToString(), pCash, pPlayers(0))
                    btnAction.Tag = counter
                    btnAction.Visible = True
                Case eActionType.CashToBank, eActionType.CashToPlayer, eActionType.PayToStack
                    btnAction.Text = String.Format("{0} - {1} {2} an ({3})", counter, a.ToString(), pCash, pPlayers(0))
                    btnAction.Tag = counter
                    btnAction.Visible = True
                Case Else
                    btnAction.Text = counter & " - " & a.ToString
                    btnAction.Tag = counter
                    btnAction.Visible = True
            End Select

            counter += 1
        Next

    End Sub

    Sub Bankrupt(sender As BasePlayer)
        TableLayoutPanel1.Controls.Find("txtPlayer" & sender.Index + 1, False)(0).Enabled = False
        TableLayoutPanel1.Controls.Find("txtCash" & sender.Index + 1, False)(0).Enabled = False
        TableLayoutPanel1.Controls.Find("lstType" & sender.Index + 1, False)(0).Enabled = False
        TableLayoutPanel1.Controls.Find("lstType" & sender.Index + 1, False)(0).Text = "Bankrott"
        MessageBox.Show(Me, String.Format("Spieler '{0}' ist bankrott", sender), $"Bankrott Spieler {sender}", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

    End Sub

    Sub RollDice(sender As BasePlayer, pDiceRollResult As DiceRollResult)
        For i = 0 To pDiceRollResult.Dices.Length - 1
            Controls.Find("txtDice" & i, True)(0).Text = pDiceRollResult.Dices(i)
        Next
    End Sub

    Sub MoneyTransfered(pFromPlayer As Entity, pToPlayer As Entity, pAmount As Integer)
        lblActionText.Text = String.Format("Spieler '{0}' gibt Spieler '{1}' '{2}'", pFromPlayer, pToPlayer, pAmount)
        If pFromPlayer.GetType().IsSubclassOf(GetType(BasePlayer)) Then
            TableLayoutPanel1.Controls.Find("txtCash" & CType(pFromPlayer, BasePlayer).Index + 1, False)(0).Text = pFromPlayer.Cash
        End If
        If pToPlayer.GetType().IsSubclassOf(GetType(BasePlayer)) Then
            TableLayoutPanel1.Controls.Find("txtCash" & CType(pToPlayer, BasePlayer).Index + 1, False)(0).Text = pToPlayer.Cash
        End If

    End Sub

    Sub MoveOn(pPlayer As BasePlayer, pField As Fields.BaseField)
        SetPlayerLocation(pField.Index, pPlayer)

        'MessageBox.Show(String.Format("Spieler '{0}' zieht auf {1}", pPlayer, pField), $"Los Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Sub MoveOver(pPlayer As BasePlayer, pField As Fields.BaseField)
        If pField.GetType() = GetType(Fields.Startfield) Then
            MessageBox.Show(Me, String.Format("Spieler '{0}' zieht über {1}", pPlayer, pField), $"Los Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub PlayerRankChanged(pPlayerRank() As BasePlayer) Handles gb.PlayerRankChanged
        MessageBox.Show(Me, String.Format("Spielerreihenfolge ist: {0}", String.Join(",", CType(pPlayerRank, Object()))), "Spielerreihenfolge", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Jail(pPlayer As BasePlayer, pPrisonEvent As eActionType)
        If pPrisonEvent = eActionType.JumpToJail Then
            MessageBox.Show(Me, String.Format("Spieler {0} geht in {1}", pPlayer, "Jail"), $"Event Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SetPlayerLocation(pPlayer.Position, pPlayer)
        ElseIf pPrisonEvent = eActionType.LoseTurn Then
            MessageBox.Show(Me, String.Format("Spieler {0} setzt eine Runde aus", pPlayer), $"Event Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Sub Chance(pPlayer As BasePlayer, pEvent As eActionType, pObject() As Object)
        MessageBox.Show(Me, $"Spieler {pPlayer} löst Event {pEvent.ToString()} mit {String.Join(", ", pObject)} aus", $"Event Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Sub FieldOwnerChange(pField As Fields.HouseField, pOwner As BasePlayer) Handles gb.FieldOwnerChange
        MessageBox.Show(Me, $"Spieler {pOwner} erwirbt {pField}", $"Kaufen Spieler {pOwner}", MessageBoxButtons.OK, MessageBoxIcon.Information)
        fieldLabels(pField.Index).BackColor = pOwner.Color
    End Sub

    Sub UpgradeHouse(pPlayer As BasePlayer, pHouse As Fields.HouseField)
        MessageBox.Show(Me, $"Spieler {pPlayer} baut das Haus {pHouse} auf Stufe {pHouse.UpgradeLevel + 1} aus", $"Ausbau Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Sub DoublesToMuch(pPlayer As BasePlayer)
        MessageBox.Show(Me, $"Spieler {pPlayer} hat einen Pash zuviel und muss in {gb.JailField} ", $"Aussetzen Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    Sub PayRent(pfield As Fields.BaseField, pPlayer As Entity, pCost As Integer)
        MessageBox.Show(Me, $"Spieler {pPlayer} zahlt {pCost} für  {pfield} ", $"Miete zahlen Spieler {pPlayer}", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    Private Sub gb_GameOver() Handles gb.GameOver
        For Each btn As Control In gbActions.Controls
            If btn.GetType() Is GetType(Button) Then
                btn.Visible = False
            End If
        Next
        lblActivePlayer.Text = gb.ActivePlayer.ToString
        MessageBox.Show(Me, $"Spieler {gb.ActivePlayer} hat gewonnen", $"Sieg Spieler {gb.ActivePlayer}", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

    End Sub

#End Region

    Private Sub btnAction_Click(sender As Object, e As EventArgs) Handles btnAction1.Click, btnAction2.Click, btnAction3.Click, btnAction4.Click, btnAction5.Click
        gb.ActivePlayer.SubmitChoosenAction(sender.tag, Nothing)
    End Sub

    Private Sub gb_NextPlayer(pPlayer As BasePlayer) Handles gb.NextPlayer
        Me.lblActivePlayer.Text = pPlayer.ToString
    End Sub

    Private Sub frmSimpleGui_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        Try
            thGame.Abort()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnColor_Click(sender As Object, e As EventArgs) Handles btnColor1.Click, btnColor2.Click, btnColor3.Click, btnColor4.Click
        Dim colorChooser As New Windows.Forms.ColorDialog()
        colorChooser.Color = DirectCast(sender, Button).BackColor
        If colorChooser.ShowDialog() = DialogResult.OK Then
            DirectCast(sender, Button).BackColor = colorChooser.Color
        End If
    End Sub

End Class
