class Character : KinematicObject
{
    EDirection direction = EDirection.south;

    public Character(Vector2d16 position, Shape shape, Image image) : base(position, shape, image)
    {

    }
}

enum EDirection
{
    north, north_east, east, south_east, south, south_west, west, north_west
}