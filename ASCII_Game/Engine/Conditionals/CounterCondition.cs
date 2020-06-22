using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Decadence.Engine.Conditionals
{
    public class CounterCondition : AbstractCondition
    {
        public int Count { get; set; }
        public int Required { get; set; } = 1;
        public bool LessThan { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private static LinkedList<CounterCondition> allCounters = new LinkedList<CounterCondition>();

        public CounterCondition() { }
        public CounterCondition(int req, string name, string desc, bool ls = false) : this()
        {
            Required = req;
            Name = name;
            LessThan = ls;
            Description = desc;
        }

        public void Increment() { Count++; }

        public override bool CheckSatisfied()
        {
            return LessThan ? Count < Required : Count >= Required;
        }

        [OnDeserialized]
        ///<summary>Ties the current counter with the static collection of counters.
        ///If there exists a counter with the same name in the collection, data from it will be copied.
        ///Otherwise, this counter will be added to the collection.</summary>
        public void LoadUp()
        {
            CounterCondition existingCounter = GetByName(Name);
            if (existingCounter == null) allCounters.AddLast(this);
            else
            {
                Required = existingCounter.Required;
                LessThan = existingCounter.LessThan;
                Count = existingCounter.Count;
                Description = existingCounter.Description;
            }
        }

        public static CounterCondition[] GetByPrefix(string prefix)
        {
            CounterCondition[] startWith = allCounters.Where(count => count.Name.StartsWith(prefix)).ToArray();
            if (startWith.Length!=0) return startWith;
            else return null;
        }

        public static CounterCondition GetByName(string name)
        {
            CounterCondition[] sameName = allCounters.Where(count => count.Name == name).ToArray();
            if (sameName.Length != 0) return sameName[0];
            else return null;
        }

        public override string ToString()
        {
            return String.Format("{0}\nCurrent progress:{1}/{2}", Description, Count, Required);
        }
    }
}
