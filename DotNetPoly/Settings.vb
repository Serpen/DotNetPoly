Public Class Settings

    Friend Sub New(pxmlnode As Xml.XmlNode)
        'HACK: ON ERROR
        On Error Resume Next
        Integer.TryParse(pxmlnode.SelectSingleNode("settings/initialcash").InnerText, START_CAPITAL)
        Integer.TryParse(pxmlnode.SelectSingleNode("settings/dicecount").InnerText, DICE_COUNT)
        Integer.TryParse(pxmlnode.SelectSingleNode("settings/diceeyes").InnerText, DICE_EYES)
        Integer.TryParse(pxmlnode.SelectSingleNode("settings/maxdoubles").InnerText, MAX_DOUBLES)
        Boolean.TryParse(pxmlnode.SelectSingleNode("settings/showendturn").InnerText, END_TURN_EVENT)
        [Enum].TryParse(pxmlnode.SelectSingleNode("settings/jailaction").InnerText, JAIL_ACTION)
        Integer.TryParse(pxmlnode.SelectSingleNode("settings/jailaction").Attributes("times").InnerText, JAIL_TIMES)

        [Enum].TryParse(pxmlnode.SelectSingleNode("settings/upgrade/upgrade").InnerText, CostUpgradeMethode)
        [Enum].TryParse(pxmlnode.SelectSingleNode("settings/upgrade/rent").InnerText, RentUpgradeMethode)

        ReDim Chances(pxmlnode.SelectSingleNode("chances").ChildNodes.Count - 1)

        Dim nodeList = pxmlnode.SelectNodes("chances/*")
        For i = 0 To nodeList.Count
            [Enum].TryParse(nodeList(i).InnerText, Chances(i))
        Next
    End Sub

    Public ReadOnly Property DICE_COUNT As Integer = 1
    Public ReadOnly Property DICE_EYES As Integer = 6
    Public ReadOnly Property START_CAPITAL As Integer
    Public ReadOnly Property MAX_DOUBLES As Integer = 2
    Public ReadOnly Property END_TURN_EVENT As Boolean = False
    Public ReadOnly Property JAIL_ACTION As eActionType = eActionType.LoseTurn
    Public ReadOnly Property JAIL_TIMES As Integer = 1

    Public ReadOnly Property UpgradePreReq As Fields.HouseField.eUpgradeAlgorithm
    Public ReadOnly Property CostUpgradeMethode As Fields.HouseField.eUpgradeAlgorithm
    Public ReadOnly Property RentUpgradeMethode As Fields.HouseField.eUpgradeAlgorithm

    ReadOnly Property Chances As eActionType()

End Class
