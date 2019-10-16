
namespace DotNetPoly.Fields
{
    public class StationField : ChanceField
    {
        public StationField(string pName, ushort pCash) : base(pName, eActionType.GoToField, pCash)
        {
        }
    }
}
