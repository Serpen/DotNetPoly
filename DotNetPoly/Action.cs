
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
}
