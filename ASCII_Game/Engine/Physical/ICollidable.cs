using System;
using System.Collections.Generic;
using System.Text;

interface ICollidable
{
    public Shape Shape { get; }

    public bool Collide(ICollidable obj);
}
