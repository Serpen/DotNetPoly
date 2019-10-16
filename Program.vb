'
' Created by SharpDevelop.
' User: serpe
' Date: 22.11.2015 12:17
'
Module Program
	Sub Main()
        Dim gf As GameBoard = GameBoard.CreateSampleBoard()

        AddHandler gf.Players(0).BuyOffer, AddressOf buyoffer
        AddHandler gf.Players(0).Dice, AddressOf Dicing

        Do Until gf.GameOver
            gf.RollDice()
        Loop

		Console.Write("Press any key to continue . . . ")
		Console.ReadKey(True)
    End Sub

    Sub buyoffer(sender As Player, pField As StreetField, e As ComponentModel.CancelEventArgs)
        Console.Write("Wollen Sie {0} für {1} kaufen?", pField.Name, pField.Cost)
        Dim answer = Console.ReadLine()
        If answer = "j" Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Sub Dicing(sender As Player, pMoves As Integer, e As EventArgs)
        Console.WriteLine("Spieler '{0}' würfelt eine '{1}'", sender, pMoves)
        Console.ReadKey(True)
    End Sub
End Module
