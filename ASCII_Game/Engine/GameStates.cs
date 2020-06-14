abstract class GameState
{
    public abstract void Physics(EInput input);

    public Map map;

    public IRenderable[] hud = new IRenderable[0];

    public KinematicObject hero;
}