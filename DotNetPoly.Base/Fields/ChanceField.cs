using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetPoly.Base.Players;

namespace DotNetPoly.Base.Fields
{
    class ChanceField : Field
    {
        eActionType[] _chanceTypes;
        ushort _chanceCash;

        static Random ChanceRandomizer = new Random();

        public ChanceField(string pName, eActionType[] pChanceType, ushort pCash) : base(pName)
        {
            _chanceTypes = pChanceType;
            _chanceCash = pCash;
        }
        public ChanceField(string pName, eActionType pChanceType, ushort pCash) 
            : this(pName, new eActionType[] { pChanceType }, pCash) { }

        internal override void onMoveOn(Player pPlayer)
        {
            foreach (var chance in _chanceTypes)
            {
                base.onMoveOn(pPlayer);

                eActionType chossenChance;
                Field contextField;
                Entity contextPlayer;
                ushort contextCash;

                if (_chanceCash == 0)
                    contextCash = ChanceRandomizer.Next(1, GameBoard.Settings.DICE_COUNT * GameBoard.Settings.DICE_EYES);
                else
                    contextCash = _chanceCash;

                if (chance == eActionType.Random)
                    chossenChance = GameBoard.Settings.Chances(ChanceRandomizer.Next(0, GameBoard.Settings.Chances.Length - 1));
                else
                    chossenChance = chance;

                switch (chossenChance)
                {
                    case eActionType.JumpToJail:
                        pPlayer.raiseChance(chossenChance, new object[] { GameBoard.JailField });
                        eActionType[] posActions;

                        if (GameBoard.FreeJailOwner.Equals(pPlayer))
                            posActions = new eActionType[] { chossenChance, eActionType.CardUseFreeJail };
                        else
                            posActions = new eActionType[] { chossenChance };

                        pPlayer.onDelegateControl(new DelegateEventArgs(posActions, new Entity[] { pPlayer }, new Field[] { GameBoard.JailField }, 0));
                        break;
                    default:
                        break;
                }
                //'If choosenChance = eActionType.PayRent Then Stop

                //'TODO: define exactly who performs action, sub chance or sub Delegate
            }

        }
    }
}
