Option Compare Text

Namespace Fields

    Public MustInherit Class BaseField

        Public Sub New(pName As String)
            Name = pName
        End Sub

        Sub Initialize(pGameBoard As GameBoard, pIndex As Integer)
            _gameboard = pGameBoard
            _Index = pIndex
            _owner = pGameBoard.BANK
        End Sub

        ReadOnly Property Name As String

        Protected _owner As Entity
        Public Overridable ReadOnly Property Owner As Entity
            Get
                Return _owner
            End Get
        End Property

        Private _gameboard As GameBoard
        Public ReadOnly Property GameBoard As GameBoard
            Get
                Return _gameboard
            End Get
        End Property

        Friend Overridable Sub onMoveOver(pPlayer As BasePlayer)
            pPlayer.RaiseMoveOver(Me)
            GameBoard.Statistic.FieldHovers(Me.Index) += 1
        End Sub

        Friend Overridable Sub onMoveOn(pPlayer As BasePlayer)
            pPlayer.RaiseMoveOn(Me)
            GameBoard.Statistic.FieldVisits(Me.Index) += 1
        End Sub

        Public ReadOnly Property Index As Integer

        Public Overrides Function ToString() As String
#If DEBUG Then
            Return $"{Me.Name}"
#Else
            Return Me.Name
#End If

        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            If obj.GetType() Is GetType(BaseField) OrElse obj.GetType().IsSubclassOf(GetType(BaseField)) Then
                Dim bobj = DirectCast(obj, BaseField)
                If bobj.Name = Me.Name AndAlso bobj.Index = Me.Index Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function
    End Class

End Namespace