class KinematicObject : PhysicalObject
{
    public KinematicObject(Vector2d16 position, Shape shape, Image image) : base(position, shape, image)
    {

    }

    public void Move(Vector2d16 position)
    {
        Vector2d16 oldPosition = this.position;
        this.position = position;
        if (Game.gameState.map.GetCollisions(this).Length > 0)
        {
            this.position = oldPosition;
        }
    }
}
