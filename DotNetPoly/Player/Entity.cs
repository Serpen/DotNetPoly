
namespace DotNetPoly
{
    public class Entity
    {
        public Entity(string pName, System.Drawing.Color pColor)
        {
            Name = pName;
            Color = pColor;
        }

        public string Name { get; }

        public System.Drawing.Color Color { get; }

        //Cached Array of Fields owned by Player
        internal Fields.HouseFieldCollection _ownfields = new Fields.HouseFieldCollection();

        public int Cash { get; private set; }

        private readonly uint _CreditLimit = 0;

        public delegate void MoneyTransferedEventHandler(Entity pFromPlayer, Entity pToPlayer, uint pAmount);
        public event MoneyTransferedEventHandler MoneyTransfered;

        internal void TransferMoney(Entity pToEntity, uint pAmount)
        {
            uint transfer;

            //if CheatMoneyFactor is active for a player
            if (pToEntity is Player)
                pAmount *= (uint)((Player)pToEntity).CheatMoneyFactor;
            if (this is Player)
                pAmount = (uint)(pAmount / ((Player)this).CheatMoneyFactor);

            if (pAmount > 0)
            {
                //Entity doesn't need cash check
                if (this.GetType() == typeof(Entity))
                {
                    
                    transfer = pAmount;
                    pToEntity.Cash += (int)transfer;
                    Cash -= (int)transfer;
                    MoneyTransfered?.Invoke(this, pToEntity, pAmount);
                }

                //to less cash -> bankcrupt
                else if (pAmount > _CreditLimit+Cash)
                {
                    if (Cash < 0)
                        transfer = (uint)(_CreditLimit - Cash);
                    else
                        transfer = (uint)(_CreditLimit + Cash);

                    pToEntity.Cash += (int)transfer;
                    Cash -= (int)transfer;
                    MoneyTransfered?.Invoke(this, pToEntity, System.Convert.ToUInt32(transfer));
                    (this as Player).OnBankrupt();
                }

                //rest should be fine
                else
                {
                    transfer = pAmount;
                    pToEntity.Cash += (int)transfer;
                    Cash -= (int)transfer;
                    MoneyTransfered?.Invoke(this, pToEntity, System.Convert.ToUInt32(transfer));
                }
            } else
                throw new DotNetPolyException("negative cash transfer!");
        }

        public override string ToString()
        {
            /* TODO ERROR: Skipped IfDirectiveTrivia */
#if (DEBUG)
            return $"{Name} ({Cash})";
#else
            return Name;
#endif
        }

        public Fields.HouseFieldCollection OwnFields
        {
            get
            {
                return _ownfields;
            }
        }

        // <Diagnostics.DebuggerStepThrough()>
        public sealed override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Entity) || obj.GetType().IsSubclassOf(typeof(Entity)))
            {
                if (((Entity)obj).Name == this.Name)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
