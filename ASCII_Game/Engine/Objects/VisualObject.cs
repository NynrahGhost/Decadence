using System.Text;

/// <summary>
/// Object that can be rendered on screen.
/// </summary>
class VisualObject : GameObject, IRenderable
{
    public Image Image => image;

    protected Image image;

    public VisualObject(Vector2d16 position, Image image) : base(position)
    {
        this.image = image;
    }

    public VisualObject(Vector2d16 position) : base(position) { }

    public void Render(Vector2d16 position)
    {
        image.Render(position);
    }

    public Vector2d16 GetVisualBB()
    {
        return image.GetVisualBB();
    }
}