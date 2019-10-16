
namespace DotNetPoly
{
    public class DiceRollResult
    {
        private static System.Random DiceRandomizer = new System.Random();

        internal DiceRollResult(GameBoard pGameboard)
        {
            bool? tmpisDoubles = new bool();

            Dices = new byte[pGameboard.Settings.DICE_COUNT - 1 + 1];
            for (var i = 0; i < pGameboard.Settings.DICE_COUNT; i++)
            {
                Dices[i] = System.Convert.ToByte(DiceRandomizer.Next(1, pGameboard.Settings.DICE_EYES + 1));
                DiceSum += Dices[i];

                if (i > 0)
                {
                    if ((!tmpisDoubles.HasValue || tmpisDoubles.Value == true) & (Dices[i - 1] == Dices[i]))
                        tmpisDoubles = true;
                    else
                        tmpisDoubles = false;
                }
            }

            if (tmpisDoubles.Value)
            {
                IsDoubles = true;
                pGameboard.Statistics.DoublesCount += 1;
            }

            pGameboard.Statistics.TotalDiceCount += 1;
            pGameboard.Statistics.DiceRollSums[DiceSum] += 1;
        }

        public override string ToString()
        {
            if (Dices.Length > 1)
                return string.Format("{0}={1} ({2})", string.Join("+", Dices), DiceSum, IsDoubles);
            else
                return string.Format("{0}", Dices[0]);
        }

        public byte[] Dices { get; }
        public byte DiceSum { get; }
        public bool IsDoubles { get; }
    }
}
