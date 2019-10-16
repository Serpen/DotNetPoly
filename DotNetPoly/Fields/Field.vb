'TODO: Mustinherit Class Field
Public Class Field

    Public Sub New(pName As String)
        Name = pName
    End Sub

    Overridable Sub Initialize()
    End Sub

    ReadOnly Property Name As String

    Protected _owner As Player = Player.BANK
    Public Overridable ReadOnly Property Owner As Player
        Get
            Return _owner
        End Get
    End Property

    Friend _gameboard As GameBoard
    Public ReadOnly Property GameBoard As GameBoard
        Get
            Return _gameboard
        End Get
    End Property

    Friend Overridable Sub onMoveOver(pPlayer As Player)
        pPlayer.onMoveOver(Me)
    End Sub

    Friend Overridable Function onMoveOn(pPlayer As Player) As PlayerActionResult
        pPlayer.onMoveOn(Me)
    End Function

    Public Overrides Function ToString() As String
        Return Me.Name
    End Function

#Region "Shared"

    Public Shared Function FromFile(pFile As String) As Field()
        Dim reader = IO.File.OpenText(pFile)
        Dim line() As String
        Dim fieldCol As New Collections.Generic.List(Of Field)

        reader.ReadLine()

        Do Until reader.EndOfStream
            line = reader.ReadLine().Split(","c)

            Select Case line(0)
                Case "StreetField"
                    fieldCol.Add(New StreetField(line(1), CUShort(line(2))))
                Case "StartField"
                    fieldCol.Add(New Startfield(CInt(line(2))))
                Case Else
                    fieldCol.Add(New Field(line(1)))
                    'Throw New NotSupportedException
            End Select


        Loop

        reader.Close()
        Return fieldCol.ToArray()
    End Function

#End Region

End Class
