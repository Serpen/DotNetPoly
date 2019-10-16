namespace DotNetPoly.Base.Fields
{
    class StationField : Field
    {
        private string contextName;
        private ushort contextCash;

        public StationField(string pName, ushort contextCash) : base(pName)
        {
            this.contextName = pName;
            this.contextCash = contextCash;
        }
    }
}