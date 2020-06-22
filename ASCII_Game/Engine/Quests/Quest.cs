using Decadence.Engine.Actions;
using Decadence.Engine.Conditionals;
using Decadence.Engine.Engine_objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Decadence.Engine
{
    /// <summary>
    /// Self-explanatory. 
    /// </summary>
    class Quest
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AbstractCondition[] StartConditions { get; set; }
        public QuestStage[] Stages { get; set; }
        public int CurrentStage { get; set; } = 0;

        private static List<Quest> loadedQuests = new List<Quest>();
        
        public Quest() { }

        public Quest(uint id, string name, string desc, QuestStage[] stages)
        {
            ID = id;
            Name = name;
            Description = desc;
            Stages = stages;
            foreach (QuestStage st in stages)
            {
                st.ParentId = id;
            }
            LoadUp(this);
        }

        /// <summary>
        /// Attempt to start the quest. If successful, adds the quest to the list of quests.
        /// </summary>
        public void TryStart()
        {
            if (StartConditions != null)
            {
                if (StartConditions.All(cond => cond.CheckSatisfied()))
                {
                    Program.Renderer.Player.ActiveQuests.Add((ID, CurrentStage));
                    Program.Renderer.AddCheck(Stages[CurrentStage]);
                }
            }
        }

        /// <summary>
        /// Progress the quest. If there are more stages, proceed to the next one. If there are none, end the quest.
        /// </summary>
        public void CompleteStage()
        {
            Program.Renderer.RemoveCheck(Stages[CurrentStage]);
            if (CurrentStage < Stages.Length - 1)
            {
                CurrentStage++;
                Program.Renderer.AddCheck(Stages[CurrentStage]);
            }
            else
            {
                Program.Renderer.Player.ActiveQuests.Remove((ID, CurrentStage));
                Program.Renderer.Player.CompletedQuests.Add(ID);
            }
        }

        /// <summary>
        /// Fail or cancel the quest. Restarts all progress.
        /// </summary>
        public void FailStage()
        {
            Program.Renderer.RemoveCheck(Stages[CurrentStage]);
            Program.Renderer.Player.ActiveQuests.Remove((ID, CurrentStage));
            CurrentStage = 0;
        }

        /// <summary>
        /// Load the quest from memory. If it's not there, load it from the file.
        /// </summary>
        /// <param name="id">Quest id.</param>
        /// <returns></returns>
        public static Quest GetQuestByID(uint id)
        {
            Quest loaded = loadedQuests.Find(q => q.ID == id);
            if (loaded == null) return XML.Load<Quest>(@"../../../Quests/quest" + id + ".xml");
            else return loaded;
        }

        [OnDeserialized]
        private void LoadUp(Quest q)
        {
            loadedQuests.Add(q);
            Program.Renderer.AddCheck(Stages[CurrentStage]);
        }

        /// <summary>
        /// Open a menu that allows the player to view all currently active quests, and cancel them at will.
        /// </summary>
        public static void EnterQuestsMenu()
        {
            Program.Renderer.ScheduleGameStateChange(Renderer.GameState.Quests);
            Quest[] activeQuests = new Quest[Program.Renderer.Player.ActiveQuests.Count];
            for (int i = 0; i < activeQuests.Length; i++)
            { 
                activeQuests[i] = GetQuestByID(Program.Renderer.Player.ActiveQuests[i].Id);
            }
            if (activeQuests.Length != 0)
            {
                Menu m = new Menu(null, "Active quests");
                string[] names = (from Quest q in activeQuests
                                  select q.Name).ToArray();
                Action[] actions = new Action[activeQuests.Length];
                Action showQuest = () =>
                {
                    Menu m1 = new Menu(null,activeQuests[m.CursorPosition].ToString()) ;
                    string[] options = { "Cancel quest", "Return" };
                    Action[] choiceActions = { ()=> { activeQuests[m.CursorPosition].FailStage(); m1.CloseMenu(); },
                        () => m1.CloseMenu() };
                    m1.options = options;
                    m1.actions = choiceActions;
                    m1.Enter();
                };
                Array.Fill(actions, showQuest);
                m.actions = actions;
                m.options = names;
                m.Enter();
            } else
            {
                Menu m = new Menu(null, "No active quests at the moment");
                string[] text = { "Return" };
                Action[] actions = { m.CloseMenu };
                m.actions = actions;
                m.options = text;
                m.Enter();
            }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(Name).Append('\n').Append(Description).Append('\n').Append(Stages[CurrentStage].ToString());
            return str.ToString();
        }
    }
}
