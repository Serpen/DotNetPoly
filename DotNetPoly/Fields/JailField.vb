Namespace Fields
    Public Class JailField
        Inherits BaseField

        Dim _actionType As eActionType

        Sub New(pName As String, pActionType As eActionType)
            MyBase.New(pName)
            _actionType = pActionType
        End Sub

        Friend Sub GoToJail(pPlayer As BasePlayer)
            pPlayer.Position = Index
            For i = 1 To GameBoard.Settings.JAIL_TIMES
                pPlayer.NextAction.Enqueue(eActionType.WaitingForTurn)
                Select Case GameBoard.Settings.JAIL_ACTION
                    Case Else
                        pPlayer.NextAction.Enqueue(eActionType.LoseTurn)
                End Select

            Next
        End Sub

        Friend Overrides Sub onMoveOn(pPlayer As BasePlayer)
            MyBase.onMoveOn(pPlayer)

            Select Case _actionType
                Case eActionType.None
                    'Nothing to Do
                Case eActionType.JumpToJail
                    pPlayer.onDelegateControl(eActionType.JumpToJail)
            End Select
        End Sub


    End Class

End Namespace


