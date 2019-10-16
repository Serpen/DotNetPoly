'
' Created by SharpDevelop.
' User: serpe
' Date: 22.11.2015
' Time: 12:38
' 
'
Public Class StreetField
	Inherits GameField
	
    Private ReadOnly _cost As UInteger
    Public ReadOnly Property Cost As UInteger
        Get
            Return _cost
        End Get
    End Property
	
    Private ReadOnly _rent As UInteger
    Public ReadOnly Property Rent As UInteger
        Get
            Return _rent
        End Get
    End Property

    Public Sub New(pName As String, pCost As UInteger, pEarn As UInteger)
        MyBase.New(pName)
        Me._cost = pCost
        Me._rent = pEarn
    End Sub

    Public Overrides Sub MoveOn(pPlayer As Player)
        MyBase.MoveOn(pPlayer)
        If Not Me.Owner.Equals(pPlayer) Then
            If Me.Owner.Equals(Player.NoPlayer) Then
                Dim e As New ComponentModel.CancelEventArgs
                pPlayer.onBuyOffer(Me)
            Else
                pPlayer.onPayRent(Me)
            End If
        Else
            ' Eigenes Feld
        End If
    End Sub
	
End Class
