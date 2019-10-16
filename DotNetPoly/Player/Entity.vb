
Public Class Entity
    Sub New(pName As String, pColor As System.Drawing.Color)
        Name = pName
        Color = pColor
    End Sub

#Region "Properties"

    Public ReadOnly Property Name As String

    Public ReadOnly Property Color As System.Drawing.Color

    Friend _ownfields As New Collections.Generic.List(Of Fields.HouseField)

    Public ReadOnly Property Cash As Integer

#End Region

    Friend Sub TransferMoney(pToEntity As Entity, pAmount As Integer)
        Dim transfer As Integer

        If pAmount > 0 Then
            If Me.GetType() Is GetType(Entity) Then
                'If Me.Equals(GameBoard.BANK) Then
                pToEntity._Cash += pAmount
                Me._Cash -= pAmount
                RaiseEvent MoneyTransfered(Me, pToEntity, pAmount)
            ElseIf Me.Cash < pAmount Then
                transfer = Me.Cash
                pToEntity._Cash += transfer
                Me._Cash -= transfer
                RaiseEvent MoneyTransfered(Me, pToEntity, transfer)
                DirectCast(Me, BasePlayer).onBankrupt()
            Else
                transfer = pAmount
                pToEntity._Cash += transfer
                Me._Cash -= transfer
                RaiseEvent MoneyTransfered(Me, pToEntity, transfer)
            End If
        End If


    End Sub

    Public Overrides Function ToString() As String
#If DEBUG Then
        Return Me.Name & " (" & Me.Cash & ")"
#Else
        Return Me.Name
#End If
    End Function

    Friend ReadOnly Property OwnFields As Fields.HouseField()
        Get
            Return _ownfields.ToArray()
        End Get
    End Property


    Public NotOverridable Overrides Function Equals(obj As Object) As Boolean
        If obj.GetType() Is GetType(Entity) OrElse obj.GetType().IsSubclassOf(GetType(Entity)) Then
            If DirectCast(obj, Entity).Name = Me.Name Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Event MoneyTransfered(pFromPlayer As Entity, pToPlayer As Entity, pAmount As Integer)

End Class
