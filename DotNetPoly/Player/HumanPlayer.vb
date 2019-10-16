Imports DotNetPoly.Fields

Public Class HumanPlayer
    Inherits BasePlayer


    Sub New(pName As String, pColor As System.Drawing.Color)
        MyBase.New(pName, pColor)
    End Sub

    Overrides Function DelegateDiceRoll() As DiceRollResult
        RaiseEvent DelegateControl(Me, New eActionType() {eActionType.DiceOnly}, Nothing, Nothing, 0)
        WaitChoosenAction.WaitOne()

        WaitChoosenAction.Reset()

        Return onRollDice()
    End Function

    Friend Overrides Sub onDelegateControl(pPossibleActions() As eActionType, pfields() As BaseField, pPlayers() As Entity, pCash As Integer)
        Dim choosenPossibility As eActionType
        Dim allPossibilities As New Collections.Generic.List(Of eActionType)(3)

        allPossibilities.AddRange(pPossibleActions)
        allPossibilities.Add(eActionType.GiveUp)

        If allPossibilities.Contains(eActionType.PayRent) Then
            If GameBoard.FreeParkingOwner.Equals(Me) Then
                allPossibilities.Add(eActionType.CardUseFreeParking)
            End If
        End If

        If allPossibilities.Contains(eActionType.JumpToJail) Then
            If GameBoard.FreeJailOwner.Equals(Me) Then
                allPossibilities.Add(eActionType.CardUseFreeJail)
            End If
        End If

        RaiseEvent DelegateControl(Me, allPossibilities.ToArray, pfields, pPlayers, pCash)

        WaitChoosenAction.WaitOne()

        WaitChoosenAction.Reset()

        Try
            choosenPossibility = allPossibilities.ToArray(choosenActionIndex - 1)
        Catch ex As Exception
            Console.WriteLine("choosenActionIndex {0} is not in list {1}", choosenActionIndex, String.Join(",", allPossibilities))
            choosenPossibility = allPossibilities.ToArray(0)
        End Try

        invokeAction(choosenPossibility, pfields, pPlayers, pCash)
    End Sub

    Overrides Sub SubmitChoosenAction(pIndex As Integer, pObject As Object)
        choosenActionIndex = pIndex
        WaitChoosenAction.Set()
    End Sub

    Event DelegateControl(pPlayer As BasePlayer, pPossibleActions() As eActionType, pFields As BaseField(), pPlayer() As Entity, pCash As Integer)


End Class
