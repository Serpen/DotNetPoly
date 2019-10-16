'
' Created by SharpDevelop.
' User: serpen
' Date: 22.11.2015 12:25 
'
Public Class Player

    Dim botlogic As PlayerAI
    Friend FieldIndex As UInteger = 0

    Public Shared ReadOnly NoPlayer As New Player("None", ePlayerType.System)

    Public Sub New(pName As String, pType As ePlayerType)
        Me._name = pName
        Me._type = pType
        If pType = ePlayerType.AI Then
            botlogic = New PlayerAI(Me, Nothing, PlayerAI.eAI.BuyWhenPossible)
            AddHandler BuyOffer, AddressOf botlogic.BuyOfferHandler
            AddHandler Bankrupt, AddressOf botlogic.BankruptHandler
            AddHandler ActivatePlayer, AddressOf botlogic.DicingHandler
        End If
        _NextAction = ePlayerActions.Move
    End Sub
    
    Private readonly _name As String
    Public ReadOnly Property Name As String
    	Get
    		Return _name
    	End Get
    End Property
    
    Private readonly _type As ePlayerType
    Public ReadOnly Property Type As ePlayerType
    	Get
    		Return _type
    	End Get
    End Property

    Friend _gameboard As GameBoard
    Public ReadOnly Property Gameboard As GameBoard
        Get
            Return _gameboard
        End Get
    End Property

    Private _isBankrupt As Boolean
    Public ReadOnly Property isBankrupt As Boolean
        Get
            Return _isBankrupt
        End Get
    End Property


    Friend _active As Boolean
    Public ReadOnly Property Active As Boolean
        Get
            Return _active
        End Get
    End Property
    
    Private _cash As Integer
    Public ReadOnly Property Cash As Integer
    	Get 
    		Return _cash
    	End Get
    End Property
    
    Private _nextAction As ePlayerActions
    Public ReadOnly Property NextAction As ePlayerActions
    	Get
    		Return _nextAction
    	End Get
    End Property

    Public Overrides Function ToString() As String
        Return Me._name & " [" & Me.Cash & "]"
    End Function

    Public Sub MoneyTransferTo(pTo As Player, amount As UShort)
        Dim transfer As Integer

        If Me.Equals(Gameboard.BANK) Then
            'Banktransfer 
            transfer = amount
            pTo._Cash += transfer
            Me._Cash -= transfer
        ElseIf Me.Cash < amount Then
            transfer = Me.Cash
            pTo._Cash += transfer
            Me._Cash -= transfer
            Me._isBankrupt = True
        Else
            transfer = amount
            pTo._cash += transfer
            Me._cash -= transfer
        End If

        Console.WriteLine("Spieler '{0}' gibt Spieler '{1}' {2}", Me, pTo, transfer)

        If Me.isBankrupt Then
            Me.onBankrupt()
        End If

    End Sub

    Public Sub PerformNextAction()
        If NextAction = ePlayerActions.Move Then
            MoveFields(_lastDiceSum)
        End If
        If Not _wasPash Then
	        Gameboard.setNextPlayer()
	        _wasPash=False	
        End If
        Gameboard.checkGameOver()

    End Sub

    Private Sub MoveFields(moves As Integer)
        Dim playerField As Integer = CInt(FieldIndex)
        Dim maxField As Integer = Gameboard.Fields.GetLength(0)
        For i As Integer = 1 To moves
            playerField = (playerField + 1) Mod (maxField)
            If i < moves Then
                Gameboard.Fields(playerField).MoveOver(Me)
            Else
                Gameboard.Fields(playerField).MoveOn(Me)
            End If
        Next i
        FieldIndex = CUInt(playerField)

    End Sub

    Friend Sub onBuyOffer(pField As StreetField)
        Dim e As New ComponentModel.CancelEventArgs(True)
        If Me.Cash >= pField.Cost Then
            RaiseEvent BuyOffer(Me, pField, e)
            If Not e.Cancel Then
                Me.MoneyTransferTo(Gameboard.BANK, CUShort(pField.Cost))
                pField.Owner = Me
                Console.WriteLine("Spieler '{0}' kauft '{1}'", Me.Name, pField.Name)
            End If
        Else
            Console.WriteLine("Spieler '{0}' hat nicht genügend Cash um '{1}' zu kaufen", Me, pField)
        End If


    End Sub

    Public Event BuyOffer(sender As Player, pField As StreetField, e As ComponentModel.CancelEventArgs)

    Friend Sub onPayRent(pField As StreetField)
        Me.MoneyTransferTo(pField.Owner, CUShort(pField.Rent))

        RaiseEvent PayRent(Me, pField, New EventArgs)
    End Sub

    Public Event PayRent(sender As Player, pField As StreetField, e As EventArgs)

    Private _lastDice As Integer()
    Private _lastDiceSum As Integer
    Private _wasPash As Boolean

    Public Function RollDice() As Integer()
        If Active Then
            _lastDice = Me.Gameboard.RollDice()
            _lastDiceSum=0
            If _lastDice.GetLength(0)>1 Then
            	_wasPash=true
	            Dim prevdice = _lastDice(0)
	            
				For i = 1 To _lastDice.GetLength(0)-1
					_wasPash = _wasPash AndAlso prevdice = _lastDice(i)
					prevdice = _lastDice(i)
				Next
            Else
            	_wasPash=False
            End If
            
            For i = 0 To _lastDice.GetLength(0)-1
            	_lastDiceSum += _lastDice(i)	
			Next		
            Return _lastDice
            Else
                Throw New NotPlayersTurnException
        End If
    End Function

    Friend Sub onBankrupt()
        For Each f In _gameboard.Fields
            If f.Owner.Equals(Me) Then
                f.Owner = Player.NoPlayer
            End If
        Next
        Me._gameboard.GameStats.ActivePlayers -= 1
        Me.Gameboard.checkGameOver()
        RaiseEvent Bankrupt(Me, New EventArgs())
    End Sub

    Public Event Bankrupt(pSender As Player, e As EventArgs)

    Sub onActivate()
        RaiseEvent ActivatePlayer(Me)
    End Sub
    Public Event ActivatePlayer(pSender As Player)

End Class

Public Enum ePlayerType As Integer
	System
	AI
	User
End Enum