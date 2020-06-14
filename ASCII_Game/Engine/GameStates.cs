abstract class GameState
{
    public abstract void Physics(EInput input);

    public Map map;

    public KinematicObject hero;
}