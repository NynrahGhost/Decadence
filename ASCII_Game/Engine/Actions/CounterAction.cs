using Decadence.Engine.Conditionals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decadence.Engine.Actions
{
    public class CounterAction : AbstractAction
    {
        public string CounterPrefix { get; set; } = null;
        public CounterAction() { }
        public CounterAction(string cPref) { CounterPrefix = cPref; }
        public CounterAction(CounterCondition count) { CounterPrefix = count.Name; }
        public override void Execute()
        {
            CounterCondition[] counters = CounterCondition.GetByPrefix(CounterPrefix);
            if (counters != null) foreach (CounterCondition c in CounterCondition.GetByPrefix(CounterPrefix)) c.Increment();
        }
    }
}
