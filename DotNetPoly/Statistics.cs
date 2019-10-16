using System;
using System.Linq;

namespace DotNetPoly
{
    partial class GameBoard
    {
        protected internal StatisticsClass Statistics;

        protected internal class StatisticsClass
        {
            private GameBoard _Gameboard;

            public StatisticsClass(GameBoard pGameboard)
            {
                _Gameboard = pGameboard;
                DiceRollSums = new int[pGameboard.Settings.DICE_COUNT * pGameboard.Settings.DICE_EYES + 1];
                FieldVisits = new int[pGameboard.FieldCount - 1 + 1];
                FieldHovers = new int[pGameboard.FieldCount - 1 + 1];
                // ReDim ChanceOccured([Enum].GetNames(GetType(eChanceType)).Length - 1)

                ChanceOccured = new System.Collections.Hashtable(Enum.GetNames(typeof(eActionType)).Length - 1);
                ActionChoosen = new System.Collections.Hashtable(Enum.GetNames(typeof(eActionType)).Length - 1);

                TotalHousefields = (from f in pGameboard.FieldCol where f.Value is Fields.HouseField select f.Value).Count();

                StartTime = DateTime.Now;
            }

            internal int TotalDiceCount;
            internal int[] DiceRollSums;
            internal int DoublesCount;

            internal int TotalEyes
            {
                get
                {
                    int erg = 0;
                    for (int i = 0; i < DiceRollSums.GetLength(0); i++)
                        erg += DiceRollSums[i] * i;
                    return erg;
                }
            }

            internal int AverageDiceSum
            {
                get
                {
                    return Convert.ToInt32(TotalEyes / (double)TotalDiceCount);
                }
            }

            internal readonly int TotalHousefields;
            internal int AvailableHouseFields => TotalHousefields - SoldHouseFields;

            internal int SoldHouseFields { get; set; }

            internal int CreditHouses { get; set; }

            internal int CompleteGroups;

            internal int[] FieldVisits;
            internal int[] FieldHovers;

            internal System.Collections.Hashtable ChanceOccured;
            internal System.Collections.Hashtable ActionChoosen;

            internal int TotalCash => _Gameboard.BANK.Cash;
  
            private readonly DateTime StartTime;

            public TimeSpan PlayTime => DateTime.Now - StartTime;

            internal int MaxUpgradeLevel;
            internal int MaxRent;
            internal ushort MaxCost;

            internal int Lap;
        }
    }
}
