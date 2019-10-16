using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetPoly.Base.Fields
{
    class Field
    {
        public Field(string pName) { }

        void Initialize(GameBoard pgameBoard, ushort pIndex)
        {
            GameBoard = pgameBoard;
            Index = pIndex;

        }

        public string Name { get; private set; }
        public int Index { get; private set; }

        public GameBoard GameBoard { get; private set; }

        public Players.Entity Owner { get; private set; }

        internal virtual void onMoveOver(Players.Player pPlayer)
        {
            pPlayer.RaiseMoveOver(this);
            GameBoard.Statistics.FieldHovers(this.Index)++;
        }

        internal virtual void onMoveOn(Players.Player pPlayer)
        {
            pPlayer.RaiseMoveOver(this);
            GameBoard.Statistics.FieldVisits(this.Index)++;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override int GetHashCode()
        {
            return $"{Name}{Index}".GetHashCode();
        }
        public override bool Equals(object obj)
        {
            Field f = obj as Field;
            if (!(f is null))
                if (f.Name == this.Name && f.Index == this.Index)
                    return true;
                else
                    return false;
            else
                return false;
        }
    } //end class Field
} //end namespace
