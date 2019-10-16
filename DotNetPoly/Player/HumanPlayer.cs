using System.Linq;

namespace DotNetPoly
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string pName, System.Drawing.Color pColor) : base(pName, pColor) { }

        public override DiceRollResult DelegateDiceRoll()
        {
            var x = new DelegateEventArgs(new eActionType[] { eActionType.DiceOnly }, null, null, 0);
            DelegateControl?.Invoke(this, ref x);
            WaitChoosenAction.WaitOne();

            WaitChoosenAction.Reset();

            return onRollDice();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        internal override void onDelegateControl(DelegateEventArgs e)
        {
            System.Collections.Generic.List<eActionType> allPossibilities = new System.Collections.Generic.List<eActionType>(3);

            // Keep e for recall
            var modifiedEventArgs = e.Copy();

            allPossibilities.AddRange(modifiedEventArgs.Actions);


            if (allPossibilities.Contains(eActionType.PayRent))
            {
                if (CheatAlwaysRentFree || GameBoard.FreeParkingOwner.Equals(this))
                    allPossibilities.Add(eActionType.Card_UseFreeParking);
            }

            if (allPossibilities.Contains(eActionType.JumpToJail))
            {
                if (CheatAlwaysJailFree || GameBoard.FreeJailOwner.Equals(this))
                    allPossibilities.Add(eActionType.Card_UseFreeJail);
            }

            if (GameBoard.Settings.FixedDepositePercentage > 0)
            {
                if (e.Cash > 0 && !allPossibilities.Contains(eActionType.LoseTurn))
                    allPossibilities.Add(eActionType.IncreaseFixedDeposite);

                if (Festgeld > 0 && !allPossibilities.Contains(eActionType.LoseTurn))
                    allPossibilities.Add(eActionType.TerminateFixedDeposite);
            }

            if (GameBoard.Settings.UseCredits)
            {
                if (GameBoard.Statistics.SoldHouseFields > 0 && this._ownfields.Count > 0 && !allPossibilities.Contains(eActionType.LoseTurn))
                    if ((from h in OwnFields where !h.Value.HasCredit select h).Any())
                        allPossibilities.Add(eActionType.CreditForHouse);

                if (GameBoard.Statistics.CreditHouses > 0)
                {
                    var hasCredit = (from h in OwnFields where h.Value.HasCredit select h).Any();

                    if (hasCredit)
                        allPossibilities.Add(eActionType.CreditReleaseHouse);
                }
            }


            allPossibilities.Add(eActionType.GiveUp);

            modifiedEventArgs.Actions = allPossibilities.ToArray();
            DelegateControl?.Invoke(this, ref modifiedEventArgs);

            WaitChoosenAction.WaitOne();

            WaitChoosenAction.Reset();

            // TODO: Next: Check if choosen vars are fileld

            if (invokeAction(choosenEventArgs))
                onDelegateControl(e);
        }

        // Event DelegateControl(pPlayer As BasePlayer, pPossibleActions() As eActionType, pFields As BaseField(), pPlayer() As Entity, pCash As UInteger)
        public event DelegateControlEventHandler DelegateControl;

        public delegate void DelegateControlEventHandler(Player pPlayer, ref DelegateEventArgs e);
    }
}
