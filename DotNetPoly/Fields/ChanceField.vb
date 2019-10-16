Namespace Fields
    Public Class ChanceField
        Inherits BaseField

        Private Shared ChanceRandomizer As New Random()
        Private _chanceType As eActionType
        Private _chanceCash As Integer = -1

        Sub New(pName As String, pChanceType As eActionType, pCash As Integer)
            MyBase.New(pName)
            _chanceType = pChanceType
            _chanceCash = pCash
        End Sub

        Friend Overrides Sub onMoveOn(pPlayer As BasePlayer)
            MyBase.onMoveOn(pPlayer)

            Dim choosenChance As eActionType
            Dim contextField As BaseField
            Dim contextPlayer As Entity

            Dim contextCash As Integer

            If _chanceCash = -1 Then
                contextCash = ChanceRandomizer.Next(1, GameBoard.Settings.DICE_COUNT * GameBoard.Settings.DICE_EYES)
            Else
                contextCash = _chanceCash
            End If

            If _chanceType = eActionType.Random Then
                choosenChance = GameBoard.Settings.Chances(ChanceRandomizer.Next(0, GameBoard.Settings.Chances.Length - 1))
            Else
                choosenChance = _chanceType
            End If

            'If choosenChance = eActionType.PayRent Then Stop

            'TODO: define exactly who performs action, sub chance or sub Delegate

            Select Case choosenChance
                Case eActionType.JumpToJail
                    pPlayer.RaiseChance(choosenChance, New Object() {GameBoard.JailField})
                    Dim posActions As eActionType()

                    If GameBoard.FreeJailOwner.Equals(pPlayer) Then
                        ReDim posActions(1)
                        posActions(0) = choosenChance
                        posActions(1) = eActionType.CardUseFreeJail
                    Else
                        ReDim posActions(0)
                        posActions(0) = choosenChance
                    End If

                    pPlayer.onDelegateControl(posActions, New BaseField() {GameBoard.JailField}, New Entity() {pPlayer}, 0)


                Case eActionType.GoToField
                    Do
                        contextField = GameBoard.Fields(ChanceRandomizer.Next(0, GameBoard.FieldCount))
                    Loop Until contextField.gettype() IsNot GetType(JailField)
                    pPlayer.RaiseChance(choosenChance, New Object() {contextField})

                    'If contextField.Equals(GameBoard.JailField) Then Stop
                    pPlayer.onDelegateControl(eActionType.GoToField, contextField)


                Case eActionType.JumpToField
                    Do
                        contextField = GameBoard.Fields(ChanceRandomizer.Next(0, GameBoard.FieldCount))
                    Loop Until contextField.GetType() IsNot GetType(JailField)

                    contextField = GameBoard.Fields(ChanceRandomizer.Next(0, GameBoard.FieldCount))

                    pPlayer.RaiseChance(choosenChance, New Object() {contextField})

                    pPlayer.onDelegateControl(choosenChance, contextField)


                Case eActionType.JumpToRelativField
                    Dim relativJump As Integer = ChanceRandomizer.Next(-GameBoard.Settings.DICE_COUNT * GameBoard.Settings.DICE_EYES, GameBoard.Settings.DICE_COUNT * GameBoard.Settings.DICE_EYES)
                    relativJump = pPlayer.Position + relativJump
                    If relativJump < 0 Then
                        relativJump = GameBoard.FieldCount + 1 - relativJump
                    End If
                    contextField = GameBoard.Fields(relativJump)

                    pPlayer.RaiseChance(choosenChance, New Object() {contextField})
                    pPlayer.onDelegateControl(choosenChance, contextField)


                Case eActionType.PayToStack
                    pPlayer.RaiseChance(choosenChance, New Object() {contextCash})
                    pPlayer.onDelegateControl(eActionType.PayToStack, GameBoard.Wundertuete, contextCash)


                Case eActionType.CollectStack
                    Dim wundertuete = GameBoard.Wundertuete.Cash

                    pPlayer.RaiseChance(choosenChance, New Object() {wundertuete})

                    pPlayer.onDelegateControl(eActionType.CollectStack, GameBoard.Wundertuete, wundertuete)


                Case eActionType.CashToBank
                    pPlayer.RaiseChance(choosenChance, New Object() {contextCash})
                    pPlayer.onDelegateControl(eActionType.CashToBank, GameBoard.BANK, contextCash)



                Case eActionType.CashFromBank
                    pPlayer.RaiseChance(choosenChance, New Object() {contextCash})
                    pPlayer.onDelegateControl(eActionType.CashFromBank, GameBoard.BANK, contextCash)


                Case eActionType.CashToPlayer
                    Do
                        contextPlayer = GameBoard.PlayerRank(ChanceRandomizer.Next(0, GameBoard.PlayerRank.Length)) 'to Player
                    Loop Until DirectCast(contextPlayer, BasePlayer).IsPlayering AndAlso Not pPlayer.Equals(contextPlayer) 'Todo: MoveTo Player/Gameboard

                    pPlayer.RaiseChance(choosenChance, New Object() {contextCash})

                    pPlayer.onDelegateControl(eActionType.CashToPlayer, contextPlayer, contextCash)


                Case eActionType.CashFromPlayer
                    Do
                        contextPlayer = GameBoard.PlayerRank(ChanceRandomizer.Next(0, GameBoard.PlayerRank.Length)) 'to Player
                    Loop Until DirectCast(contextPlayer, BasePlayer).IsPlayering AndAlso Not pPlayer.Equals(contextPlayer) 'Todo: MoveTo Player/Gameboard

                    pPlayer.RaiseChance(choosenChance, New Object() {contextCash})

                    pPlayer.onDelegateControl(eActionType.CashFromPlayer, contextPlayer, contextCash)


                Case eActionType.CashFromAll

                    pPlayer.RaiseChance(choosenChance, New Object() {contextCash})

                    pPlayer.onDelegateControl(eActionType.CashFromAll, contextCash)


                Case eActionType.PayPerHouse
                    pPlayer.RaiseChance(choosenChance, New Object() {contextCash})

                    contextCash = contextCash * pPlayer.OwnFields.Length
                    pPlayer.onDelegateControl(eActionType.CashToBank, GameBoard.BANK, contextCash)

                Case eActionType.AddMove, eActionType.Move 'TODO: move handled?
                    pPlayer.RaiseChance(choosenChance, New Object() {})

                    pPlayer.onDelegateControl(eActionType.AddMove)


                Case eActionType.None, eActionType.DiceOnly, eActionType.WaitingForTurn, eActionType.Pass, eActionType.LoseTurn, eActionType.GiveUp, eActionType.EndTurn
                    pPlayer.RaiseChance(choosenChance, New Object() {})

                    pPlayer.onDelegateControl(eActionType.Pass)

                Case eActionType.UpgradeHouse
                    Dim houseindex As Integer

                    If pPlayer.OwnFields.Length > 0 Then
                        houseindex = ChanceRandomizer.Next(0, pPlayer.OwnFields.Length)
                        pPlayer.RaiseChance(choosenChance, New Object() {pPlayer.OwnFields(houseindex)})
                        pPlayer.onDelegateControl(eActionType.UpgradeHouse, pPlayer.OwnFields(houseindex))

                    End If

                Case eActionType.BuyHouse
                    Dim houseindex As Integer
                    Dim housecounter, fieldcounter As Integer

                    If GameBoard.Statistics.AvailableHouseFields > 0 Then
                        houseindex = ChanceRandomizer.Next(0, GameBoard.Statistics.AvailableHouseFields - 1)
                        Do
                            If GameBoard.HouseFields(fieldcounter).Owner.Equals(GameBoard.BANK) Then
                                If housecounter = houseindex Then
                                    pPlayer.RaiseChance(choosenChance, New Object() {GameBoard.HouseFields(fieldcounter)})
                                    pPlayer.onDelegateControl(eActionType.BuyHouse, GameBoard.HouseFields(fieldcounter))
                                End If
                                housecounter += 1
                            End If

                            fieldcounter += 1
                        Loop Until housecounter > houseindex
                    End If

                Case eActionType.PayRent
                    Dim houseindex As Integer
                    Dim housecounter As Integer = 0
                    Dim fieldcounter As Integer = 0
                    Dim posActions As eActionType()

                    If GameBoard.Statistics.SoldHouseFields > 0 Then
                        houseindex = ChanceRandomizer.Next(0, GameBoard.Statistics.SoldHouseFields - 1)
                        If GameBoard.FreeParkingOwner.Equals(pPlayer) Then
                            ReDim posActions(1)
                            posActions(0) = choosenChance
                            posActions(1) = eActionType.CardUseFreeParking
                        Else
                            ReDim posActions(0)
                            posActions(0) = choosenChance
                        End If
                        Do
                            If Not GameBoard.HouseFields(fieldcounter).Owner.Equals(GameBoard.BANK) Then
                                If housecounter = houseindex Then
                                    contextField = GameBoard.HouseFields(fieldcounter)
                                    pPlayer.RaiseChance(choosenChance, New Object() {GameBoard.Fields(fieldcounter)})
                                    pPlayer.onDelegateControl(posActions, New BaseField() {contextField}, New BasePlayer() {pPlayer}, DirectCast(contextField, HouseField).Rent)

                                End If
                                housecounter += 1
                            End If

                            fieldcounter += 1
                        Loop Until housecounter > houseindex
                    End If

                Case eActionType.CardGetFreeParking
                    pPlayer.RaiseChance(choosenChance, New Object() {GameBoard.FreeParkingOwner})
                    pPlayer.onDelegateControl(choosenChance)

                Case eActionType.CardGetFreeJail
                    pPlayer.RaiseChance(choosenChance, New Object() {GameBoard.FreeJailOwner})
                    pPlayer.onDelegateControl(choosenChance)

                Case Else
                    pPlayer.RaiseChance(choosenChance, New Object() {})
                    Diagnostics.Debug.Print("Spieler {0} auf Feld {1} bekommt Chance {2}", pPlayer, pPlayer.PositionField, choosenChance)


            End Select

            GameBoard.Statistics.ChanceOccured(choosenChance) = CInt(GameBoard.Statistics.ChanceOccured(choosenChance)) + 1
        End Sub
    End Class


    Public Class StationField
        Inherits ChanceField

        Sub New(pName As String, pCash As Integer)
            MyBase.New(pName, eActionType.GoToField, pCash)
        End Sub
    End Class
End Namespace