Imports DotNetPoly

Public Class StreetField
    Inherits Field

    Sub New(pName As String, pCost As Integer)
        MyBase.New(pName)
        Cost = pCost
        Rent = pCost
    End Sub

    ReadOnly Property Cost As Integer
    ReadOnly Property Rent As Integer


    Friend Sub BuyField(pPlayer As Player)
        pPlayer.TransferMoney(Me.Owner, Me.Cost)
        Me._owner = pPlayer
        Me.GameBoard.onChangeOwner(Me, pPlayer)
    End Sub

    Friend Overrides Function onMoveOn(pPlayer As Player) As PlayerActionResult
        Dim erg = MyBase.onMoveOn(pPlayer)
        If Me.Owner.Equals(Player.BANK) Then
            Return pPlayer.onDelegateControl(New ePlayerAction() {ePlayerAction.BuyStreet, ePlayerAction.Pass}, New Field() {Me}, ePlayerAction.Pass)
        ElseIf Me.Owner.Equals(pPlayer) Then

        Else
            pPlayer.onPayRent(Me)
        End If
    End Function

    Friend Overridable Sub ChangeOwner(pPlayer As Player)
        Me._owner = pPlayer
    End Sub
End Class
