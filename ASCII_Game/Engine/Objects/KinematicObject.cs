/// <summary>
/// Object that can interact with map's geometry and react to it.
/// </summary>
class KinematicObject : PhysicalObject
{
    public KinematicObject(Vector2d16 position, int image, int shader, byte zIndex, int shape) : base(position, image, shader, zIndex, shape)
    {

    }

    public bool Move(Vector2d16 position)
    {
        Vector2d16 oldPosition = this.position;
        this.position = position;
        if (Game.gameState.map.GetCollisions(this).Length > 0)
        {
            this.position = oldPosition;
            return false;
        }
        return true;
    }
}