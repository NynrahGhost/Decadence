using Decadence.Engine.Engine_objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Decadence.Engine.Checking
{
    /// <summary>
    /// Area that launches player character only events on entry, like starting dialogues or progressing quests.
    /// </summary>
    public class NonDamageArea : AbstractArea
    {
        [XmlIgnore]
        public override Type ReactTo { get => typeof(PlayerCharacter); }
        [XmlIgnore]
        public Action OnEnter { get; set; } = () => { return; };
        private Renderer.GameState goInto;
        private bool isIn = false;

        public NonDamageArea(Renderer.GameState state, int x, int y, int width, int height) : base(x, y, width, height)
        {
            goInto = state;
        }

        public NonDamageArea(Renderer.GameState state, string modelPath, int x, int y, int zIndex = 0) : base(modelPath,x,y,zIndex)
        {
            goInto = state;
        }

        public NonDamageArea() { }

        public override bool Check()
        {
            isIn = base.Check();
            return false;
        }

        public override void CreateEvent(AbstractMapObject obj)
        {
            if (!isIn)
            {
                isIn = true;
                Program.Renderer.ScheduleGameStateChange(goInto);
                OnEnter();
                Program.Renderer.ScheduleGameStateChange(Renderer.GameState.Play);
            }
        }
    }
}
