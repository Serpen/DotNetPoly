Public Class Settings

    Private Sub New(pSetCol As Collections.Generic.Dictionary(Of String, String))
        For Each itm In pSetCol
            Select Case itm.Key
                Case "DICE_COUNT"
                    DICE_COUNT = CInt(itm.Value)
                Case "DICE_EYES"
                    DICE_EYES = CInt(itm.Value)
                Case "START_CAPITAL"
                    START_CAPITAL = CInt(itm.Value)
            End Select

        Next
    End Sub

    Public ReadOnly Property DICE_COUNT As Integer
    Public ReadOnly Property DICE_EYES As Integer
    Public ReadOnly Property START_CAPITAL As Integer

    Public Shared Function FromFile(pFile As String) As Settings
        Dim reader = IO.File.OpenText(pFile)
        Dim line() As String
        Dim SettingsCol As New Collections.Generic.Dictionary(Of String, String)(5)


        reader.ReadLine()

        Do Until reader.EndOfStream
            line = reader.ReadLine().Split(","c)


            SettingsCol.Add(line(0), line(1))
        Loop

        Dim SettingsObj As New Settings(SettingsCol)

        reader.Close()
        Return SettingsObj
    End Function
End Class
