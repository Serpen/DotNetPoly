namespace DotNetPoly
{
    public class BotPlayer : Player
    {
        public BotPlayer(string pName, System.Drawing.Color pColor) : base(pName, pColor) { }

        public override DiceRollResult DelegateDiceRoll()
        {
            return onRollDice();
        }

        internal override void onDelegateControl(DelegateEventArgs e)
        {
            for (var i = 0; i < e.Actions.Length; i++)
            {
                if (e.Actions[i] == eActionType.BuyHouse)
                {
                    if (this.Cash > ((Fields.HouseField)e.Fields[0]).Cost)
                    {
                        e.ChoosenAction = e.Actions[i];
                        break;
                    }
                    else
                        e.ChoosenAction = e.Actions[i];
                }
            }
            if (e.ChoosenAction == eActionType.None)
                e.ChoosenAction = e.Actions[0];

            if (invokeAction(e))
                onDelegateControl(e);
        }
    }
}
