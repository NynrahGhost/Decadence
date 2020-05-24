class TactileObject : GameObject, ICollidable
{
    Shape shape;

    public TactileObject(Vector2d16 position, Shape shape) : base(position)
    {
        this.shape = shape;
    }

    public new Vector2d16 GetBoundingBox()
    {
        return shape.GetBoundingBox();
    }

    public bool Collide(GameObject obj)
    {
        throw new System.NotImplementedException();
    }
}