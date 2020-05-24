using System.Text;
using System.Collections.Generic;

class GameObject
{
    public Vector2d16 position;

    public GameObject(Vector2d16 position)
    {
        this.position = position;
    }

    public Vector2d16 GetBoundingBox()
    {
        return new Vector2d16(1, 1);
    }
}