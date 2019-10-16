Imports DotNetPoly

Module Program
    Private gf As GameBoard
    Private botlogic As PlayerAI

    Sub Main()
        gf = GameBoard.CreateSampleBoard()

        For Each p In gf.Players
            If p.Type = ePlayerType.User Then
                AddHandler p.BuyOffer, AddressOf buyoffer
                AddHandler p.ActivatePlayer, AddressOf ActivatePlayer
                AddHandler p.Bankrupt, AddressOf Bankrupt
            End If

        Next

        Do Until gf.GameOver

            gf.ActivePlayer.onActivate()
        Loop

        Console.WriteLine("Spiel ist vorbei, gewonnen hat: '{0}'", gf.ActivePlayer)

        Console.Write("Press any key to continue . . . ")
        Console.ReadKey(True)
    End Sub

    Sub buyoffer(sender As Player, pField As StreetField, e As ComponentModel.CancelEventArgs)
        Console.Write("Will Spieler '{0}' {1} für {2} kaufen?", sender, pField, pField.Cost)
        Dim answer = Console.ReadLine()
        If answer = "j" Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Sub ActivatePlayer(sender As Player)
        Dim sum = sender.RollDice()
        Console.WriteLine("Spieler '{0}' würfelt {1} ", sender, String.Join(",", sum))
        sender.PerformNextAction()
    End Sub

    Sub Bankrupt(sender As Player, e As EventArgs)
        Dim pre = Console.ForegroundColor
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine("Spieler '{0}' ist bankrott", sender)
        Console.ForegroundColor = pre
    End Sub
End Module
