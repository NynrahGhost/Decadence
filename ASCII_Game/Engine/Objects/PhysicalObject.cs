using System.Text;

/// <summary>
/// Object that can both participate in collision detection and be rendered.
/// </summary>
class PhysicalObject : GameObject, IRenderable, ICollidable
{
    public Image Image { get => Game.gameState.map.images[image]; set => System.Array.IndexOf(Game.gameState.map.images, value); }
    public Shader Shader { get => Game.gameState.map.shaders[shader]; set => System.Array.IndexOf(Game.gameState.map.shaders, value); }
    public byte ZIndex { get => zIndex; set => zIndex = value; }
    public Shape Shape { get => Game.gameState.map.shapes[shape]; set => System.Array.IndexOf(Game.gameState.map.shapes, value); }

    protected int image;
    protected int shader;
    protected byte zIndex;
    protected int shape;

    public PhysicalObject(Vector2d16 position, int image, int shader, byte zIndex, int shape) : base(position)
    {
        this.image = image;
        this.shader = shader;
        this.zIndex = zIndex;
        this.shape = shape;
    }

    public void Render(Vector2d16 position)
    {
        Image.Render(Shader, position);
    }

    public Vector2d16 GetVisualBB()
    {
        return Image.GetVisualBB();
    }

    public bool Collide(ICollidable obj)
    {
        throw new System.NotImplementedException();
    }
}