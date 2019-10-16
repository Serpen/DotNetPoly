using System;

namespace DotNetPoly.Base
{
    class DotNetPolyException : Exception
    {
        public DotNetPolyException() { }
        public DotNetPolyException(string Message) : base(Message) {
        }
    }
}
