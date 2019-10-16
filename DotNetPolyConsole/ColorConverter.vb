Public Class ColorConverter

    Private Shared MatchingCache As New Collections.Generic.Dictionary(Of System.Drawing.Color, ConsoleColor)

    Friend Shared Function ClosestConsoleColor(color As System.drawing.color) As ConsoleColor
        Dim ret As ConsoleColor

        If MatchingCache.TryGetValue(color, ret) Then
            Return ret
        End If

        Dim rr As Double = color.R
        Dim gg As Double = color.G
        Dim bb As Double = color.B
        Dim delta = Double.MaxValue

        For Each cc As ConsoleColor In [Enum].GetValues(GetType(ConsoleColor))
            Dim name = [Enum].GetName(GetType(ConsoleColor), cc)
            Dim c = System.Drawing.Color.FromName(CStr(Microsoft.VisualBasic.IIf(name = "DarkYellow", "Orange", name)))
            Dim t = Math.Pow(c.R - rr, 2) + Math.Pow(c.G - gg, 2) + Math.Pow(c.B - bb, 2)
            If t = 0 Then
                If Not MatchingCache.ContainsKey(color) Then MatchingCache.Add(color, cc)
                Return cc
            End If
            If t < delta Then
                delta = t
                ret = cc
            End If
        Next
        If Not MatchingCache.ContainsKey(color) Then MatchingCache.Add(color, ret)
        Return ret
    End Function

End Class
