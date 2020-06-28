/// <summary>
/// Object that can participate in collision detection.
/// </summary>
class TactileObject : GameObject, ICollidable
{
    public Shape Shape { get => Game.gameState.map.shapes[shape]; set => System.Array.IndexOf(Game.gameState.map.shapes, value); }

    protected int shape;

    public TactileObject(Vector2d16 position, int shape) : base(position)
    {
        this.shape = shape;
    }


    /*
public new Vector2d16 GetBoundingBox()
{
return shape.GetBoundingBox();
}
*/
    public bool Collide(ICollidable obj)
    {
        return Shape.Collide(Position, obj.Shape, Position);
    }
}