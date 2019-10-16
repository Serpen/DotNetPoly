using System.Linq;

namespace DotNetPoly
{
    namespace Fields
    {
        public class HouseField : Field
        {
            internal HouseField(System.Xml.XmlNode pXmlNode, Settings pSettings) : base(pXmlNode.Attributes["name"]?.InnerText)
            {
                ushort.TryParse(pXmlNode.Attributes["group"]?.InnerText, out _Street);

                var upgradeNodes = pXmlNode.SelectNodes("./level");
                UpgradeMatrix = new ushort[upgradeNodes.Count - 1 + 1, 2];

                RentUpgradeFactor = pSettings.DefaultRentUpgradeFactor;
                var loopTo = upgradeNodes.Count - 1;
                for (var i = 0; i <= loopTo; i++)
                {
                    ushort.TryParse(upgradeNodes[i].Attributes["cost"].InnerText, out UpgradeMatrix[i, 0]);
                    ushort.TryParse(upgradeNodes[i].Attributes["rent"].InnerText, out UpgradeMatrix[i, 1]);
                    try
                    {
                        int.TryParse(upgradeNodes[i].Attributes["rent"].Attributes["factor"].InnerText, out RentUpgradeFactor);
                    }
                    catch
                    {
                        RentUpgradeFactor = pSettings.DefaultRentUpgradeFactor;
                    }
                }
                maxUpgradeLevel = upgradeNodes.Count;

                UpgradeAlgorithmRent = pSettings.RentUpgradeMethode;
                UpgradeAlgorithmCost = pSettings.CostUpgradeMethode;
            }


            private readonly int maxUpgradeLevel = 2;

            private readonly int RentUpgradeFactor;

            private readonly eUpgradeAlgorithm UpgradeAlgorithmRent;
            private readonly eUpgradeAlgorithm UpgradeAlgorithmCost;

            public bool HasCredit { get; private set; }
            public enum eUpgradeAlgorithm
            {
                FullGroup,
                Table,
                OnGroup,
                OnHouse
            }

            // When could a field be upgraded
            public enum eUpgradeRange
            {
                OnThisHouse     // only when positioned on House
    ,
                OnThisGroup     // every house in positioned group
    ,
                OnOwnField      // all, when on own field
    ,
                Always          // always
            }

            private readonly ushort[,] UpgradeMatrix;

            public ushort Cost
            {
                get
                {
                    ushort myCost = 0;
                    for (var i = 0; i <= UpgradeLevel; i++)
                        myCost += UpgradeMatrix[i, 0];
                    return myCost;
                }
            }

            public int UpgradeLevel { get; private set; } = 0;

            // TODO: Whats about no Street
            internal readonly ushort _Street;

            public ushort Rent
            {
                get
                {
                    ushort myrent;

                    if (UpgradeAlgorithmRent == eUpgradeAlgorithm.FullGroup && !this.Owner.Equals(GameBoard.BANK) && HasCompleteStreet(this.Owner))
                        myrent = System.Convert.ToUInt16(UpgradeMatrix[UpgradeLevel, 1] * 2);
                    else
                        myrent = System.Convert.ToUInt16(UpgradeMatrix[UpgradeLevel, 1]);

                    if (!this.Owner.Equals(GameBoard.BANK))
                        myrent *= System.Convert.ToUInt16(((Player)Owner).RentPercentMultiplier / (double)100);

                    return myrent;
                }
            }

            internal void Buy(Player pPlayer)
            {
                Buy(pPlayer, Cost);
            }
            [System.Obsolete()]
            internal void Buy(Player pPlayer, uint pCost)
            {
                GameBoard.RaiseChangeOwner(this, pPlayer);

                pPlayer.TransferMoney(this.Owner, pCost);
                ChangeOwner(pPlayer);
            }

            private bool HasCompleteStreet(Entity pPlayer)
            {
                if (GameBoard.Statistics.CompleteGroups == 0)
                    return false;

                return (from h in GameBoard.Groups[this._Street] select h).All((HouseField h) => h.Owner.Equals(pPlayer));
                foreach (HouseField h in GameBoard.Groups[this._Street])
                {
                    if (!h.Owner.Equals(pPlayer))
                        return false;
                }
                return true;
            }


            public bool CanUpgrade(Player pPlayer)
            {
                // TODO: Check for street
                foreach (var h in new HouseField[] { this })
                {
                    if (h.UpgradeLevel >= h.maxUpgradeLevel)
                        return false;
                    if (h.UpgradeAlgorithmRent == eUpgradeAlgorithm.Table && h.UpgradeLevel >= h.UpgradeMatrix.GetLength(0))
                        return false;
                    if (pPlayer.Cash < h.UpgradeCost)
                        return false;
                }

                return true;
            }

