using System.Text;

class VisualObject : GameObject, IRenderable
{
    public Image Image => image;

    Image image;

    public VisualObject(Vector2d16 position, Image image) : base(position)
    {
        this.image = image;
    }

    public void Render(Vector2d16 position)
    {
        image.Render(position);
    }
}