'
' Created by SharpDevelop.
' User: serpe
' Date: 22.11.2015
' Time: 12:31
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Class GameBoard
	
	Const DICESEYES As Byte = 6
    Const DICECOUNT As Byte = 2

    Const INITIALCASH As UInteger = 12000

    Private DiceRandomizer As New Random

	Public _activePlayer As Integer = 0
	Public readonly Property ActivePlayer As Player
		Get
			Return Players(_activePlayer)
		End Get
	End Property

    Private ReadOnly _fields As New Collections.Generic.List(Of GameField)
	Public ReadOnly Property Fields As GameField()
		Get
			Return _fields.ToArray
		End Get
	End Property
	
    Private ReadOnly _players As New Collections.Generic.List(Of Player)
	Public ReadOnly Property Players As Player()
		Get
			Return _players.ToArray
		End Get
	End Property
	
	Sub AssignPlayer(pPlayer As Player)
		pPlayer.AssignToBoard(Me)
		_players.Add(pPlayer)
	End Sub
	
	Sub assignFields(pFields As GameField())
		For Each f As GameField In pFields
			f.assignToBoard(Me)
			_fields.Add(f)
		Next
	End Sub
	
	Shared Function CreateSampleBoard() As GameBoard
		Dim players1 As Player() = {New Player("Marco", ePlayerType.User)}
        Dim fields1 As GameField() = {New StartField(), _
            New StreetField("Karstadt", 100, 20), _
            New StreetField("Sky", 100, 10), _
            New StreetField("Edeka", 100, 15), _
            New StreetField("Penny", 20, 6), _
            New StreetField("DOC", 200, 40)}
		Dim gb As New GameBoard()
		For Each p As Player In players1
			gb.AssignPlayer(p)
		Next
		gb.assignFields(fields1)
		
		Return gb
	End Function

	Public Sub RollDice()
		Dim playerField As Integer = activePlayer.FieldIndex
		Dim maxField As Integer = _fields.Count - 1
		
		Dim moves As Integer = DiceRandomizer.Next(DICECOUNT,DICECOUNT*DICESEYES)

        ActivePlayer.onDice(moves)

		Console.WriteLine(ActivePlayer.Name & " würfelt " & moves)
		
		For i As Integer = 1 To moves
			playerField = (playerField + 1) Mod (maxField+1)
			If i < moves then
				Fields(playerField).MoveOver(ActivePlayer)
			Else
				Fields(playerField).MoveOn(ActivePlayer)
			End If
        Next i
        _activePlayer = +1 Mod (_players.Count)
    End Sub

    Public ReadOnly Property GameOver As Boolean
        Get
            For Each f In Fields
                If Not f.Owner.Equals(Players(0)) Then
                    Return False
                End If
            Next
            Return True
        End Get
    End Property

End Class
