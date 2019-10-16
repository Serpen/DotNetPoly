Public Class GameBoard
    Sub New(pPlayers() As Player, pFields() As Field, pSettings As Settings)
        PlayerRank = pPlayers
        Fields = pFields
        Settings = pSettings
    End Sub

    ReadOnly Property Fields As Field()
    ReadOnly Property Settings As Settings

    Public ReadOnly Property PlayerRank As Player()

    Private _activePlayer As Integer = -1
    ReadOnly Property ActivePlayer As Player
        Get
            Return PlayerRank(_activePlayer)
        End Get
    End Property

    ReadOnly Property IsGameOver As Boolean
        Get
            Return checkGameOver()
        End Get
    End Property

    Friend Function checkGameOver() As Boolean
        Dim activePlayersCount As Integer
        Dim lastActivePlayer As Integer
        For i As Integer = 0 To PlayerRank.GetLength(0) - 1
            If PlayerRank(i).Status < ePlayerStatus.Bankrupt Then
                activePlayersCount += 1
                lastActivePlayer = i
            End If
        Next 'i
        If activePlayersCount < 2 Then
            _activePlayer = lastActivePlayer
            RaiseEvent GameOver()
            Return True
        Else
            Return False
        End If
    End Function


    Sub Initialize()
        Dim sortedList As New Collections.SortedList()
        Dim sortkey As Integer

        For Each p In PlayerRank
            Player.BANK.TransferMoney(p, Settings.START_CAPITAL)
            p.Status = ePlayerStatus.Active
            p.GameBoard = Me
            p.Position = 0
        Next

        For Each p In PlayerRank
            Dim result = p.onDelegateControl(New ePlayerAction() {ePlayerAction.DiceOnly}, Nothing, ePlayerAction.DiceOnly)
            sortkey = CType(result.Result, DiceRollResult).DiceSum * 10
            While sortedList.ContainsKey(-sortkey)
                sortkey += 1
            End While
            sortedList.Add(-sortkey, p) 'negativ für Sortierung
        Next

        sortedList.Values.CopyTo(PlayerRank, 0)

        onPlayerRankChanged()

        For Each f In Fields
            f._gameboard = Me
            f.Initialize()
        Next
    End Sub

    Sub onChangeOwner(pField As StreetField, pPlayer As Player)
        pField.ChangeOwner(pPlayer)
        RaiseEvent FieldOwnerChange(pField, pPlayer)
    End Sub

    Sub performPlayerAction()
        Do
            _activePlayer = (_activePlayer + 1) Mod (PlayerRank.GetLength(0))
        Loop Until ActivePlayer.Status = ePlayerStatus.Waiting

        ActivePlayer.Status = ePlayerStatus.Active

        Do While ActivePlayer.Status = ePlayerStatus.Active
            If Not IsGameOver Then
                ActivePlayer.onDelegateControl(New ePlayerAction() {ePlayerAction.Move}, Nothing, ePlayerAction.Move)
            End If
        Loop

    End Sub

    Public Sub onFieldOwnerChange(pField As StreetField, pOwner As Player)
        RaiseEvent FieldOwnerChange(pField, pOwner)
    End Sub

    Sub onPlayerRankChanged()
        RaiseEvent PlayerRankChanged(PlayerRank)
    End Sub

    Event GameOver()

    Event FieldOwnerChange(pField As StreetField, pOwner As Player)

    Event PlayerRankChanged(pPlayerRank As Player())


End Class
