using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DotNetPoly.Base.Players;
using DotNetPoly.Base.Fields;

namespace DotNetPoly.Base
{
    public class GameBoard
    {
        public GameBoard(string pGameBoardFile, Players.Player[] pPlayers)
        {
            var xmldoc = new XmlDocument();

            ushort contextCash;
            string contextName;
            eActionType contextAction;

            var fileVersion = new Version("0.0");

            xmldoc.Load(Environment.ExpandEnvironmentVariables(pGameBoardFile));

            Version.TryParse(xmldoc.SelectSingleNode("monopoly").Attributes["version"].InnerText, out fileVersion);

            if (fileVersion.Major < 6)
                throw new DotNetPolyException("Gameboard Version to low");

            Setting = new Settings(xmldoc.SelectSingleNode("monopoly"));

            foreach (XmlNode xmlfield in xmldoc.SelectNodes("monopoly/fields/*"))
            {
                contextName = xmlfield.Attributes["name"].InnerText;

                ushort.TryParse(xmlfield.Attributes["cash"].InnerText, out contextCash);

                switch (xmlfield.Name.ToLower())
                {
                    case "start":
                        FieldCol.Add(contextName, new StartField(contextCash, contextName));
                        break;
                    case "house":
                        FieldCol.Add(contextName, new HouseField(xmlfield, Setting));
                        break;
                    case "chance":
                        string[] actionText = xmlfield.Attributes["action"].InnerText.Split(',');
                        eActionType[] actions = new eActionType[actionText.Length-1];
                        for (int i = 0; i < actionText.Length - 1; i++)
                            if (!Enum.TryParse<eActionType>(actionText[i], out actions[i]))
                                System.Diagnostics.Debug.WriteLine($"field {contextName} has unknown action {xmlfield.Attributes.GetNamedItem("action")?.InnerText}");

                        FieldCol.Add(contextName, new ChanceField(contextName, actions, contextCash));
                        break;
                    case "station":
                        FieldCol.Add(contextName, new StationField(contextName, contextCash));
                        break;
                    case "jail":
                        if (Enum.TryParse<eActionType>(xmlfield.Attributes["action"].InnerText, out contextAction))
                            System.Diagnostics.Debug.WriteLine($"field {contextName} has unknown action {xmlfield.Attributes.GetNamedItem("action")?.InnerText}");
                        FieldCol.Add(contextName, new JailField(contextName, contextAction);
                        break;
                    default:
                        throw new DotNetPolyException($"Fieldtype {nameof(xmlfield.Name)}={xmlfield.Name} not supported");
                }

                if (Setting.UseWundertuete)
                    Wundertuete = new Players.Entity("Wundertuete", System.Drawing.Color.White);

                playerRank = pPlayers;
                Statistic = new Statistics(this);
            }
        }

        private Fields.FieldCollection FieldCol;
        private Settings Setting;
        private Statistics Statistic;

        Player[] playerRank;

        readonly Entity BANK = new Entity("BANK", System.Drawing.Color.White);
        readonly Entity Wundertuete = new Entity("Wundertuete", System.Drawing.Color.White);
        readonly Entity Festgeld = new Entity("Festfeld", System.Drawing.Color.White);

        Entity FreeParkingOwner, FreeJailOwner;





    }
}
