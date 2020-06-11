interface IRenderable
{
    public Image Image { get; }

    public void Render(Vector2d16 position);

    public Vector2d16 GetVisualBB();


}