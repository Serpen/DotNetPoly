'
' Created by SharpDevelop.
' User: serpe
' Date: 22.11.2015
' Time: 12:18
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public MustInherit Class GameField
	Implements IBoardAssignment
	
	Public Overridable Sub MoveOver(pPlayer As Player)
		Console.WriteLine(pPlayer.Name & " zieht über " & Me.Name)
	End Sub
	Public overridable Sub MoveOn(pPlayer As Player)
		Console.WriteLine(pPlayer.Name & " zieht auf " & Me.Name)
	End Sub
	
    Private _gameboard As GameBoard
	Public ReadOnly Property GameBoard As GameBoard Implements IBoardAssignment.GameBoard
		Get
			Return _GameBoard
		End Get
	End Property
	
	Public Sub New(pName As String)
        Me._Name = pName
        Me.Owner = Player.NoPlayer
	End Sub
	
    Private ReadOnly _Name As String
	Public ReadOnly Property Name As String
		Get
			Return _Name	
		End Get
	End Property
	
	Public Property Owner As Player
		
	Sub AssignToBoard(pBoard As GameBoard) implements IBoardAssignment.assignToBoard
		If _gameboard Is Nothing Then
			Me._gameboard=pBoard
		Else
			Throw New NotImplementedException()
		End If
	End Sub
End Class
