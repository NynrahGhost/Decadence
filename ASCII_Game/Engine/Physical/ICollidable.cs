using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Base interface for collidable objects.
/// </summary>
interface ICollidable
{
    public Shape Shape { get; }

    public bool Collide(ICollidable obj);
}
