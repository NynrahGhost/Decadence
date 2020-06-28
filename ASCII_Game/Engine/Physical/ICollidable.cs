using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Base interface for collidable objects.
/// </summary>
interface ICollidable : IPositionable
{
    public Shape Shape { get; set; }

    public bool Collide(ICollidable obj);
}
