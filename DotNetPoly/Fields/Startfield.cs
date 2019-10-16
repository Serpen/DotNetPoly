namespace DotNetPoly
{
    namespace Fields
    {
        public class Startfield : Field
        {
            public Startfield(ushort pStartMoney, string pName) : base(pName)
            {
                StartCash = pStartMoney;
            }

            public ushort StartCash { get; }

            internal override void onMoveOn(Player pPlayer)
            {
                base.onMoveOn(pPlayer);
                helper(pPlayer, 2);
            }

            internal override void onMoveOver(Player pPlayer)
            {
                base.onMoveOver(pPlayer);
                helper(pPlayer, 1);
            }

            private void helper(Player pPlayer, int cashMulitiply)
            {
                base.onMoveOver(pPlayer);

                uint cash;
                if (GameBoard.Settings.FixedDepositePercentage > 0)
                    cash = System.Convert.ToUInt32(cashMulitiply * StartCash + pPlayer.Festgeld * GameBoard.Settings.FixedDepositePercentage / 100d);
                else
                    cash = System.Convert.ToUInt32(cashMulitiply * StartCash);

                GameBoard.BANK.TransferMoney(pPlayer, cash);
            }
        }
    }
}
