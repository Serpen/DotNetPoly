Namespace Fields

    Public Class HouseField
        Inherits BaseField

        Friend Sub New(pXmlNode As Xml.XmlNode, pSettings As Settings)
            MyBase.New(pXmlNode.Attributes("name")?.InnerText)
            Integer.TryParse(pXmlNode.Attributes("group")?.InnerText, Group)

            Dim upgradeNodes = pXmlNode.SelectNodes("./level")
            ReDim UpgradeMatrix(upgradeNodes.Count - 1, 1)

            For i = 0 To upgradeNodes.Count - 1
                    Integer.TryParse(upgradeNodes(i).Attributes("cost").InnerText, UpgradeMatrix(i, 0))
                    Integer.TryParse(upgradeNodes(i).Attributes("rent").InnerText, UpgradeMatrix(i, 1))
                Next
            maxUpgradeLevel = upgradeNodes.Count - 1


            UpgradeAlgorithmRent = pSettings.RentUpgradeMethode
            UpgradeAlgorithmCost = pSettings.CostUpgradeMethode
            UpgradePreReq = pSettings.UpgradePreReq
        End Sub

        Private ReadOnly maxUpgradeLevel As Integer = 2

        Private ReadOnly UpgradePreReq As eUpgradeAlgorithm
        Private ReadOnly UpgradeAlgorithmRent As eUpgradeAlgorithm
        Private ReadOnly UpgradeAlgorithmCost As eUpgradeAlgorithm

        Public Enum eUpgradeAlgorithm
            FullGroup

            Table
            FullGroupDoublesRent
        End Enum

        Private ReadOnly UpgradeMatrix(,) As Integer

        ReadOnly Property Cost As Integer
            Get
                Dim myCost As Integer = 0
                For i = 0 To UpgradeLevel
                    myCost += UpgradeMatrix(i, 0)
                Next
                Return myCost
            End Get
        End Property

        ReadOnly Property UpgradeLevel As Integer = 0

        Public ReadOnly Group As Integer

        ReadOnly Property Rent As Integer
            Get
                Dim multiplier As Integer
                If UpgradeAlgorithmRent = eUpgradeAlgorithm.FullGroupDoublesRent AndAlso Not Me.Owner.Equals(GameBoard.BANK) AndAlso hasCompleteStreet(Me.Owner) Then
                    multiplier = 2
                Else
                    multiplier = 1
                End If

                Return UpgradeMatrix(UpgradeLevel, 1) * multiplier
            End Get
        End Property

        Friend Sub Buy(pPlayer As BasePlayer)
            Me.GameBoard.RaiseChangeOwner(Me, pPlayer)

            pPlayer.TransferMoney(Me.Owner, Me.Cost)
            ChangeOwner(pPlayer)

        End Sub

        Private Function hasCompleteStreet(pPlayer As Entity) As Boolean
            If GameBoard.Statistics.CompleteGroups = 0 Then Return False
            For Each h As HouseField In GameBoard.Groups(Me.Group)
                If Not h.Owner.Equals(pPlayer) Then
                    Return False
                End If
            Next
            Return True
        End Function

        Public Function CanUpgrade(pPlayer As BasePlayer) As Boolean
            If UpgradeLevel >= maxUpgradeLevel Then Return False
            If UpgradeAlgorithmRent = eUpgradeAlgorithm.Table AndAlso UpgradeLevel >= UpgradeMatrix.GetLength(0) Then Return False
            If pPlayer.Cash < UpgradeCost Then Return False

            'If pPlayer.GetType() Is GetType(Entity) Then Return False
            If UpgradePreReq = eUpgradeAlgorithm.FullGroup Then
                Return hasCompleteStreet(pPlayer)
            End If
            Return True
        End Function

        Public ReadOnly Property UpgradeCost As Integer
            Get
                Select Case UpgradeAlgorithmRent
                    Case eUpgradeAlgorithm.Table
                        Return UpgradeMatrix(UpgradeLevel, 0)
                    Case Else 'eUpgradeAlgorithm.Linear
                        Return Math.Max(CInt(Me.Cost * (_UpgradeLevel + 1) / 2), _UpgradeLevel + 1) 'Cost >= 1
                End Select
            End Get
        End Property

        Friend Sub Upgrade()
            Dim _owner = DirectCast(Owner, BasePlayer)
            Owner.TransferMoney(GameBoard.BANK, UpgradeCost)
            If _owner.IsPlayering Then
                DirectCast(Owner, BasePlayer).RaiseEventHouseUpgrade(Me)
                _UpgradeLevel += 1
            End If

            If Me.Cost > GameBoard.Statistics.MaxCost Then GameBoard.Statistics.MaxCost = Me.Cost
            If Me.Rent > GameBoard.Statistics.MaxRent Then GameBoard.Statistics.MaxRent = Me.Rent
            If Me.UpgradeLevel > GameBoard.Statistics.MaxUpgradeLevel Then GameBoard.Statistics.MaxUpgradeLevel = Me.UpgradeLevel
        End Sub

        Friend Overrides Sub onMoveOn(pPlayer As BasePlayer)
            MyBase.onMoveOn(pPlayer)

            If Me.Owner.Equals(GameBoard.BANK) Then
                pPlayer.onDelegateControl(New eActionType() {eActionType.BuyHouse, eActionType.Pass}, New BaseField() {Me}, Nothing, 0)
            ElseIf Me.Owner.Equals(pPlayer) Then
                If CanUpgrade(pPlayer) Then
                    pPlayer.onDelegateControl(New eActionType() {eActionType.UpgradeHouse, eActionType.Pass}, New BaseField() {Me}, Nothing, 0)
                End If

            Else
                pPlayer.onDelegateControl(eActionType.PayRent, Me)

            End If
        End Sub

        Friend Sub ChangeOwner(pPlayer As Entity)
            Dim dbg1 As Integer = 0

            If Me._owner.Equals(GameBoard.BANK) Then
                GameBoard.Statistics.SoldHouseFields += 1
            Else
                GameBoard.Statistics.SoldHouseFields -= 1
            End If





            Me._owner._ownfields.Remove(Me)
            Me._owner = pPlayer

            'If pPlayer.GetType() IsNot GetType(Entity) Then
            pPlayer._ownfields.Add(Me)
            'End If

            'HACK: Correcter for Wrong Count
            Dim totalsold As Integer
            For Each f As HouseField In GameBoard._Housefields.Values
                If Not f.Owner.Equals(GameBoard.BANK) Then
                    totalsold += 1
                End If
            Next

            If Me.Cost > GameBoard.Statistics.MaxCost Then GameBoard.Statistics.MaxCost = Me.Cost
            If Me.Rent > GameBoard.Statistics.MaxRent Then GameBoard.Statistics.MaxRent = Me.Rent
            If Me.UpgradeLevel > GameBoard.Statistics.MaxUpgradeLevel Then GameBoard.Statistics.MaxUpgradeLevel = Me.UpgradeLevel

            'Check Whether Group ist complete
            Dim groupcomplete As Boolean = True
            For Each h As HouseField In GameBoard.Groups(Me.Group)
                If Not h.Owner.Equals(pPlayer) Then
                    groupcomplete = False
                End If
            Next

            If groupcomplete Then
                GameBoard.Statistics.CompleteGroups = 0
                'If Group is complete, calculate all Groups
                For Each g In GameBoard.Groups
                    Dim prevOwner As Entity = Nothing
                    groupcomplete = False
                    For Each h As HouseField In g
                        If (prevOwner IsNot Nothing AndAlso Not h.Owner.Equals(prevOwner)) OrElse h.Owner.Equals(GameBoard.BANK) Then
                            groupcomplete = False
                            Exit For
                        Else
                            groupcomplete = True
                            prevOwner = h.Owner
                        End If
                    Next
                    If groupcomplete Then
                        GameBoard.Statistics.CompleteGroups += 1
                    End If
                Next
                GameBoard.Statistics.CompleteGroups += 1
            End If


            Diagnostics.Debug.WriteLineIf(GameBoard.Statistics.SoldHouseFields <> totalsold, $"HACK: SoldHouseFields irregular in ChangeOwner {GameBoard.Statistics.SoldHouseFields} <> {totalsold}")
            GameBoard.Statistics.SoldHouseFields = totalsold

        End Sub

    End Class
End Namespace
