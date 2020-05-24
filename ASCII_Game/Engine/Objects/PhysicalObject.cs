using System.Text;

class PhysicalObject : TactileObject, IRenderable
{
    public Image Image => image;

    Image image;

    public PhysicalObject(Vector2d16 position, Shape shape, Image image) : base(position, shape)
    {
        this.image = image;
    }

    public StringBuilder Render()
    {
        return image.Render();
    }
}