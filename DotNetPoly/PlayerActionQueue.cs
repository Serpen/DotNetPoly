namespace DotNetPoly
{
    class PlayerActionQueue
    {
        PlayerActionQueue(Player p, eActionType[] newactions)
        {
            player = p;
            actions = new System.Collections.Generic.List<eActionType>(newactions);
            actions.Add(eActionType.GiveUp);
        }

        PlayerActionQueue(Player p) : this(p, new eActionType[] { }) { }

        private Player player;
        private System.Collections.Generic.List<eActionType> actions;

        void Add(eActionType a)
        {
            if (!actions.Contains(a))
                actions.Add(a);
        }

        public eActionType[] getPossibleActions()
        {
           

            return actions.ToArray();
        }
    }

    
}
