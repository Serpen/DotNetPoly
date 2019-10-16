namespace DotNetPoly
{
    namespace Fields
    {
        public abstract class Field
        {
            public Field(string pName)
            {
                Name = pName;
            }

            public void Initialize(GameBoard pGameBoard, ushort pIndex)
            {
                GameBoard = pGameBoard;
                Index = pIndex;
                Owner = pGameBoard.BANK;
            }

            public string Name { get; }

            public virtual Entity Owner { get; protected set; }
  
            public GameBoard GameBoard { get; private set; }

            internal virtual void onMoveOver(Player pPlayer)
            {
                pPlayer.RaiseMoveOver(this);
                GameBoard.Statistics.FieldHovers[this.Index] += 1;
            }

            internal virtual void onMoveOn(Player pPlayer)
            {
                pPlayer.RaiseMoveOn(this);
                GameBoard.Statistics.FieldVisits[this.Index] += 1;
            }

            public ushort Index { get; private set; }

            public override string ToString()
            {
                /* TODO ERROR: Skipped IfDirectiveTrivia */
                return Name;
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() == typeof(Field) || obj.GetType().IsSubclassOf(typeof(Field)))
                {
                    var bobj = (Field)obj;
                    if (bobj.Name == this.Name && bobj.Index == this.Index)
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
}
