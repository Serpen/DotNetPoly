Imports DotNetPoly
Imports DotNetPoly.Fields

Module Program
    Private WithEvents gb As GameBoard

    Sub Main(args() As String)
        Dim gamefile As String

        If args.Length = 0 Then
            Console.Write("Spielbrettdatei: ")
            gamefile = Console.ReadLine()
            Console.WriteLine()
        Else
            gamefile = args(0)
        End If
        Console.ForegroundColor = ConsoleColor.White
        Dim players() As BasePlayer = {
            New BotPlayer("Marco", System.Drawing.Color.Red),
            New BotPlayer("Manfred", System.Drawing.Color.DarkGreen),
            New BotPlayer("Malte", System.Drawing.Color.Cyan)}


        gb = New GameBoard(gamefile, players)

        'Set Event Handlers for Players
        For Each p In gb.PlayerRank
            AddHandler p.RollDice, AddressOf RollDice
            If p.GetType() Is GetType(HumanPlayer) Then
                AddHandler CType(p, HumanPlayer).DelegateControl, AddressOf DelegateControl
            End If

            AddHandler p.Bankrupt, AddressOf Bankrupt
            AddHandler p.MoneyTransfered, AddressOf MoneyTransfered
            AddHandler p.MoveOn, AddressOf MoveOn
            AddHandler p.MoveOver, AddressOf MoveOver
            AddHandler p.Jail, AddressOf Prison
            AddHandler p.Chance, AddressOf Chance
            AddHandler p.UpgradeHouse, AddressOf UpgradeHouse
        Next
        AddHandler gb.BANK.MoneyTransfered, AddressOf MoneyTransfered

        gb.Initialize()

        Do Until gb.IsGameOver
            gb.NextPlayersTurn()
        Loop

        Console.WriteLine("Spiel ist vorbei, gewonnen hat: '{0}'", gb.ActivePlayer)

        Console.Write("Press any key to continue . . . ")
        Console.ReadKey(True)
    End Sub

    Private Sub WriteLineInPlayerColor(pLine As String, pPlayer As Entity, ParamArray format As Object())
        Dim preColor = Console.ForegroundColor
        Console.ForegroundColor = ColorConverter.ClosestConsoleColor(pPlayer.Color)
        If Console.BackgroundColor = Console.ForegroundColor Then Console.ForegroundColor = preColor
        Console.WriteLine(pLine, format)
        Console.ForegroundColor = preColor
    End Sub

    Sub DelegateControl(pPlayer As BasePlayer, pPossibleActions() As eActionType, pFields As BaseField(), pPlayers() As Entity, pCash As Integer)
        Dim counter As Integer = 1
        Dim erg As String

        WriteLineInPlayerColor("Spieler {0} ist am Zug und hat folgende Möglichkeiten:", pPlayer, pPlayer)
        For Each a In pPossibleActions
            Select Case a
                Case eActionType.BuyHouse
                    WriteLineInPlayerColor("{0} - {1} ({2} {3})", pPlayer, counter, a.ToString(), pFields(0), DirectCast(pFields(0), Fields.HouseField).Cost)
                Case eActionType.PayRent
                    WriteLineInPlayerColor("{0} - {1} {2} für ({3})", pPlayer, counter, a.ToString(), DirectCast(pFields(0), HouseField).Rent, pFields(0))
                Case eActionType.UpgradeHouse
                    WriteLineInPlayerColor("{0} - {1} {2} für {3}", pPlayer, counter, a.ToString(), pFields(0), DirectCast(pFields(0), Fields.HouseField).UpgradeCost)
                Case eActionType.CashFromBank, eActionType.CashFromPlayer, eActionType.CollectStack
                    WriteLineInPlayerColor("{0} - {1} {2} von ({3})", pPlayer, counter, a.ToString(), pCash, pPlayers(0))
                Case eActionType.CashToBank, eActionType.CashToPlayer, eActionType.PayToStack
                    WriteLineInPlayerColor("{0} - {1} {2} an ({3})", pPlayer, counter, a.ToString(), pCash, pPlayers(0))

                Case Else
                    WriteLineInPlayerColor(counter & " - " & a.ToString, pPlayer)
            End Select

            counter += 1
        Next

        erg = Console.ReadLine()
        Try
            pPlayer.SubmitChoosenAction(CInt(erg), Nothing)
        Catch ex As Exception
            pPlayer.SubmitChoosenAction(0, Nothing)
        End Try

    End Sub

    Sub Bankrupt(sender As BasePlayer)
        Dim pre = Console.BackgroundColor
        Console.BackgroundColor = ConsoleColor.Red
        WriteLineInPlayerColor("Spieler '{0}' ist bankrott", sender, sender)
        Console.BackgroundColor = pre
    End Sub

    Sub RollDice(sender As BasePlayer, pDiceRollResult As DiceRollResult)
        WriteLineInPlayerColor("Spieler '{0}' würfelt {1}", sender, sender, pDiceRollResult)
    End Sub

    Sub MoneyTransfered(pFromPlayer As Entity, pToPlayer As Entity, pAmount As Integer)
        Dim precolor = Console.ForegroundColor
        Console.ForegroundColor = ColorConverter.ClosestConsoleColor(pFromPlayer.Color)
        Console.Write("Spieler '{0}' gibt Spieler ", pFromPlayer)
        Console.ForegroundColor = ColorConverter.ClosestConsoleColor(pToPlayer.Color)
        Console.Write("'{1}' ", pFromPlayer, pToPlayer, pAmount)
        Console.ForegroundColor = ColorConverter.ClosestConsoleColor(pFromPlayer.Color)
        Console.WriteLine(pAmount)
        Console.ForegroundColor = precolor
    End Sub

    Sub MoveOn(pPlayer As BasePlayer, pField As BaseField)
        Dim precolor = Console.ForegroundColor
        Console.ForegroundColor = ColorConverter.ClosestConsoleColor(pPlayer.Color)
        Console.Write("Spieler '{0}' zieht auf ", pPlayer, pPlayer, pField)
        Console.ForegroundColor = ColorConverter.ClosestConsoleColor(pField.Owner.Color)
        Console.WriteLine(pField)
        Console.ForegroundColor = precolor
    End Sub

    Sub MoveOver(pPlayer As BasePlayer, pField As BaseField)
        If pField.GetType() = GetType(Startfield) Then
            WriteLineInPlayerColor("Spieler '{0}' zieht über {1}", pPlayer, pPlayer, pField)
        End If
    End Sub

    Private Sub PlayerRankChanged(pPlayerRank() As BasePlayer) Handles gb.PlayerRankChanged
        Console.WriteLine("Spielerreihenfolge ist: {0}", String.Join(", ", CType(pPlayerRank, Object())))
    End Sub

    Private Sub Prison(pPlayer As BasePlayer, pPrisonEvent As eActionType)
        If pPrisonEvent = eActionType.JumpToJail Then
            WriteLineInPlayerColor("Spieler {0} geht in {1}", pPlayer, pPlayer, "Jail")
        ElseIf pPrisonEvent = eActionType.LoseTurn Then
            WriteLineInPlayerColor("Spieler {0} setzt eine Runde aus", pPlayer, pPlayer)
        End If

    End Sub

    Sub Chance(pPlayer As BasePlayer, pChance As eActionType, pObject() As Object)
        WriteLineInPlayerColor("Spieler {0} löst Event {1} mit {2} aus", pPlayer, pPlayer, pChance.ToString(), String.Join(", ", pObject))
    End Sub

    Sub UpgradeHouse(pPlayer As BasePlayer, pHouse As HouseField)
        WriteLineInPlayerColor("Spieler {0} baut {1} aus", pPlayer, pPlayer, pHouse)
    End Sub

    Private Sub FieldOwnerChange(pField As HouseField, pOwner As Entity) Handles gb.FieldOwnerChange
        WriteLineInPlayerColor("Spieler {0} kauft {1}", pOwner, pOwner, pField)
    End Sub
End Module
