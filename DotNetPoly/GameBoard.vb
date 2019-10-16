Imports DotNetPoly.Fields

Public Class GameBoard

    Public ReadOnly Property BANK As New Entity("BANK", System.Drawing.Color.White)
    Public ReadOnly Property Wundertuete As New Entity("Wundertuete", System.Drawing.Color.White)

    Friend FreeParkingOwner As Entity = BANK
    Friend FreeJailOwner As Entity = BANK

    Sub New(pGameBoardFile As String, pPlayers As BasePlayer())
        Dim xmlfile As New Xml.XmlDocument()

        Dim contextCash As Integer
        Dim contextName As String
        Dim contextAction As eActionType

        Dim fileVersion As New Version("0.0")

        xmlfile.Load(System.Environment.ExpandEnvironmentVariables(pGameBoardFile))


        If Not Version.TryParse(xmlfile.SelectSingleNode("monopoly").Attributes("version")?.InnerText, fileVersion) Then
            fileVersion = New Version("0.0")
        End If

        If fileVersion.Minor < 5 Then
            Throw New NotSupportedException("Gameboard Version to low")
        End If

        Settings = New Settings(xmlfile.SelectSingleNode("monopoly"))

        For Each xmlfield As Xml.XmlNode In xmlfile.SelectNodes("monopoly/fields/*")

            contextName = xmlfield.Attributes.GetNamedItem("name")?.InnerText

            Integer.TryParse(xmlfield.Attributes.GetNamedItem("cash")?.InnerText, contextCash)

            Select Case xmlfield.Name.ToLower
                Case "start"

                    FieldCol.Add(contextName, New Fields.Startfield(contextCash, contextName))
                Case "house"

                    Integer.TryParse(xmlfield.Attributes.GetNamedItem("cost")?.InnerText, contextCash)

                    FieldCol.Add(contextName, New Fields.HouseField(contextName, contextCash))
                Case "chance"
                    If Not [Enum].TryParse(Of eActionType)(xmlfield.Attributes.GetNamedItem("action")?.InnerText, contextAction) Then
                        Diagnostics.Debug.WriteLine($"field {contextName} has unknown action {xmlfield.Attributes.GetNamedItem("action")?.InnerText}")
                    End If
                    FieldCol.Add(contextName, New Fields.ChanceField(contextName, contextAction, contextCash))
                Case "station"

                    FieldCol.Add(contextName, New Fields.StationField(contextName, contextCash))
                Case "jail"
                    If Not [Enum].TryParse(Of eActionType)(xmlfield.Attributes.GetNamedItem("action")?.InnerText, contextAction) Then
                        Diagnostics.Debug.WriteLine($"field {contextName} has unknown action {xmlfield.Attributes.GetNamedItem("action")?.InnerText}")
                    End If
                    FieldCol.Add(contextName, New Fields.JailField(contextName, contextAction))
                Case Else
                    FieldCol.Add(contextName, New Fields.ChanceField(contextName, eActionType.None, 0))
                    Throw New NotSupportedException($"Fieldtype {NameOf(xmlfield.Name)}={xmlfield.Name} not supported")
            End Select
        Next

        PlayerRank = pPlayers
        Statistic = New Statistics(Me)
    End Sub

    Private Property FieldCol As New FieldCollection

    ReadOnly Property FieldCount As Integer
        Get
            Return FieldCol.Count
        End Get
    End Property

    ReadOnly Property Settings As Settings

    Public ReadOnly Property PlayerRank As BasePlayer()

    Public ReadOnly Property JailField As JailField

    Friend ReadOnly Property _Housefields As New FieldCollection
    Public ReadOnly Property HouseFields(index As Integer) As HouseField
        Get
            Return DirectCast(_Housefields.Item(index), HouseField)
        End Get
    End Property


    Private _activePlayer As Integer = -1

    ReadOnly Property Fields(index As Integer) As Fields.BaseField
        Get
            Return FieldCol.Item(index)
        End Get
    End Property
    ReadOnly Property ActivePlayer As BasePlayer
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
        Dim activePlayersCount As Integer 'How many activePlayers
        Dim lastActivePlayer As Integer
        For i As Integer = 0 To PlayerRank.GetLength(0) - 1
            If PlayerRank(i).IsPlayering Then
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
        Dim housfieldcounter As Integer

        For i = 0 To FieldCol.Count - 1
            Fields(i).Initialize(Me, i)
            Select Case Fields(i).GetType
                Case GetType(JailField)
                    _JailField = DirectCast(Fields(i), JailField)
                Case GetType(HouseField)
                    _Housefields.Add((DirectCast(Fields(i), HouseField)).ToString(), (DirectCast(Fields(i), HouseField)))
                    housfieldcounter += 1
                Case Else
            End Select
        Next

        'Initialize without rank
        For i = 0 To PlayerRank.Length - 1
            PlayerRank(i).Initialize(Me, i)
        Next

        'Give every Player On dial for rank
        For Each p In PlayerRank
            _activePlayer = p.Index
            RaiseEvent NextPlayer(ActivePlayer)
            Dim result = p.DelegateDiceRoll()
            sortkey = result.DiceSum * 10
            While sortedList.ContainsKey(-sortkey)
                sortkey += 1
            End While
            sortedList.Add(-sortkey, p) 'negativ for sorting
        Next


        sortedList.Values.CopyTo(PlayerRank, 0)

        'Initialize Part 2 with rank
        For i = 0 To PlayerRank.Length - 1
            PlayerRank(i).Index=i
        Next

        _activePlayer = -1

        RaisePlayerRankChanged()



    End Sub

    Sub NextPlayersTurn()
        Dim playerActions As New Collections.Generic.List(Of eActionType) 'Stores possible player actions

        'Loop until isPlaying player who waits for turn
        Do
            _activePlayer = (_activePlayer + 1) Mod (PlayerRank.GetLength(0))
        Loop Until ActivePlayer.IsPlayering AndAlso ActivePlayer.NextAction.Peek() = eActionType.WaitingForTurn

        RaiseEvent NextPlayer(ActivePlayer)

        ' Should only dequeue waiting!?
        ActivePlayer.NextAction.Dequeue()

        'Find possible actions till next turn or NIL
        Do
            playerActions.Clear()

            Do
                playerActions.Add(ActivePlayer.NextAction.Dequeue())
            Loop Until ActivePlayer.NextAction.Count = 0 OrElse ActivePlayer.NextAction.Peek = eActionType.WaitingForTurn

            ActivePlayer.onDelegateControl(playerActions.ToArray(), Nothing, Nothing, 0)
        Loop Until ActivePlayer.NextAction.Count = 0 OrElse ActivePlayer.NextAction.Peek = eActionType.WaitingForTurn

        'Enqueue next default action, isnt't needed
        If ActivePlayer.IsPlayering Then
            If ActivePlayer.NextAction.Count = 0 Then
                ActivePlayer.NextAction.Enqueue(eActionType.WaitingForTurn)
                ActivePlayer.NextAction.Enqueue(eActionType.Move)
            End If

            'Should notify end turn
            If Settings.END_TURN_EVENT Then
                ActivePlayer.onDelegateControl(eActionType.EndTurn)
            End If
        End If

        ActivePlayer.DoublesCount = 0

    End Sub

    Sub RaiseChangeOwner(pField As HouseField, pPlayer As BasePlayer)
        RaiseEvent FieldOwnerChange(pField, pPlayer)
    End Sub

    Sub RaisePlayerRankChanged()
        RaiseEvent PlayerRankChanged(PlayerRank)
    End Sub

    Event GameOver()

    Event FieldOwnerChange(pField As HouseField, pOwner As BasePlayer)

    Event PlayerRankChanged(pPlayerRank As BasePlayer())

    Event NextPlayer(pPlayer As BasePlayer)

End Class
