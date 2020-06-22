using Decadence.Engine.Checking;
using Decadence.Engine.Conditionals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decadence.Engine.Actions
{
    public class QuestStage : IEventChecker
    {
        public uint ParentId { get; set; }
        public string Description { get; set; }
        public AbstractCondition[] Objectives { get; set; }
        public AbstractCondition[] FailConditions { get; set; }

        public QuestStage() { }

        public QuestStage(string desc, AbstractCondition[] fulfill, AbstractCondition[] avoid = null)
        {
            Description = desc;
            Objectives = fulfill;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(Description).Append("\nObjectives:\n");
            foreach (AbstractCondition cond in Objectives)
            {
                str.Append('*').Append(cond.ToString()).Append('\n');
            }
            if (FailConditions != null)
            {
                str.Append(Description).Append("\nFailure conditions:\n");
                foreach (AbstractCondition fail in FailConditions)
                {
                    str.Append('X').Append(fail.ToString()).Append('\n');
                }
            }
            return str.ToString();
        }

        public bool Check()
        {
            if (Objectives.All(obj => obj.CheckSatisfied()))
            {
                Quest.GetQuestByID(ParentId).CompleteStage();
            }
            if (FailConditions != null)
            {
                if (FailConditions.All(obj => obj.CheckSatisfied())) Quest.GetQuestByID(ParentId).FailStage();
            }
            return false;
        }
    }
}
