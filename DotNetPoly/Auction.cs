namespace DotNetPoly
{
    public class Auction
    {
        internal Auction(Fields.HouseField pHouse, Player pInitiator)
        {
            House = pHouse;
            _initiator = pInitiator;

            _activeBidders = System.Convert.ToByte(pHouse.GameBoard.PlayerRank.Length);

            _PlayerBids = new uint[_activeBidders - 1 + 1];
        }

        private Player _initiator;

        private uint[] _PlayerBids;
        public uint HighestBid { get; private set; }


        private byte _bidTurn;
        private byte _activeBidders;

        public bool Finished { get; private set; }

        public void setBid(Player pPlayer, uint pBid)
        {
            if (pPlayer.Equals(_initiator))
            {
                _bidTurn += System.Convert.ToByte(1);

                if (_activeBidders < 2)
                {
                    Finished = true;
                    sellToHighestBidPlayer();
                    return;
                }
                _activeBidders = 0;
            }

            if (!(pBid > HighestBid))
            {
            }
            else
            {
                _activeBidders += System.Convert.ToByte(1);
                _PlayerBids[pPlayer.Index] = pBid;
                HighestBid = pBid;
            }
        }

        public void sellToHighestBidPlayer()
        {
            Player p = null;
            for (var i = 0; i <= _PlayerBids.Length; i++)
            {
                if (_PlayerBids[i] == HighestBid)
                {
                    p = House.GameBoard.PlayerRank[i];
                    break;
                }
            }
            if (p != null) 
                House.Buy(p, HighestBid);
        }

        private void ModifyActionQueue()
        {
            foreach (var p in House.GameBoard.PlayerRank)
            {
                System.Diagnostics.Debug.WriteLine("Befor:" + string.Join(",", p.NextAction));
                System.Collections.Generic.Queue<eActionType> actionTempQueue = new System.Collections.Generic.Queue<eActionType>();

                // If p.NextAction.Count > 0 Then Stop

                actionTempQueue.Enqueue(eActionType.WaitingForTurn);
                actionTempQueue.Enqueue(eActionType.Auction);

                while (p.NextAction.Count > 0)
                    actionTempQueue.Enqueue(p.NextAction.Dequeue());


                do
                    p.NextAction.Enqueue(actionTempQueue.Dequeue());
                while (actionTempQueue.Count > 0);
                System.Diagnostics.Debug.WriteLine("After:" + string.Join(",", p.NextAction));
            }
        }

        internal void PerformAction(Player pPlayer)
        {
            if (pPlayer.Equals(_initiator))
            {
                if (_activeBidders < 2)
                    Finished = true;
                else
                    ModifyActionQueue();
            }

            pPlayer.raiseAuctionBid();
        }


        public Fields.HouseField House { get; }
    }
}
