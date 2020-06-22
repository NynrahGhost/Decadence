using System;
using System.Collections.Generic;
using System.Text;

namespace Decadence.Engine.Engine_objects
{
    public abstract class AbstractDamageable : AbstractHolder
    {
        public double Health { get; set; } = 100;
        public uint Armor { get; set; } = 0;

        public AbstractDamageable(string modelPath, int x, int y, int zIndex = 0, int linesOffTop = 0, bool isMovable = false) :
            base(modelPath, x, y, zIndex, linesOffTop, isMovable)
        { }

        public AbstractDamageable() { }

        public void TakeDamage(uint damage)
        {
            if (Armor == 0) Health -= (int) damage;
            else
            {
                Health -= Math.Ceiling((double)damage * (1 - Armor / (Armor+25)));
            }
        }
    }
}
