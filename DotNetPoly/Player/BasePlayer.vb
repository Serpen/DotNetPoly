Imports DotNetPoly.Fields

Public MustInherit Class BasePlayer
    Inherits Entity

    Protected WaitChoosenAction As Threading.ManualResetEvent = New Threading.ManualResetEvent(False)
    Protected choosenActionIndex As Integer
    Private _isPlaying As Boolean
    Private _index As Integer

    Sub New(pName As String, pColor As System.Drawing.Color)
        MyBase.New(pName, pColor)
    End Sub

    Friend Sub Initialize(pGameBoard As GameBoard, pIndex As Integer)
        GameBoard = pGameBoard
        GameBoard.BANK.TransferMoney(Me, pGameBoard.Settings.START_CAPITAL)
        _position = 0
        _isPlaying = True
        _index = pIndex
        'NextAction.Enqueue(ePlayerAction.DiceOnly)
        NextAction.Enqueue(eActionType.WaitingForTurn)
        NextAction.Enqueue(eActionType.Move)
    End Sub

#Region "Properties"

    Friend Property GameBoard As GameBoard

    Private _position As Integer

    Property Position As Integer
        Get
            Return _position
        End Get
        Friend Set(value As Integer)
            _position = value
        End Set
    End Property

    ReadOnly Property PositionField As BaseField
        Get
            Return GameBoard.Fields(_Position)
        End Get
    End Property

    Friend Sub RaiseEventHouseUpgrade(houseField As HouseField)
        RaiseEvent UpgradeHouse(Me, houseField)
    End Sub

    Friend Property DoublesCount As Integer

    Friend ReadOnly Property IsPlayering As Boolean
        Get
            Return _isPlaying
        End Get
    End Property


    Friend Property NextAction As New Collections.Generic.Queue(Of eActionType)

    Public Property Index As Integer
        Get
            Return _index
        End Get
        Friend Set(value As Integer)
            _index = value
        End Set
    End Property

