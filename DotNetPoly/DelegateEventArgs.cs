namespace DotNetPoly
{
    public class DelegateEventArgs
    {
        public DelegateEventArgs(eActionType[] pPossibleAction, Entity[] pPlayers, Fields.Field[] pFields, uint pCash, bool pAllowChooseAction, bool pAllowChoosePlayers, bool pAllowChooseFields, bool pAllowChooseCash)
        {
            _actions = pPossibleAction;
            _players = pPlayers;
            _fields = pFields;
            _cash = pCash;

            _AllowActionChoose = pAllowChooseAction;
            _AllowCashChoose = pAllowChooseCash;
            _AllowFieldChoose = pAllowChooseFields;
            _AllowPlayerChoose = pAllowChoosePlayers;
        }

        public DelegateEventArgs(eActionType[] pPossibleAction, Entity[] pPlayers, Fields.Field[] pFields, uint pCash) : this(pPossibleAction, pPlayers, pFields, pCash, true, false, false, false)
        {
        }

        public DelegateEventArgs(eActionType pPossibleAction) : this(new eActionType[] { pPossibleAction }, null, null, 0)
        {
        }

        public DelegateEventArgs(eActionType[] pPossibleActions) : this(pPossibleActions, null, null, 0)
        {
        }

        public DelegateEventArgs(eActionType pPossibleAction, Fields.Field pField) : this(new eActionType[] { pPossibleAction }, null, new Fields.Field[] { pField }, 0)
        {
        }
        public DelegateEventArgs(eActionType[] pPossibleAction, Fields.Field pField) : this(pPossibleAction, null, new Fields.Field[] { pField }, 0)
        {
        }

        public DelegateEventArgs(eActionType pPossibleAction, Fields.Field pField, uint pCash) : this(new eActionType[] { pPossibleAction }, null, new Fields.Field[] { pField }, pCash)
        {
        }

        public DelegateEventArgs(eActionType pPossibleAction, Entity pPlayer, uint pCash) : this(new eActionType[] { pPossibleAction }, new Entity[] { pPlayer }, null, pCash)
        {
        }

        public DelegateEventArgs(eActionType pPossibleAction, uint pCash) : this(new eActionType[] { pPossibleAction }, null, null, pCash)
        {
        }

        internal DelegateEventArgs Copy()
        {
            return new DelegateEventArgs(_actions, _players, _fields, _cash);
        }



        private readonly bool  _AllowActionChoose = true;
        private readonly bool _AllowFieldChoose;
        private readonly bool _AllowPlayerChoose;
        private readonly bool _AllowCashChoose;

        private eActionType[] _actions;
        public eActionType[] Actions
        {
            get
            {
                return _actions;
            }
            internal set
            {
                _actions = value;
            }
        }

        private readonly Entity[] _players;
        public Entity[] Players
        {
            get
            {
                return _players;
            }
            set
            {
                throw new DotNetPolyException("choosePlayer is not permitted");
            }
        }

        private Fields.Field[] _fields;
        public Fields.Field[] Fields
        {
            get
            {
                return _fields;
            }
            set
            {
                switch (ChoosenAction)
                {
                    case eActionType.CreditForHouse:
                    case eActionType.CreditReleaseHouse:
                        {
                            _fields = value;
                            break;
                        }

                    case eActionType.UpgradeHouse:
                        {
                            _fields = value;
                            break;
                        }

                    default:
                        {
                            throw new DotNetPolyException($"Field Change is not permitted for Action {ChoosenAction}");
                        }
                }
            }
        }

        private uint _cash;
        public uint Cash
        {
            get
            {
                return _cash;
            }
            set
            {
                switch (ChoosenAction)
                {
                    case eActionType.IncreaseFixedDeposite:
                        {
                            _cash = value;
                            break;
                        }

                    default:
                        {
                            throw new DotNetPolyException($"change Cash is not permitted for action {ChoosenAction}");
                            
                        }
                }
            }
        }

        private eActionType _choosenAction;
        public eActionType ChoosenAction
        {
            get
            {
                return _choosenAction;
            }
            set
            {
                if (_AllowActionChoose)
                {
                    for (var i = 0; i < Actions.Length; i++)
                    {
                        if (Actions[i] == value)
                        {
                            _choosenAction = value;
                            return;
                        }
                    }
                    throw new DotNetPolyException($"{_choosenAction} is not in {nameof(Actions)}");
                }
                else
                    throw new DotNetPolyException("chooseAction is not permitted");
            }
        }
    }
}
