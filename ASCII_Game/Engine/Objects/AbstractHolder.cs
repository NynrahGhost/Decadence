using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Decadence.Engine.Engine_objects
{
    /// <summary>
    /// A base class for entities that can hold objects.
    /// </summary>
    public class AbstractHolder: VisualObject
    {
        public Renderer.Direction Direction { get; set; } = Renderer.Direction.Right;
        public uint HeldItemId { get; set; } = 0;
        [XmlIgnore]
        public AbstractHoldable CurrentItem { get; set; }

        public AbstractHolder(string modelPath, int x, int y, int zIndex = 0, int linesOffTop = 0, bool isMovable = false) :
            base(modelPath, x, y, zIndex, linesOffTop, isMovable)
        { }

        public AbstractHolder() { }

        [OnDeserializing]
        public void LoadHeld()
        {
            if (HeldItemId != 0)
            {
                CurrentItem = AbstractHoldable.holdables.Find(obj => obj.Id == HeldItemId);
                CurrentItem.Holder = this;
            }
        }
    }
}
