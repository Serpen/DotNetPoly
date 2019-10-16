Public Class DiceRollResult

    Private Shared DiceRandomizer As New Random()

    Friend Sub New(pPlayer As Player, pNumDices As Integer, pNumEyes As Integer)
        Dim tmpisPash As Boolean?

        Player = pPlayer

        ReDim Dices(pNumDices - 1)


        For i = 0 To pNumDices - 1
            Dices(i) = CUShort(DiceRandomizer.Next(1, pNumEyes))
            DiceSum += Dices(i)

            If i > 0 Then
                If (Not tmpisPash.HasValue OrElse tmpisPash.Value = True) And (Dices(i - 1) = Dices(i)) Then
                    tmpisPash = True
                ElseIf Dices(i - 1) <> Dices(i) Then
                    tmpisPash = False
                Else
                    Console.Write("DEBUG: na")
                End If
            End If

        Next
        If tmpisPash = True Then
            IsPash = True
        End If
    End Sub

    Private ReadOnly Property Player As Player
    Public ReadOnly Property Dices As Integer()
    Public ReadOnly Property DiceSum As Integer
    Public ReadOnly Property IsPash As Boolean

End Class
