using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetPoly.Base.Fields;

namespace DotNetPoly.Base.Players
{
    public class Player : Entity
    {
        internal void RaiseMoveOver(Field field)
        {
            throw new NotImplementedException();
        }

        internal void raiseChance(eActionType chossenChance, object[] v)
        {
            throw new NotImplementedException();
        }

        internal void onDelegateControl(DelegateEventArgs delegateEventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
