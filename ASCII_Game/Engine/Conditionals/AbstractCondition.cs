using System;
using System.Collections.Generic;
using System.Text;

namespace Decadence.Engine.Conditionals
{
    public abstract class AbstractCondition
    {
        public AbstractCondition() { }
        public abstract bool CheckSatisfied();

        public override abstract string ToString();
    }
}
