Namespace Fields

    Public Class HouseField
        Inherits BaseField

        Private _rent As Integer

        Sub New(pName As String, pCost As Integer)
            MyBase.New(pName)
            Cost = pCost
            _rent = pCost
        End Sub

        ReadOnly Property Cost As Integer

        ReadOnly Property Rent As Integer
            Get
                Return _rent * _UpgradeLevel
            End Get
        End Property

        ReadOnly Property UpgradeLevel As Integer = 1

        Friend Sub Buy(pPlayer As BasePlayer)
            Me.GameBoard.RaiseChangeOwner(Me, pPlayer)

            pPlayer.TransferMoney(Me.Owner, Me.Cost)
            ChangeOwner(pPlayer)

        End Sub

        Public ReadOnly Property UpgradeCost As Integer
            Get
                Return Math.Max(CInt(Me.Cost * _UpgradeLevel / 2), _UpgradeLevel) 'Cost >= 1
            End Get
        End Property

        Friend Sub Upgrade()
            Dim _owner = DirectCast(Owner, BasePlayer)
            Owner.TransferMoney(GameBoard.BANK, UpgradeCost)
            If _owner.IsPlayering Then
                DirectCast(Owner, BasePlayer).RaiseEventHouseUpgrade(Me)
                _UpgradeLevel += 1
            End If

        End Sub

        Friend Overrides Sub onMoveOn(pPlayer As BasePlayer)
            MyBase.onMoveOn(pPlayer)

            If Me.Owner.Equals(GameBoard.BANK) Then
                pPlayer.onDelegateControl(New eActionType() {eActionType.BuyHouse, eActionType.Pass}, New BaseField() {Me}, Nothing, 0)
            ElseIf Me.Owner.Equals(pPlayer) Then
                pPlayer.onDelegateControl(New eActionType() {eActionType.UpgradeHouse, eActionType.Pass}, New BaseField() {Me}, Nothing, 0)
            Else
                pPlayer.onDelegateControl(eActionType.PayRent, Me)

            End If
        End Sub

        Friend Sub ChangeOwner(pPlayer As Entity)
            Dim dbg1 As Integer = 0

            If Me._owner.Equals(GameBoard.BANK) Then
                GameBoard.Statistic.SoldHouseFields += 1
            Else
                GameBoard.Statistic.SoldHouseFields -= 1
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

            Diagnostics.Debug.WriteLineIf(GameBoard.Statistic.SoldHouseFields <> totalsold, $"HACK: SoldHouseFields irregular in ChangeOwner {GameBoard.Statistic.SoldHouseFields} <> {totalsold}")
            GameBoard.Statistic.SoldHouseFields = totalsold

        End Sub

    End Class
End Namespace
