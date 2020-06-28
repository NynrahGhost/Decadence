using System.Text;

/// <summary>
/// Object that can be rendered on screen.
/// </summary>
class VisualObject : GameObject, IRenderable
{
    public Image Image { get => Game.gameState.map.images[image]; set => System.Array.IndexOf(Game.gameState.map.images, value); }
    public Shader Shader { get => Game.gameState.map.shaders[shader]; set => System.Array.IndexOf(Game.gameState.map.shaders, value); }
    public byte ZIndex { get => zIndex; set => zIndex = value; }

    protected int image;
    protected int shader;
    protected byte zIndex = 127;

    public VisualObject(Vector2d16 position, int image, int shader, byte zIndex) : base(position)
    {
        this.image = image;
        this.shader = shader;
        this.zIndex = zIndex;
    }

    public VisualObject(Vector2d16 position) : base(position) { }

    public void Render(Vector2d16 position)
    {
        Image.Render(Shader, position);
    }

    public Vector2d16 GetVisualBB()
    {
        return Image.GetVisualBB();
    }
}