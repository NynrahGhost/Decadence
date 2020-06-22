using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Decadence.Engine
{
    /// <summary>
    /// A universal menu class. To use, specify <see cref="actions"/>actions and <see cref="options"/> and use <see cref="Enter"/>Enter.
    /// <see cref="CloseMenu"/>CloseMenu is used to close the menu loop from a lambda function.
    /// Use Up and Down arrows to navigate, Enter to select, and Escape to close the menu.
    /// </summary>
    public class Menu
    {
        private int curPos = 0;
        private bool loop = true;

        [XmlIgnore]
        public Action[] actions = null;
        public string[] options = null;
        [XmlIgnore]
        /// <summary>
        /// The action that is executed when 'Escape' is pressed.
        /// </summary>
        public Action returnAction = null;

        /// <summary>
        /// The message that's displayed above all other options.
        /// </summary>
        public string topMessage = null;

        public int CursorPosition { get => curPos; }
        [XmlIgnore]
        public Func<string, string> CursorHighlight { get; set; } = (s) => '>' + s;

        public Menu() { }

        public Menu(Action returnAction = null, string message = null)
        {
            this.returnAction = returnAction;
            topMessage = message;
        }

        public void Enter()
        {
            loop = true;
            while (loop)
            {
                Console.Clear();
                if (topMessage != null) Console.WriteLine(topMessage+'\n');
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == curPos) Console.WriteLine(CursorHighlight(options[i]));
                    else Console.WriteLine(options[i]);
                }
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (curPos > 0) curPos--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (curPos < options.Length - 1) curPos++;
                        break;
                    case ConsoleKey.Enter:
                        actions[curPos]();
                        break;
                    case ConsoleKey.Escape:
                        loop = false;
                        if (returnAction != null) { returnAction(); return; }
                        else return;
                }
            }
        }

        public void CloseMenu()
        {
            loop = false;
        }
    }
}
