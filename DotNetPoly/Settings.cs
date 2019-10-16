using System;

namespace DotNetPoly
{
    public class Settings
    {
        internal Settings(System.Xml.XmlNode pxmlnode)
        {

            //HACK: ON ERROR
            //On Error Resume Next

                ushort.TryParse(pxmlnode.SelectSingleNode("settings/initialcash").InnerText, out _sTART_CAPITAL);
                byte.TryParse(pxmlnode.SelectSingleNode("settings/dicecount").InnerText, out _dICE_COUNT);
                byte.TryParse(pxmlnode.SelectSingleNode("settings/diceeyes").InnerText, out _dICE_EYES);
                byte.TryParse(pxmlnode.SelectSingleNode("settings/maxdoubles")?.InnerText, out _mAX_DOUBLES);
                bool.TryParse(pxmlnode.SelectSingleNode("settings/showendturn")?.InnerText, out _eND_TURN_EVENT);
                Enum.TryParse(pxmlnode.SelectSingleNode("settings/jailaction").InnerText, out _jAIL_ACTION);
                byte.TryParse(pxmlnode.SelectSingleNode("settings/jailaction").Attributes["times"].InnerText, out _jAIL_TIMES);

                byte.TryParse(pxmlnode.SelectSingleNode("settings/FixedDepositePercentage")?.InnerText, out FixedDepositePercentage);

                Enum.TryParse(pxmlnode.SelectSingleNode("settings/upgrade/upgrade").InnerText, out _costUpgradeMethode);
                Enum.TryParse(pxmlnode.SelectSingleNode("settings/upgrade/rent").InnerText, out _rentUpgradeMethode);
                int.TryParse(pxmlnode.SelectSingleNode("settings/upgrade/rent")?.Attributes["factor"]?.InnerText, out _defaultRentUpgradeFactor);
                Enum.TryParse(pxmlnode.SelectSingleNode("settings/upgrade/when").InnerText, out _upgradeGroupMode);

                bool.TryParse(pxmlnode.SelectSingleNode("settings/Credits").InnerText, out _useCredits);
                bool.TryParse(pxmlnode.SelectSingleNode("settings/Wundertuete").InnerText, out _useWundertuete);
                bool.TryParse(pxmlnode.SelectSingleNode("settings/Auctions").InnerText, out _useAuctions);

                Chances = new eActionType[pxmlnode.SelectSingleNode("chances").ChildNodes.Count - 1 + 1];

                var nodeList = pxmlnode.SelectNodes("chances/*");
                for (var i = 0; i < nodeList.Count; i++)
                    Enum.TryParse(nodeList?[i].InnerText, out Chances[i]);
            
            
        }

        public byte DICE_COUNT => _dICE_COUNT;

        public byte DICE_EYES => _dICE_EYES;
        public ushort START_CAPITAL => _sTART_CAPITAL;
        public byte MAX_DOUBLES => _mAX_DOUBLES;
        public bool END_TURN_EVENT => _eND_TURN_EVENT;
        public eActionType JAIL_ACTION => _jAIL_ACTION;
        public byte JAIL_TIMES => _jAIL_TIMES;

        public Fields.HouseField.eUpgradeAlgorithm CostUpgradeMethode => _costUpgradeMethode;
        public Fields.HouseField.eUpgradeAlgorithm RentUpgradeMethode => _rentUpgradeMethode;

        public Fields.HouseField.eUpgradeAlgorithm UpgradeGroupMode => _upgradeGroupMode;

        public bool UseCredits => _useCredits;
        public bool UseWundertuete => _useWundertuete;
        public bool UseAuctions => _useAuctions;

        public int DefaultRentUpgradeFactor => _defaultRentUpgradeFactor;        
        // Public ReadOnly Property DefaultRentUpgradeSummand As Integer

        public readonly byte FixedDepositePercentage;
        private readonly byte _dICE_COUNT = 1;
        private readonly byte _dICE_EYES = 6;
        private readonly byte _mAX_DOUBLES = 2;
        private readonly eActionType _jAIL_ACTION = eActionType.LoseTurn;
        private readonly ushort _sTART_CAPITAL;
        private readonly bool _eND_TURN_EVENT = false;
        private readonly byte _jAIL_TIMES = 1;
        private readonly Fields.HouseField.eUpgradeAlgorithm _costUpgradeMethode;
        private readonly Fields.HouseField.eUpgradeAlgorithm _rentUpgradeMethode;
        private readonly Fields.HouseField.eUpgradeAlgorithm _upgradeGroupMode;
        private readonly bool _useCredits;
        private readonly bool _useWundertuete;
        private readonly bool _useAuctions;
        private readonly int _defaultRentUpgradeFactor;

        public eActionType[] Chances { get; }
    }
}
