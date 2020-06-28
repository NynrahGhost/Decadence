using System.Text;
using System.Collections.Generic;

/// <summary>
/// Base object that can appear on map.
/// </summary>
public class GameObject : IPositionable
{
    public Vector2d16 Position { get => position; set => position = value; }

    protected Vector2d16 position;

    public GameObject(Vector2d16 position)
    {
        this.position = position;
    }

    public static GameObject FromJSON(List<object> array)
    {
        List<object> position = (List<object>)array[0];
        switch (array.Count)
        {
            case 2:
                return new TactileObject(((short)(int)position[0], (short)(int)position[1]), (int)array[1]);
            case 4:
                return new VisualObject(((short)(int)position[0], (short)(int)position[1]), (int)array[1], (int)array[2], (byte)(int)array[3]);
            case 5:
                return new PhysicalObject(((short)(int)position[0], (short)(int)position[1]), (int)array[1], (int)array[2], (byte)(int)array[3], (int)array[4]);
        }
        throw new JSONException("GameObject cannot be created from given array.");
    }
}