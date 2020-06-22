using Decadence.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Decadence.Engine.Engine_objects
{
    public abstract class AbstractWeapon : AbstractHoldable
    {        
        private readonly Stopwatch st = new Stopwatch();
        public uint RateOfFire { get; set; }
        public uint ProjectileSpeed { get; set; }
        public uint Damage { get; set; }

        public AbstractWeapon(uint rof, uint speed, uint damage, string rightModelPath, 
            AbstractDamageable holder) : base(rightModelPath, holder)
        {
            RateOfFire = rof;
            ProjectileSpeed = speed;
            Damage = damage;
        }

        public AbstractWeapon() { }

        public virtual void Shoot(Renderer.Direction dir)
        {
            if (st.ElapsedMilliseconds > 1000 / RateOfFire && X > 0 && Y > 0)
            {
                Program.Renderer.AddCheckable(new Projectile(dir, ProjectileSpeed, Damage, X, Y));
            }
            st.Restart();
        }
    }
}
