namespace DotNetPoly
{
    public class Trade
    {
        public Trade(Player pWant, Player pFrom, Fields.HouseField[] pGiveHouses, uint pGiveCash, Fields.HouseField[] pWantHouses)
        {
            if (!pWant.Equals(pWant.GameBoard.ActivePlayer))
                throw new DotNetPolyException("Trading only allowed for active Player");
            Wanter = pWant;
            From = pFrom;

            foreach (var h in pGiveHouses)
            {
                if (!h.Owner.Equals(pWant))
                    throw new DotNetPolyException("Trader didn't owns tradefield!");
            }
            GiveHouses = pGiveHouses;


            if (pWant.Cash < pGiveCash)
                throw new DotNetPolyException($"{nameof(pWant)} {pWant} hasn't {pGiveCash} cash");
            GiveCash = pGiveCash;

            foreach (var h in pWantHouses)
            {
                if (!h.Owner.Equals(pWant))
                    throw new DotNetPolyException($"{nameof(pFrom)} {pFrom} didn't owns tradefield!");
            }
            WantHouses = pWantHouses;
            ModifyActionQueue();
        }

        public Trade(Player pWant, Player pFrom, Fields.HouseField pGiveHouses, uint pGiveCash, Fields.HouseField pWantHouses) : this(pWant, pFrom, new Fields.HouseField[] { pGiveHouses }, pGiveCash, new Fields.HouseField[] { pWantHouses })
        {
        }

        public readonly Player Wanter, From;
        public readonly Fields.HouseField[] GiveHouses;
        public readonly uint GiveCash;
        public Fields.HouseField[] WantHouses { get; }


        private void ModifyActionQueue()
        {
            Player p = Wanter;
            int pIndex = (Wanter.Index + 1) % From.GameBoard.PlayerRank.Length;

            while (!From.Equals(p))
            {
                System.Diagnostics.Debug.WriteLine("Befor:" + string.Join(",", p.NextAction));
                System.Collections.Generic.Queue<eActionType> actionTempQueue = new System.Collections.Generic.Queue<eActionType>(2);

                // If p.NextAction.Count > 0 Then Stop

                actionTempQueue.Enqueue(eActionType.WaitingForTurn);
                actionTempQueue.Enqueue(eActionType.Pass);

                while (p.NextAction.Count > 0)
                    actionTempQueue.Enqueue(p.NextAction.Dequeue());


                do
                    p.NextAction.Enqueue(actionTempQueue.Dequeue());
                while (actionTempQueue.Count > 0);
                System.Diagnostics.Debug.WriteLine("After:" + string.Join(",", p.NextAction));

                pIndex = (pIndex + 1) % From.GameBoard.PlayerRank.Length;
                p = From.GameBoard.PlayerRank[pIndex];
            }

            System.Diagnostics.Debug.WriteLine("Befor:" + string.Join(",", p.NextAction));
            System.Collections.Generic.Queue<eActionType> actionTempQueue2 = new System.Collections.Generic.Queue<eActionType>(2);

            // If p.NextAction.Count > 0 Then Stop

            actionTempQueue2.Enqueue(eActionType.WaitingForTurn);
            actionTempQueue2.Enqueue(eActionType.Trade);

            while (p.NextAction.Count > 0)
                actionTempQueue2.Enqueue(p.NextAction.Dequeue());


            do
                p.NextAction.Enqueue(actionTempQueue2.Dequeue());
            while (actionTempQueue2.Count > 0);
            System.Diagnostics.Debug.WriteLine("After:" + string.Join(",", p.NextAction));
        }
    }
}
