using System.Text;
using System.Collections.Generic;

/// <summary>
/// Base object that can appear on map.
/// </summary>
class GameObject
{
    public Vector2d16 position;

    public GameObject(Vector2d16 position)
    {
        this.position = position;
    }

    public Vector2d16 GetPhysicAABB()
    {
        return new Vector2d16(1, 1);
    }
}