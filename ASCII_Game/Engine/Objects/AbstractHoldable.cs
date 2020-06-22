using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Decadence.Engine.Engine_objects
{
    /// <summary>
    /// Base class for any item the rendering of which is dependent on a separate entity.
    /// For instance, a gun that is held in an enemy's hand.
    /// </summary>
    public class AbstractHoldable : AbstractMapObject
    {
        public Model ModelRight {get;set; }
        public Model ModelUp { get; set; }

        private static uint curId = 1;

        [XmlIgnore]
        public AbstractHolder Holder { get; set; } = Program.Renderer.Player;
        public uint Id { get; set; } = curId;

        public override int X { get => GetPosition(Holder, Holder.Direction).x; set => base.X = value; }
        public override int Y { get => GetPosition(Holder, Holder.Direction).y; set => base.Y = value; }
        public override Model ObjectModel { get => GetModel(); set => base.ObjectModel = value; }
        public override int ZIndex { get => Holder.ZIndex + Holder.ObjectModel.Height / 2; set => base.ZIndex = value; }

        public static List<AbstractHoldable> holdables = new List<AbstractHoldable>();

        public AbstractHoldable(string rightModelPath, AbstractDamageable holder) : 
            base(rightModelPath, GetPosition(holder, holder.Direction).x,
                GetPosition(holder, holder.Direction).y, holder.ZIndex, true)
        {
            curId++;
            Holder = holder;
            ModelRight = new Model(rightModelPath, true);
            ModelUp = new Model(rightModelPath.Replace("Hor", "Ver"), true);
            Load();
        }

        public AbstractHoldable() { }

        [OnDeserializing]
        public void Load()
        {
            holdables.Add(this);
        }

        protected static (int x, int y) GetPosition(AbstractHolder holder, Renderer.Direction dir)
        {
            switch (dir)
            {
                case Renderer.Direction.Up:
                    return (holder.X + holder.ObjectModel.Width / 2, holder.Y - 1);
                case Renderer.Direction.Down:
                    return (holder.X + holder.ObjectModel.Width / 2, holder.Y + holder.ObjectModel.Height);
                case Renderer.Direction.Left:
                    return (holder.X - 2, holder.Y + holder.ObjectModel.Height / 2);
                case Renderer.Direction.Right:
                    return (holder.X + holder.ObjectModel.Width, holder.Y + holder.ObjectModel.Height / 2);
            }
            return (1, 1);
        }

        protected Model GetModel()
        {
            switch (Holder.Direction)
            {
                case Renderer.Direction.Up:
                    return ModelUp;
                case Renderer.Direction.Down:
                    return Model.Reverse(ModelUp);
                case Renderer.Direction.Right:
                    return ModelRight;
                case Renderer.Direction.Left:
                    return Model.Reverse(ModelRight);
            }
            return ModelRight;
        }
    }
}
