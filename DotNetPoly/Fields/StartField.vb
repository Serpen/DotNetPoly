Namespace Fields

    Public Class Startfield
        Inherits BaseField

        Sub New(pStartMoney As Integer, pName As String)
            MyBase.New(pName)
            _StartCash = pStartMoney
        End Sub

        Public ReadOnly Property StartCash As Integer

        Friend Overrides Sub onMoveOn(pPlayer As BasePlayer)
            MyBase.onMoveOn(pPlayer)
            GameBoard.BANK.TransferMoney(pPlayer, StartCash * 2)
        End Sub

        Friend Overrides Sub onMoveOver(pPlayer As BasePlayer)
            MyBase.onMoveOver(pPlayer)
            GameBoard.BANK.TransferMoney(pPlayer, StartCash)
        End Sub
    End Class
End Namespace

