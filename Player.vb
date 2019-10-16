'
' Created by SharpDevelop.
' User: serpen
' Date: 22.11.2015 12:25 
'
Public Class Player
	Implements IBoardAssignment

    Private ReadOnly ID As Integer
	
    Private ReadOnly _name As String
	Public ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

    Private ReadOnly _type As ePlayerType
	Public ReadOnly Property Type As ePlayerType
		Get
			return _type
		End Get
	End Property
	
    Private _gameboard As GameBoard
	Public ReadOnly Property Gameboard As GameBoard Implements IBoardAssignment.GameBoard
        Get
            Return _gameboard
        End Get
	End Property
		
    Public Shared ReadOnly NoPlayer As New Player("None", ePlayerType.System)
    Public Shared ReadOnly BANK As New Player("Bank", ePlayerType.System)
	
	Public Sub New(pName As String, pType As ePlayerType)
		Me._name = pName
		Me._type=pType
	End Sub
	
	Friend Sub AssignToBoard(pGameboard As GameBoard) Implements IBoardAssignment.assignToBoard
		If Me._gameboard Is Nothing Then
			Me._gameboard=pGameboard
		Else
			Throw New NotSupportedException
		End If
	End Sub
	
    Friend FieldIndex As UInteger = 0
	
    Private _cash As Integer
	Public ReadOnly Property Cash As Integer
		Get
			Return _cash
		End Get
    End Property

    Public Sub MoneyTransferTo(pTo As Player, amount As UShort)
        Dim transfer As Integer

        transfer = Me.Cash - amount
        If transfer >= 0 Or Me.Equals(Player.BANK) Then
            transfer = amount
        Else
            transfer = amount + transfer
            Me.onBankrupt()
        End If
        Me._cash -= transfer

        pTo._cash += transfer
        Console.WriteLine("Spieler '{0}' gibt Spieler '{1}' {2}", Me, pTo, transfer)
    End Sub

    Friend Sub onBuyOffer(pField As StreetField)
        Dim e As New ComponentModel.CancelEventArgs(True)
        RaiseEvent BuyOffer(Me, pField, e)
        If Not e.Cancel Then
            Me.MoneyTransferTo(Player.BANK, CUShort(pField.Cost))
            pField.Owner = Me
            Console.WriteLine("Spieler '{0}' kauft '{1}'", Me.Name, pField.Name)
        End If
    End Sub

    Public Event BuyOffer(sender As Player, pField As StreetField, e As ComponentModel.CancelEventArgs)

    Friend Sub onPayRent(pField As StreetField)
        Me.MoneyTransferTo(pField.Owner, CUShort(pField.Rent))

        RaiseEvent PayRent(Me, pField, New EventArgs)
    End Sub

    Public Event PayRent(sender As Player, pField As StreetField, e As EventArgs)

    Friend Sub onDice(pMoves As Integer)
        RaiseEvent Dice(Me, pMoves, New EventArgs())
    End Sub

    Public Event Dice(pSender As Player, pMoves As Integer, e As EventArgs)

    Public Overrides Function ToString() As String
        Return Me._name
    End Function

    Friend Sub onBankrupt()
        RaiseEvent Bankrupt(Me, New EventArgs())
    End Sub

    Public Event Bankrupt(pSender As Player, e As EventArgs)

End Class

Public Enum ePlayerType As Integer
	System
	AI
	User
End Enum