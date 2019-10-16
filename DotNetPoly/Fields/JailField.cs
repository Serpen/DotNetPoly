namespace DotNetPoly
{
    namespace Fields
    {
        public class JailField : Field
        {
            private eActionType _actionType;

            public JailField(string pName, eActionType pActionType) : base(pName)
            {
                _actionType = pActionType;
            }

            internal void GoToJail(Player pPlayer)
            {
                pPlayer.Position = Index;
                var loopTo = GameBoard.Settings.JAIL_TIMES;
                for (var i = 1; i <= loopTo; i++)
                {
                    pPlayer.NextAction.Enqueue(eActionType.WaitingForTurn);
                    switch (GameBoard.Settings.JAIL_ACTION)
                    {
                        default:
                            {
                                pPlayer.NextAction.Enqueue(eActionType.LoseTurn);
                                break;
                            }
                    }
                }
            }

            internal override void onMoveOn(Player pPlayer)
            {
                base.onMoveOn(pPlayer);

                switch (_actionType)
                {
                    case eActionType.None:
                        {
                            break;
                        }

                    case eActionType.JumpToJail:
                        {
                            pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.JumpToJail));
                            break;
                        }
                }
            }
        }
    }
}
