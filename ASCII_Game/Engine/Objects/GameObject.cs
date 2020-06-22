using System.Text;
using System.Collections.Generic;

/// <summary>
/// Base object that can appear on map.
/// </summary>
public class GameObject
{
    public Vector2d16 position;

    public GameObject(Vector2d16 position)
    {
        this.position = position;
    }
}