Public Class PlayerAI

    Private ReadOnly _player As Player
    Private ReadOnly _board As GameBoard
    Private ReadOnly _AI As eAI

    <Flags()>
    Public Enum eAI As Integer
        BuyWhenPossible
        BuyWhen2Possible
    End Enum

    Friend Sub New(pPlayer As Player, pBoard As GameBoard, pAI As eAI)
        _player = pPlayer
        _board = pBoard
        _AI = pAI
    End Sub

    Sub BuyOfferHandler(sender As Player, pField As StreetField, e As ComponentModel.CancelEventArgs)
        If _player.Cash >= pField.Cost And _AI = eAI.BuyWhenPossible Then
            e.Cancel = False
        ElseIf _player.Cash >= pField.Cost * 2 And _AI = eAI.BuyWhen2Possible Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Sub DicingHandler(sender As Player)
        Dim sum = sender.RollDice()
        Console.WriteLine("Spieler '{0}' würfelt {1} ", sender, String.Join(",", sum))
        sender.PerformNextAction()
    End Sub

    Sub BankruptHandler(sender As Player, e As EventArgs)
        Dim pre = Console.ForegroundColor
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine("Spieler '{0}' ist bankrott", sender)
        Console.ForegroundColor = pre
    End Sub

End Class
