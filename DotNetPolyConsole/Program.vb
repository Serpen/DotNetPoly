Imports DotNetPoly

Module Program
    Private WithEvents gb As GameBoard

    Sub Main()
        Console.ForegroundColor = ConsoleColor.White
        Dim players() As Player = {
            New Player("Marco", ConsoleColor.Red),
            New Player("Malte", ConsoleColor.Cyan)}

        Dim fields = Field.FromFile(".\MonopolyJunior.csv")

        Dim settings = DotNetPoly.Settings.FromFile(".\Settings.csv")

        gb = New GameBoard(players, fields, settings)


        'Set Event Handlers for Players
        For Each p In gb.PlayerRank
            AddHandler p.RollDice, AddressOf RollDice
            AddHandler p.DelegateControl, AddressOf DelegateControl
            AddHandler p.Bankrupt, AddressOf Bankrupt
            AddHandler p.MoneyTransfered, AddressOf MoneyTransfered
            AddHandler p.MoveOn, AddressOf MoveOn
            AddHandler p.MoveOver, AddressOf MoveOver
        Next
        AddHandler Player.BANK.MoneyTransfered, AddressOf MoneyTransfered

        gb.Initialize()

        Do Until gb.IsGameOver
            gb.performPlayerAction()
        Loop

        Console.WriteLine("Spiel ist vorbei, gewonnen hat: '{0}'", gb.ActivePlayer)

        Console.Write("Press any key to continue . . . ")
        Console.ReadKey(True)
    End Sub

    Private Sub WriteLineInPlayerColor(pLine As String, pPlayer As Player, ParamArray format As Object())
        Dim preColor = Console.ForegroundColor
        Console.ForegroundColor = pPlayer.Color
        If Console.BackgroundColor = Console.ForegroundColor Then Console.ForegroundColor = preColor
        Console.WriteLine(pLine, format)
        Console.ForegroundColor = preColor
    End Sub

    Sub DelegateControl(pPlayer As Player, pPossibleActions() As ePlayerAction, pFields As Field(), ByRef pChoosenAction As ePlayerAction)
        Dim counter As Integer = 1
        Dim erg As String

        WriteLineInPlayerColor("Spieler {0} ist am Zug und hat folgende Möglichkeiten:", pPlayer, pPlayer)
        For Each a In pPossibleActions
            Select Case a
                Case ePlayerAction.BuyStreet
                    WriteLineInPlayerColor("{0} - {1} ({2})", pPlayer, counter, a.ToString(), pFields(0))
                Case Else
                    WriteLineInPlayerColor(counter & " - " & a.ToString, pPlayer)
            End Select

            counter += 1
        Next
        erg = Console.ReadLine()
        Try
            pChoosenAction = pPossibleActions(CInt(erg) - 1)
        Catch ex As Exception

        End Try

    End Sub

    Sub Bankrupt(sender As Player)
        Dim pre = Console.BackgroundColor
        Console.BackgroundColor = ConsoleColor.Red
        WriteLineInPlayerColor("Spieler '{0}' ist bankrott", sender, sender)
        Console.BackgroundColor = pre
    End Sub

    Sub RollDice(sender As Player, pDiceRollResult As DiceRollResult)
        WriteLineInPlayerColor("Spieler '{0}' würfelt {1}", sender, sender, pDiceRollResult.DiceSum)
    End Sub

    Sub MoneyTransfered(pFromPlayer As Player, pToPlayer As Player, pAmount As Integer)
        Dim precolor = Console.ForegroundColor
        Console.ForegroundColor = pFromPlayer.Color
        Console.Write("Spieler '{0}' gibt Spieler ", pFromPlayer)
        Console.ForegroundColor = pToPlayer.Color
        Console.Write("'{1}' ", pFromPlayer, pToPlayer, pAmount)
        Console.ForegroundColor = pFromPlayer.Color
        Console.WriteLine(pAmount)
        Console.ForegroundColor = precolor
    End Sub

    Sub MoveOn(pPlayer As Player, pField As Field)
        Dim precolor = Console.ForegroundColor
        Console.ForegroundColor = pPlayer.Color
        Console.Write("Spieler '{0}' zieht auf ", pPlayer, pPlayer, pField)
        Console.ForegroundColor = pField.Owner.Color
        Console.WriteLine(pField)
        Console.ForegroundColor = precolor
    End Sub

    Sub MoveOver(pPlayer As Player, pField As Field)
        If pField.GetType() = GetType(Startfield) Then
            Console.WriteLine("Spieler '{0}' zieht über {1}", pPlayer, pField)
        End If
    End Sub

    Private Sub PlayerRankChanged(pPlayerRank() As Player) Handles gb.PlayerRankChanged
        Console.Write("Spielerreihenfolge ist: ")
        For i = 0 To pPlayerRank.GetLength(0) - 1
            Console.Write(pPlayerRank(i).ToString & ", ")
        Next
        Console.WriteLine()
    End Sub
End Module
