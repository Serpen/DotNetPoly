Public Class DiceRollResult

    Private Shared DiceRandomizer As New Random()

    Private Shared PlayerDoubles() As Integer

    Friend Sub New(pNumDices As Integer, pNumEyes As Integer)
        Dim tmpisDoubles As Boolean?

        ReDim Dices(pNumDices - 1)

        For i = 0 To pNumDices - 1
            Dices(i) = CUShort(DiceRandomizer.Next(1, pNumEyes + 1))
            DiceSum += Dices(i)

            If i > 0 Then
                If (Not tmpisDoubles.HasValue OrElse tmpisDoubles.Value = True) And (Dices(i - 1) = Dices(i)) Then
                    tmpisDoubles = True
                Else
                    tmpisDoubles = False
                End If
            End If
        Next

        If tmpisDoubles Then
            IsDoubles = True
        End If
    End Sub

    Public Overrides Function ToString() As String
        If Dices.Length > 1 Then
            Return String.Format("{0}={1} ({2})", String.Join("+", Dices), DiceSum, IsDoubles)
        Else
            Return String.Format("{0}", Dices(0))
        End If

    End Function

    Public ReadOnly Property Dices As Integer()
    Public ReadOnly Property DiceSum As Integer
    Public ReadOnly Property IsDoubles As Boolean

End Class