            public ushort UpgradeCost
            {
                get
                {
                    switch (UpgradeAlgorithmRent)
                    {
                        case eUpgradeAlgorithm.Table:
                            {
                                return UpgradeMatrix[UpgradeLevel, 0];
                            }

                        default:
                            {
                                return System.Convert.ToUInt16(System.Math.Max((this.Cost * (UpgradeLevel + 1) / (double)2), UpgradeLevel + 1)); // Cost >= 1
                            }
                    }
                }
            }

            internal void Upgrade()
            {
                if (Owner is Player PlayerOwner)
                {
                    PlayerOwner.TransferMoney(GameBoard.BANK, UpgradeCost);
                    if (PlayerOwner.IsPlayering)
                    {
                        PlayerOwner.RaiseEventHouseUpgrade(this);
                        UpgradeLevel += 1;
                    }

                    if (this.Cost > GameBoard.Statistics.MaxCost)
                        GameBoard.Statistics.MaxCost = this.Cost;
                    if (this.Rent > GameBoard.Statistics.MaxRent)
                        GameBoard.Statistics.MaxRent = this.Rent;
                    if (this.UpgradeLevel > GameBoard.Statistics.MaxUpgradeLevel)
                        GameBoard.Statistics.MaxUpgradeLevel = this.UpgradeLevel;
                }
            }

            //Shouldn return anything?!
            public Entity PayRent(Player pFromPlayer)
            {
                if (HasCredit)
                {
                    //No Rent, Bank Rent?
                    pFromPlayer.TransferMoney(GameBoard.BANK, Rent);
                    return GameBoard.BANK;
                }
                else
                {
                    pFromPlayer.TransferMoney(Owner, Rent);
                    return Owner;
                }
            }

            internal void TakeForCredit()
            {
                HasCredit = true;
                GameBoard.BANK.TransferMoney(this.Owner, this.Cost);
                GameBoard.Statistics.CreditHouses += 1;
            }

            internal void RemoveCredit()
            {
                Owner.TransferMoney(GameBoard.BANK, this.Cost);
                HasCredit = false;
                GameBoard.Statistics.CreditHouses -= 1;
            }

            internal override void onMoveOn(Player pPlayer)
            {
                base.onMoveOn(pPlayer);

                if (Owner.Equals(GameBoard.BANK))
                    pPlayer.onDelegateControl(new DelegateEventArgs(new eActionType[] { eActionType.BuyHouse, eActionType.Pass }, null, new Field[] { this }, 0));
                else if (this.Owner.Equals(pPlayer))
                {
                    if (CanUpgrade(pPlayer))
                        pPlayer.onDelegateControl(new DelegateEventArgs(new eActionType[] { eActionType.UpgradeHouse, eActionType.Pass }, null, new Field[] { this }, 0));
                }
                else
                    pPlayer.onDelegateControl(new DelegateEventArgs(eActionType.PayRent, this));
            }

            internal void ChangeOwner(Entity pPlayer)
            {
                if (Owner.Equals(GameBoard.BANK))
                    GameBoard.Statistics.SoldHouseFields += 1;
                else
                    GameBoard.Statistics.SoldHouseFields -= 1;

                Owner._ownfields.Remove(this.Name);
                Owner = pPlayer;

                pPlayer._ownfields.Add(this.Name,this);

                // HACK: Correcter for Wrong Count
                int totalsold = (from h in GameBoard._Housefields where !h.Value.Owner.Equals(GameBoard.BANK) select h.Value).Count();

                if (Cost > GameBoard.Statistics.MaxCost)
                    GameBoard.Statistics.MaxCost = this.Cost;
                if (Rent > GameBoard.Statistics.MaxRent)
                    GameBoard.Statistics.MaxRent = this.Rent;
                if (UpgradeLevel > GameBoard.Statistics.MaxUpgradeLevel)
                    GameBoard.Statistics.MaxUpgradeLevel = this.UpgradeLevel;

                // Check Whether Group ist complete
                

                if (HasCompleteStreet(pPlayer))
                {
                    GameBoard.Statistics.CompleteGroups = 0;
                    // If Group is complete, calculate all Groups
                    foreach (var g in GameBoard.Groups)
                    {
                        Entity prevOwner = null;
                        bool groupcomplete = false;
                        foreach (HouseField h in g)
                        {
                            if ((prevOwner != null && !h.Owner.Equals(prevOwner)) || h.Owner.Equals(GameBoard.BANK))
                            {
                                groupcomplete = false;
                                break;
                            }
                            else
                            {
                                groupcomplete = true;
                                prevOwner = h.Owner;
                            }
                        }
                        if (groupcomplete)
                            GameBoard.Statistics.CompleteGroups += 1;
                    }
                    GameBoard.Statistics.CompleteGroups += 1;
                }


                System.Diagnostics.Debug.WriteLineIf(GameBoard.Statistics.SoldHouseFields != totalsold, $"HACK: SoldHouseFields irregular in ChangeOwner {GameBoard.Statistics.SoldHouseFields} <> {totalsold}");
                GameBoard.Statistics.SoldHouseFields = totalsold;
            }
        }
    }
}
