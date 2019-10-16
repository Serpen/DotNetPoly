'
' Created by SharpDevelop.
' User: serpe
' Date: 22.11.2015
' Time: 12:18

Public MustInherit Class GameField

    Public Overridable Sub MoveOver(pPlayer As Player)
        Console.WriteLine(pPlayer.Name & " zieht über " & Me.Name)
        RaiseEvent FieldHovered(Me, pPlayer, New EventArgs)
    End Sub
	Public overridable Sub MoveOn(pPlayer As Player)
        Console.WriteLine(pPlayer.Name & " zieht auf " & Me.Name)
        RaiseEvent FieldVisited(Me, pPlayer, New EventArgs)
	End Sub
	
    Private _gameboard As GameBoard
    Public Property GameBoard As GameBoard
        Get
            Return _gameboard
        End Get
        Friend Set(value As GameBoard)
            _gameboard = value
        End Set
    End Property

    Public Sub New(pName As String)
        Me._Name = pName
        Me.Owner = Player.NoPlayer
	End Sub
	
    Private ReadOnly _name As String
	Public ReadOnly Property Name As String
		Get
            Return _name
		End Get
    End Property

    Public Overrides Function ToString() As String
        Return _name
    End Function
	
	Public Property Owner As Player

    Public Event FieldVisited(pField As GameField, pPlayer As Player, e As EventArgs)
    Public Event FieldHovered(pField As GameField, pPlayer As Player, e As EventArgs)
End Class
