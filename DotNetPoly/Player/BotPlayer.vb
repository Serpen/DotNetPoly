Public Class BotPlayer
    Inherits BasePlayer

    Sub New(pName As String, pColor As System.Drawing.Color)
        MyBase.New(pName, pColor)
    End Sub

    Overrides Function DelegateDiceRoll() As DiceRollResult
        Return onRollDice()
    End Function

    Friend Overrides Sub onDelegateControl(pPossibleActions() As eActionType, pfields() As Fields.BaseField, pPlayers() As Entity, pCash As Integer)
        Dim choosenAction = 0

        For i = 0 To pPossibleActions.Length - 1
            If pPossibleActions(i) = eActionType.BuyHouse Then
                If Me.Cash > DirectCast(pfields(0), Fields.HouseField).Cost Then
                    choosenAction = i
                    Exit For
                Else
                    choosenAction = 1
                End If
            End If
        Next

        invokeAction(pPossibleActions(choosenAction), pfields, pPlayers, pCash)

    End Sub

    Overrides Sub SubmitChoosenAction(pIndex As Integer, pObject As Object)
        Throw New NotSupportedException()
    End Sub

End Class
