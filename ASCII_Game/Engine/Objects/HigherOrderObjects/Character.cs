class Character : KinematicObject
{
    EDirection direction = EDirection.south;

    public Character(Vector2d16 position, int image, int shader, byte zIndex, int shape) : base(position, image, shader, zIndex, shape)
    {

    }
}

enum EDirection
{
    north, north_east, east, south_east, south, south_west, west, north_west
}