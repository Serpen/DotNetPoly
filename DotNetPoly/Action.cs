
namespace DotNetPoly
{
    class Action
    {
        public eActionType Type { get; set; }
        public Entity PlayerSource { get; set; }
        public Entity[] PlayerDests { get; set; }
        public uint Cash { get; set; }
        public Fields.Field[] Fields { get; set; }
        public NotificationEventDelgate NotificationEvent;

        public delegate void NotificationEventDelgate(object sender, System.EventArgs e);

    }

    class Test_Action
    {
        void Test()
        {
            var p = new HumanPlayer("Test", System.Drawing.Color.Red);
            var f = new Fields.JailField("Jail", eActionType.JumpToJail);

            var a = new Action() {
                Type = eActionType.JumpToJail,
                PlayerSource = p,
                Fields = new Fields.Field[] { f },
                NotificationEvent = p.OnMove,
            };
        }
    }
}