#End Region


    Friend Sub RaisePayRent(pStreetField As HouseField)
        Me.TransferMoney(pStreetField.Owner, pStreetField.Rent)
        If Me.IsPlayering Then
            RaiseEvent PayRent(pStreetField, pStreetField.Owner, pStreetField.Rent)
        End If

    End Sub

    Protected Friend Sub onBankrupt()
        Me._isPlaying = False
        NextAction.Clear()

        'Transfer cash back to BANK (when manually raised)
        If Cash > 0 Then
            TransferMoney(GameBoard.BANK, Cash)
        End If
        RaiseEvent Bankrupt(Me)

        'Give all Fields back to BANK
        Dim lst = OwnFields
        For Each f In lst
            DirectCast(f, Fields.HouseField).ChangeOwner(GameBoard.BANK)
        Next

        GameBoard.checkGameOver()
    End Sub

    Friend Function onRollDice() As DiceRollResult
        Dim diceRoll As New DiceRollResult(GameBoard.Settings.DICE_COUNT, GameBoard.Settings.DICE_EYES)

        GameBoard.Statistics.TotalDiceCount += 1
        GameBoard.Statistics.DiceRollSums(diceRoll.DiceSum) += 1
        If diceRoll.IsDoubles Then GameBoard.Statistics.DoublesCount += 1

        RaiseEvent RollDice(Me, diceRoll)

        Return diceRoll
    End Function

    MustOverride Function DelegateDiceRoll() As DiceRollResult

    Friend Sub onDelegateControl(pPossibleAction As eActionType)
        onDelegateControl(New eActionType() {pPossibleAction}, Nothing, Nothing, 0)
    End Sub
    Friend Sub onDelegateControl(pPossibleAction As eActionType, pCash As Integer)
        onDelegateControl(New eActionType() {pPossibleAction}, Nothing, Nothing, pCash)
    End Sub
    Friend Sub onDelegateControl(pPossibleAction As eActionType, pPlayer As Entity, pCash As Integer)
        onDelegateControl(New eActionType() {pPossibleAction}, Nothing, New Entity() {pPlayer}, pCash)
    End Sub
    Friend Sub onDelegateControl(pPossibleAction As eActionType, pfield As BaseField)
        onDelegateControl(New eActionType() {pPossibleAction}, New BaseField() {pfield}, Nothing, 0)
    End Sub
    Friend Sub onDelegateControl(pPossibleAction As eActionType, pfield As BaseField, pCash As Integer)
        onDelegateControl(New eActionType() {pPossibleAction}, New BaseField() {pfield}, Nothing, pCash)
    End Sub

    ''' <summary>
    ''' Gives control to Player and allows choosing action
    ''' </summary>
    ''' <param name="pPossibleActions">List of possible actions</param>
    ''' <param name="pfields">contextual fields</param>
    ''' <param name="pPlayers">contextual fields</param>
    ''' <param name="pCash">contextual cash</param>
    ''' <param name="defaultActionIndex">Default action</param>
    ''' <returns></returns>
    Friend MustOverride Sub onDelegateControl(pPossibleActions() As eActionType, pfields() As BaseField, pPlayers() As Entity, pCash As Integer)

    MustOverride Sub SubmitChoosenAction(pIndex As Integer, pObject As Object)

    Protected Sub invokeAction(pChoosenAction As eActionType, pfields() As BaseField, pPlayers() As Entity, pCash As Integer)
        GameBoard.Statistics.ActionChoosen(pChoosenAction) = CInt(GameBoard.Statistics.ActionChoosen(pChoosenAction)) + 1

        Select Case pChoosenAction
            Case eActionType.Move
                onMove()
            Case eActionType.BuyHouse
                DirectCast(pfields(0), HouseField).Buy(Me)
            Case eActionType.PayRent
                RaisePayRent(DirectCast(pfields(0), HouseField))
            Case eActionType.GiveUp
                onBankrupt()
            Case eActionType.UpgradeHouse
                DirectCast(pfields(0), HouseField).Upgrade()
            Case eActionType.LoseTurn
                RaiseJail(eActionType.LoseTurn)
            Case eActionType.GoToField
                MoveTo(pfields(0), False)
            Case eActionType.JumpToField, eActionType.JumpToRelativField
                MoveTo(pfields(0), True)
            Case eActionType.JumpToJail
                DirectCast(GameBoard.JailField, Fields.JailField).GoToJail(Me)
                RaiseJail(eActionType.JumpToJail)
            Case eActionType.PayToStack
                TransferMoney(GameBoard.Wundertuete, pCash)
            Case eActionType.CollectStack
                GameBoard.Wundertuete.TransferMoney(Me, pCash)
            Case eActionType.CashToBank
                TransferMoney(GameBoard.BANK, pCash)
            Case eActionType.CashFromBank
                GameBoard.BANK.TransferMoney(Me, pCash)
            Case eActionType.CashToPlayer
                TransferMoney(pPlayers(0), pCash)
            Case eActionType.CashFromPlayer
                pPlayers(0).TransferMoney(Me, pCash)
            Case eActionType.CashFromAll
                For Each p In GameBoard.PlayerRank
                    If p.IsPlayering Then
                        p.TransferMoney(Me, pCash)
                    End If
                Next
            Case eActionType.PayPerHouse
                TransferMoney(GameBoard.BANK, pCash)
            Case eActionType.AddMove
                NextAction.Clear()
                NextAction.Enqueue(eActionType.Move)
            Case eActionType.UpgradeHouse
                'TODO: Check correct implemented?
                DirectCast(GameBoard.Fields(pfields(0).Index), HouseField).Upgrade()

            Case eActionType.CardUseFreeParking
                RaiseChance(eActionType.CardUseFreeParking, New Object() {pfields(0)})
                If Not CheatAlwaysRentFree Then
                    GameBoard.FreeParkingOwner = GameBoard.BANK
                End If

            Case eActionType.CardGetFreeParking
                GameBoard.FreeParkingOwner = Me
            Case eActionType.CardUseFreeJail
                RaiseChance(eActionType.CardUseFreeJail, New Object() {GameBoard.JailField})
                If Not CheatAlwaysJailFree Then
                    GameBoard.FreeParkingOwner = GameBoard.BANK
                End If

            Case eActionType.CardGetFreeJail
                GameBoard.FreeJailOwner = Me
            Case eActionType.Pass

            Case Else
                Throw New NotImplementedException(pChoosenAction.ToString())
        End Select
    End Sub

    Friend Sub onMove()
        Dim diceRoll = onRollDice()

        If diceRoll.IsDoubles Then
            DoublesCount += 1
        End If

        'Doubles Limit?
        If DoublesCount >= GameBoard.Settings.MAX_DOUBLES Then
            NextAction.Clear()
            NextAction.Enqueue(eActionType.JumpToJail)
            RaiseDoublesLimit()
        Else
            'Move over every field in path, an move on the last

            'TODO: Should call MoveTo with overload
            For i = 1 To diceRoll.DiceSum - 1
                _Position = (_Position + 1) Mod (GameBoard.FieldCount)
                GameBoard.Fields(_Position).onMoveOver(Me)
            Next
            _Position = (_Position + 1) Mod GameBoard.FieldCount
            PositionField.onMoveOn(Me)

            If Me.IsPlayering AndAlso (diceRoll.IsDoubles) AndAlso Me.Position <> GameBoard.JailField.Index Then
                'Doubles Limit no reached, so add another move
                NextAction.Enqueue(eActionType.Move)
                'TODO: AddMove needs own event
                RaiseChance(eActionType.AddMove, New Object() {diceRoll})
            End If
        End If


    End Sub

    Friend Sub MoveTo(pFields As BaseField, pJump As Boolean)
        If pJump Then
            _Position = pFields.Index
            PositionField.onMoveOn(Me)
        Else
            _Position = (_Position + 1) Mod (GameBoard.FieldCount)
            Do Until PositionField.Equals(pFields)
                PositionField.onMoveOver(Me)
                _Position = (_Position + 1) Mod (GameBoard.FieldCount)
            Loop
            PositionField.onMoveOn(Me)
        End If

    End Sub

#Region "Event Raisers"

    Friend Sub RaiseMoveOn(pField As BaseField)
        RaiseEvent MoveOn(Me, pField)
    End Sub

    Friend Sub RaiseMoveOver(pField As BaseField)
        RaiseEvent MoveOver(Me, pField)
    End Sub

    'TODO: Protected
    Friend Sub RaiseChance(pEvent As eActionType, pObject() As Object)
        RaiseEvent Chance(Me, pEvent, pObject)
    End Sub

    Protected Sub RaiseJail(pEvent As eActionType)
        RaiseEvent Jail(Me, pEvent)
    End Sub

    Protected Sub RaiseDoublesLimit()
        RaiseEvent DoublesToMuch(Me)
    End Sub

#End Region

#Region "Events"

    Event Bankrupt(sender As BasePlayer)

    Event RollDice(sender As BasePlayer, pDiceRollResult As DiceRollResult)

    Event MoveOn(pPlayer As BasePlayer, pField As BaseField)
    Event MoveOver(pPlayer As BasePlayer, pField As BaseField)

    Event PayRent(pfield As BaseField, pPlayer As Entity, pCost As Integer)

    Event Jail(pPlayer As BasePlayer, pPrisonEvent As eActionType)

    Event Chance(pPlayer As BasePlayer, pEvent As eActionType, pObject() As Object)

    Event DoublesToMuch(pPlayer As BasePlayer)

    Event UpgradeHouse(pPlayer As BasePlayer, pHouse As HouseField)

#End Region

#Region "Cheats"

    Public CheatAlwaysJailFree As Boolean = False
    Public CheatAlwaysRentFree As Boolean = False
    Public CheatMoneyFactor As Integer = 1
    'Public CheatAlwaysMaxAllowedDoubles As Boolean = False



#End Region

End Class
