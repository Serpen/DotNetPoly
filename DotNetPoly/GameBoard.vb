Public Class GameBoard

    Const DICESEYES As Byte = 6
    Const DICECOUNT As Byte = 1

    Const INITIALCASH As UInteger = 100

    Private DiceRandomizer As New Random

    Public ReadOnly BANK As New Player("Bank", ePlayerType.System)

    Sub New(pPlayers As Player(), pFields As GameField())
        BANK._gameboard = Me

        _players = pPlayers
        For Each p In pPlayers
            p._gameboard = Me

            BANK.MoneyTransferTo(p, INITIALCASH)
        Next

        _fields = pFields
        For Each f As GameField In pFields
            f.GameBoard = Me
        Next

        Me.GameStats = New GameStatistics(Me)

        _activePlayerIndex = 0
        Me.Players(0)._active = True
    End Sub

    Private _activePlayerIndex As Integer
    Private Property ActivePlayerIndex As Integer
        Get
            Return _activePlayerIndex
        End Get
        Set(value As Integer)
            Me.Players(_activePlayerIndex)._active = False
            _activePlayerIndex = value
            Me.Players(_activePlayerIndex)._active = True
        End Set
    End Property

    Public ReadOnly Property ActivePlayer As Player
        Get
            Return Players(ActivePlayerIndex)
        End Get
    End Property

    Private ReadOnly _fields As GameField()
    Public ReadOnly Property Fields As GameField()
        Get
            Return _fields
        End Get
    End Property

    Private ReadOnly _players As Player()
    Public ReadOnly Property Players As Player()
        Get
            Return _players
        End Get
    End Property

    Shared Function CreateSampleBoard() As GameBoard
        Dim players1 As Player() = {New Player("Marco", ePlayerType.AI),
                                    New Player("Malte", ePlayerType.AI)} ',
        'New Player("Bot", ePlayerType.AI)}
        Dim fields1 As GameField() = {New StartField(),
            New StreetField("Karstadt", 100, 20),
            New StreetField("Sky", 80, 10),
            New StreetField("Edeka", 100, 15),
            New StreetField("Penny", 20, 6),
            New StreetField("Aldi", 25, 8),
            New StreetField("H&M", 110, 40),
            New StreetField("DOC", 200, 60)}
        Dim gb As New GameBoard(players1, fields1)

        Return gb
    End Function

    Public Function RollDice() As Integer()
        Me.GameStats.TotalDices += CUInt(1)
        Dim dices(DICECOUNT) As Integer
        
        For i = 0 To DICECOUNT
        	dices(i)=DiceRandomizer.Next(1, DICESEYES)
        Next
        
        Return dices

    End Function

    Sub setNextPlayer()
        Dim last = ActivePlayerIndex

        Do
            ActivePlayerIndex = (ActivePlayerIndex + 1) Mod (_players.GetLength(0))
        Loop While ActivePlayer.isBankrupt
        If ActivePlayerIndex = last Then
            _gameover = True
        End If
    End Sub

    Function checkGameOver() As Boolean
        'TODO: Dirty
        Dim pleiteCount As Integer
        For Each p In _players
            If p.isBankrupt Then pleiteCount += 1
        Next
        If pleiteCount = _players.GetLength(0) - 1 Then
            Me._gameover = True
            setNextPlayer()
        End If
        Return _gameover
    End Function

    Private _gameover As Boolean
    Public ReadOnly Property GameOver As Boolean
        Get
            Return _gameover
        End Get
    End Property

End Class
