Public Class PlayerActionResult
    Sub New(pPlayer As Player, pAction As ePlayerAction, pTurnModifier As eTurnModifier, pResult As Object)
        Player = pPlayer
        Action = pAction
        TurnModifier = pTurnModifier
        Result = pResult
    End Sub

    ReadOnly Property Player As Player
    ReadOnly Property Action As ePlayerAction
    Public ReadOnly Property TurnModifier As eTurnModifier
    Public ReadOnly Result As Object
End Class

Public Enum eTurnModifier As Integer
    Unchanged
    [End]

    Additional
End Enum