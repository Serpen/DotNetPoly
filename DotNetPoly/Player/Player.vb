Imports DotNetPoly

Public Class Player
    Sub New(pName As String, pColor As ConsoleColor)
        Name = pName
        Color = pColor
    End Sub

#Region "Properties"

    Public ReadOnly Property Name As String

    Public ReadOnly Property Color As ConsoleColor

    Public ReadOnly Property Cash As Integer

    Public Property Status As ePlayerStatus

    Friend Property GameBoard As GameBoard

    Friend Property Position As Integer
    Public ReadOnly Property onField As Field
        Get
            Return GameBoard.Fields(Position)
        End Get
    End Property


#End Region

    Friend Sub onPayRent(pStreetField As StreetField)
        Me.TransferMoney(pStreetField.Owner, pStreetField.Rent)
        RaiseEvent PayRent(pStreetField, pStreetField.Owner, pStreetField.Rent)
    End Sub

    Friend Sub TransferMoney(ptoPlayer As Player, pAmount As Integer)
        Dim transfer As Integer

        If Me.Equals(BANK) Then
            'Banktransfer 
            transfer = pAmount
            ptoPlayer._cash += transfer
            Me._cash -= transfer
            RaiseEvent MoneyTransfered(Me, ptoPlayer, transfer)
        ElseIf Me.Cash < pAmount Then
            transfer = Me.Cash
            ptoPlayer._cash += transfer
            Me._cash -= transfer
            RaiseEvent MoneyTransfered(Me, ptoPlayer, transfer)
            Me.onBankrupt()
        Else
            transfer = pAmount
            ptoPlayer._cash += transfer
            Me._cash -= transfer
            RaiseEvent MoneyTransfered(Me, ptoPlayer, transfer)
        End If

    End Sub

    Private Sub onBankrupt()
        Me.Status = ePlayerStatus.Bankrupt
        RaiseEvent Bankrupt(Me)

        GameBoard.checkGameOver()
    End Sub

    Friend Sub onMoveOn(pField As Field)
        RaiseEvent MoveOn(Me, pField)
    End Sub

    Friend Sub onMoveOver(pField As Field)
        RaiseEvent MoveOver(Me, pField)
    End Sub

    Friend Function onRollDice() As PlayerActionResult
        Dim diceRoll As New DiceRollResult(Me, GameBoard.Settings.DICE_COUNT, GameBoard.Settings.DICE_EYES)
        RaiseEvent RollDice(Me, diceRoll)
        Return New PlayerActionResult(Me, ePlayerAction.DiceOnly, CType(Microsoft.VisualBasic.IIf(diceRoll.IsPash, eTurnModifier.Additional, eTurnModifier.Unchanged), eTurnModifier), diceRoll)
    End Function

    Friend Function onDelegateControl(pPossibleActions() As ePlayerAction, pfields() As Field, defaultAction As ePlayerAction) As PlayerActionResult
        Dim actResult As PlayerActionResult
        Dim choosenAction = defaultAction

        RaiseEvent DelegateControl(Me, pPossibleActions, pfields, choosenAction)
        Select Case choosenAction
            Case ePlayerAction.DiceOnly
                actResult = onRollDice()
                Status = ePlayerStatus.Waiting
                Return actResult
            Case ePlayerAction.Move
                actResult = onMove()
                If actResult.TurnModifier < eTurnModifier.Additional And Status <> ePlayerStatus.Bankrupt Then
                    Status = ePlayerStatus.Waiting
                End If
                Return actResult
            Case ePlayerAction.BuyStreet
                CType(pfields(0), StreetField).BuyField(Me)

        End Select
    End Function

    Friend Function onMove() As PlayerActionResult
        Dim diceRoll As New DiceRollResult(Me, GameBoard.Settings.DICE_COUNT, GameBoard.Settings.DICE_EYES)
        RaiseEvent RollDice(Me, diceRoll)
        For i = 1 To diceRoll.DiceSum - 1
            Position = (Position + 1) Mod (GameBoard.Fields.GetLength(0))
            GameBoard.Fields(Position).onMoveOver(Me)
        Next
        Position = (Position + 1) Mod (GameBoard.Fields.GetLength(0))
        onField.onMoveOn(Me)
        Return New PlayerActionResult(Me, ePlayerAction.Move, CType(Microsoft.VisualBasic.IIf(diceRoll.IsPash, eTurnModifier.Additional, eTurnModifier.Unchanged), eTurnModifier), Me.onField)
    End Function

    Public Overrides Function ToString() As String
#If DEBUG Then
        Return Me.Name & " (" & Math.Abs(Me.Cash) & ")"
#Else
        Return Me.Name
#End If

    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If obj.GetType() Is GetType(Player) Then
            If CType(obj, Player).Name = Me.Name Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

#Region "Events"

    Event Bankrupt(sender As Player)

    Event DelegateControl(pPlayer As Player, pPossibleActions() As ePlayerAction, pFields As Field(), ByRef pChoosenAction As ePlayerAction)

    Event RollDice(sender As Player, pDiceRollResult As DiceRollResult)

    Event MoneyTransfered(pFromPlayer As Player, pToPlayer As Player, pAmount As Integer)

    Event MoveOn(pPlayer As Player, pField As Field)
    Event MoveOver(pPlayer As Player, pField As Field)

    Event PayRent(pfield As Field, pPlayer As Player, pCost As Integer)

#End Region

#Region "Shared"

    Public Shared ReadOnly Property BANK As New Player("BANK", ConsoleColor.White)


#End Region

End Class

Public Enum ePlayerStatus
    Active = 1
    Playing = 2
    Waiting = 3

    Bankrupt = 10

End Enum

Public Enum ePlayerAction
    Pass
    DiceOnly
    Move
    BuyStreet
    FieldInfo
End Enum
