class Projectile : KinematicObject
{
    byte ricochetLeft = 0;

    public Projectile(Vector2d16 position, int image, int shader, byte zIndex, int shape) : base(position, image, shader, zIndex, shape)
    {

    }
}
