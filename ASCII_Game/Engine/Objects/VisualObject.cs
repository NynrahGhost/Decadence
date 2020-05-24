using System.Text;

class VisualObject : GameObject, IRenderable
{
    public Image Image => image;

    Image image;

    public VisualObject(Vector2d16 position, Image image) : base(position)
    {
        this.image = image;
    }

    public StringBuilder Render()
    {
        return image.Render();
    }
}