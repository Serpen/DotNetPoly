Imports DotNetPoly

Public Class Startfield
    Inherits Field

    Sub New(pStartMoney As Integer)
        MyBase.New("Start")
        _StartCash = pStartMoney
    End Sub

    Public ReadOnly Property StartCash As Integer

    Friend Overrides Function onMoveOn(pPlayer As Player) As PlayerActionResult
        Dim erg = MyBase.onMoveOn(pPlayer)
        Player.BANK.TransferMoney(pPlayer, StartCash * 2)
        Return erg
    End Function

    Friend Overrides Sub onMoveOver(pPlayer As Player)
        MyBase.onMoveOver(pPlayer)
        Player.BANK.TransferMoney(pPlayer, StartCash)
    End Sub
End Class
