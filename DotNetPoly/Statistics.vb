Partial Class GameBoard

    Friend Statistics As GameBoard.StatisticsClass

    Friend Class StatisticsClass
        Private _Gameboard As GameBoard

        Sub New(pGameboard As GameBoard)
            _Gameboard = pGameboard
            ReDim DiceRollSums(pGameboard.Settings.DICE_COUNT * pGameboard.Settings.DICE_EYES)
            ReDim FieldVisits(pGameboard.FieldCount - 1)
            ReDim FieldHovers(pGameboard.FieldCount - 1)
            'ReDim ChanceOccured([Enum].GetNames(GetType(eChanceType)).Length - 1)
            ChanceOccured = New Collections.Hashtable([Enum].GetNames(GetType(eActionType)).Length - 1)
            ActionChoosen = New Collections.Hashtable([Enum].GetNames(GetType(eActionType)).Length - 1)

            'TotalHousefields = pGameboard._Housefields.Values.Count
            For Each field In pGameboard.FieldCol.Values
                If field.GetType() Is GetType(DotNetPoly.Fields.HouseField) Then
                    TotalHousefields += 1
                End If
            Next
            Me.StartTime = DateTime.Now
        End Sub

#Region "Dice"
        Friend TotalDiceCount As Integer
        Friend DiceRollSums() As Integer
        Friend DoublesCount As Integer

        Friend ReadOnly Property TotalEyes As Integer
            Get
                Dim erg As Integer = 0
                For i = 0 To DiceRollSums.GetLength(0) - 1
                    erg += DiceRollSums(i) * i
                Next
                Return erg
            End Get
        End Property

        Friend ReadOnly Property AverageDiceSum As Integer
            Get
                Return CInt(TotalEyes / TotalDiceCount)
            End Get
        End Property
#End Region

        Friend ReadOnly TotalHousefields As Integer
        Friend ReadOnly Property AvailableHouseFields As Integer
            Get
                Return TotalHousefields - SoldHouseFields
            End Get
        End Property
        Friend Property SoldHouseFields As Integer

        Friend CompleteGroups As Integer

        Friend FieldVisits() As Integer
        Friend FieldHovers() As Integer

        Friend ChanceOccured As Collections.Hashtable
        Friend ActionChoosen As Collections.Hashtable

        Friend ReadOnly Property TotalCash As Integer
            Get
                Return _Gameboard.BANK.Cash
            End Get
        End Property

        ReadOnly StartTime As DateTime

        ReadOnly Property PlayTime As TimeSpan
            Get
                Return DateTime.Now - StartTime
            End Get
        End Property

        Friend MaxUpgradeLevel As Integer
        Friend MaxRent As Integer
        Friend MaxCost As Integer

        Friend Lap As Integer


    End Class

End Class
