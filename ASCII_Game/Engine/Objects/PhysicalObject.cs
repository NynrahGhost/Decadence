using System.Text;

/// <summary>
/// Object that can both participate in collision detection and be rendered.
/// </summary>
class PhysicalObject : TactileObject, IRenderable
{
    public Image Image => image;

    Image image;

    public PhysicalObject(Vector2d16 position, Shape shape, Image image) : base(position, shape)
    {
        this.image = image;
    }

    public void Render(Vector2d16 position)
    {
        image.Render(position);
    }

    public Vector2d16 GetVisualBB()
    {
        return image.GetVisualBB();
    }
}