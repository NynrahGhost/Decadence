interface IRenderable
{
    public Image Image { get; }

    public void Render(Vector2d16 position);
}