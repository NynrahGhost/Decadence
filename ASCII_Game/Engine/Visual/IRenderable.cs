/// <summary>
/// Base interface for renderable objects.
/// </summary>
interface IRenderable : IPositionable
{
    public Image Image { get; set; }
    public Shader Shader { get; set; }
    public byte ZIndex { get; set; }

    public void Render(Vector2d16 position);

    public Vector2d16 GetVisualBB();


}