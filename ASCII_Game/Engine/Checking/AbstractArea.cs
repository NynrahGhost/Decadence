using Decadence.Engine.Engine_objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Decadence.Engine.Checking
{
    //Base for all area classes
    public abstract class AbstractArea : GameObject, IEventChecker
    {
        [XmlIgnore]
        public virtual Type ReactTo { get; set; } = typeof(AbstractDamageable);

        //Create an invisible area from size.
        public AbstractArea(int x, int y, int width, int height) : base(x, y, width, height, -999) { }

        //Create an area from a model.
        public AbstractArea(string modelPath, int x, int y, int zIndex = 0, bool movable = false) : 
            base(modelPath, x, y, zIndex, movable) { }

        //Empty for serialization purposes.
        public AbstractArea() { }


        /// <summary>
        ///  Returns true if visual changes need to occur.
        ///  Launches events if there is an object inside that matches or subclasses the ReactTo type.
        /// </summary>
        public virtual bool Check()
        {
            bool res = false;
            foreach (AbstractMapObject obj in Program.Renderer.Map.MapObjects.Where(mapObj => mapObj.Movable)
                .Append(Program.Renderer.Player))
            {
                if (obj.GetType()!=ReactTo && obj.GetType().IsSubclassOf(ReactTo))
                {
                    continue;
                }
                if (!(obj.X+obj.ObjectModel.Width-1 < X || obj.X > X + ObjectModel.Width-1
                    || obj.Y + obj.ObjectModel.Height-1 < Y || obj.Y > Y + ObjectModel.Height-1))
                {
                    CreateEvent(obj);
                    res = true;
                }
            }
            return res;
        }

        //Code execution for 
        public abstract void CreateEvent(AbstractMapObject obj);
    }
}
