using Decadence.Engine.Actions;
using Decadence.Engine.Conditionals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Decadence.Engine
{
    public class DialogueChoice
    {
        public string ChoiceText { get; set; } = null;
        public AbstractCondition[] Conditions { get; set; } = null;
        public DialogueSegment NextSegment { get; set; } = null;

        public DialogueChoice() { }
        public DialogueChoice(string text, DialogueSegment next = null, AbstractCondition[] conds = null)
        {
            ChoiceText = text;
            NextSegment = next;
            Conditions = conds;
        }
    }

    public class DialogueSegment
    {
        private DialogueResponse choiceResponse = (0, 0);

        public string Text { get; set; }
        public DialogueChoice[] Choices { get; set; } = null;
        public AbstractAction[] Actions { get; set; } = null;
        public DialogueSegment Next { get; set; } = null;
        public DialogueResponse Response { get; set; } = (0, 0);

        public DialogueSegment() { }

        public DialogueSegment(string text, DialogueChoice[] choices = null, 
            AbstractAction[] actions = null, DialogueSegment next = null)
        {
            Text = text;
            Choices = choices;
            Actions = actions;
            Next = next;
        }

        ///<summary>Opens a new Menu to serve as a dialogue window.</summary>
        public DialogueResponse EnterDialogue()
        {
            Menu m = new Menu(null,Text);
            if (Actions!=null) foreach (AbstractAction ex in Actions) ex.Execute();
            bool loop = true;
            DialogueSegment nextSegment = Next;
            //Infinite loop to allow return to choices
            while (loop)
            {
                if (Choices != null)
                {
                    FillMenu(m);
                    m.CursorHighlight = s =>
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append('>').Append(s);
                        //Lists the descriptions of all conditions of the current choice under the current cursor position
                        if (Choices[m.CursorPosition].Conditions != null)
                        {
                            str.Append('\n');
                            foreach (AbstractCondition cond in Choices[m.CursorPosition].Conditions)
                            {
                                str.Append(cond.ToString()).Append('\n');
                            }
                        }
                        return str.ToString();
                    };
                }
                else
                {
                    string[] opt = { "..." };
                    Action[] act = { m.CloseMenu };
                    m.options = opt;
                    m.actions = act;
                }
                m.Enter();
                switch (choiceResponse.Response)
                {
                    case ResponseType.DoNothing:
                        loop = false;
                        break;
                    case ResponseType.ReturnToChoice:
                        if (Choices.Length < 2) return choiceResponse;
                        if (choiceResponse.Repeat > 0) return (3, choiceResponse.Repeat - 1);
                        else break;
                    case ResponseType.SkipForward:
                        for (int i = 0; i < choiceResponse.Repeat; i++)
                        {
                            if (nextSegment != null) nextSegment = nextSegment.Next;
                        }
                        loop = false;
                        break;
                    case ResponseType.CloseDialogue:
                        return (4, 0);
                }
            }
            if (nextSegment != null)
            {
                nextSegment.EnterDialogue();
            }
            return Response;
         }

        private Menu FillMenu(Menu m)
        {
            Action[] followUps = new Action[Choices.Length];
            for (int i = 0; i < Choices.Length; i++)
            {
                DialogueChoice curChoice = Choices[i]; 
                if (curChoice.Conditions == null ||
                    (Choices[i].Conditions != null && Choices[i].Conditions.All(cond => cond.CheckSatisfied())))
                {
                    followUps[i] = () =>
                    {
                        if (curChoice.NextSegment != null)
                            choiceResponse = curChoice.NextSegment.EnterDialogue();
                        else choiceResponse = (0, 0);
                        m.CloseMenu();
                    };
                }
                else
                {
                    followUps[i] = () => { return; };
                }
            }
            m.options = (from ch in Choices
                         select ch.ChoiceText).ToArray();
            m.actions = followUps;
            return m;
        }
    }

    public struct DialogueResponse
    {
        public int Repeat { get; set; }
        public ResponseType Response { get; set; }

        public static implicit operator DialogueResponse((int a, int b) v)
        {
            DialogueResponse res = new DialogueResponse
            {
                Repeat = v.a,
                Response = (ResponseType)v.b
            };
            return res;
        }
    }

    public enum ResponseType
    {
        /// <summary>Continue going through the dialogue normally (goes to the Next segment).</summary>
        DoNothing,
        /// <summary>Return to a choice. Can be repeated to go beyond just the previous choice.</summary>
        ReturnToChoice,
        /// <summary>Return to the previous choice and skip (Repeat) number of Next segments.</summary>
        SkipForward,
        /// <summary>Immediately close the dialogue window.</summary>
        CloseDialogue
    }
}
