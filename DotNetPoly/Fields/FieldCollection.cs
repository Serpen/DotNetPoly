using System.Linq;

namespace DotNetPoly.Fields
{
    class FieldCollection : System.Collections.Generic.Dictionary<string, Field>
    {
        public Field this[int i]
        {
            get
            {
                return Values.ElementAt(i);
            }
        }
    }

    public class HouseFieldCollection : System.Collections.Generic.Dictionary<string, HouseField>
    {
        public Field this[int i]
        {
            get
            {
                return Values.ElementAt(i);
            }
        }
    }
}
