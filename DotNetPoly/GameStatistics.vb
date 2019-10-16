Partial Public Class GameBoard
    Public Class GameStatistics
        Friend Sub New(pBoard As GameBoard)
            _gameboard = pBoard

            TotalFields = pBoard._fields.GetLength(0) + 1
            TotalPlayer = pBoard._players.GetLength(0)
            ActivePlayers = TotalPlayer


            ReDim FieldVisits(TotalFields)
            ReDim PlayerFields(TotalPlayer)

        End Sub

        Private ReadOnly _gameboard As GameBoard

        Public FieldVisits As Integer()
        Public PlayerFields As Integer()

        Public TotalFields As Integer
        Public SoldFields As Integer

        Public TotalPlayer As Integer
        Public ActivePlayers As Integer

        Public TotalDices As UInteger

    End Class

    Friend GameStats As GameStatistics

End Class
