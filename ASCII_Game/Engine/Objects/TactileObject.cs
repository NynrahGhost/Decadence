class TactileObject : GameObject, ICollidable
{
    Shape shape;

    public Shape Shape => shape;

    public TactileObject(Vector2d16 position, Shape shape) : base(position)
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
        return shape.Collide(position, obj.Shape, ((GameObject)obj).position);
    }
}