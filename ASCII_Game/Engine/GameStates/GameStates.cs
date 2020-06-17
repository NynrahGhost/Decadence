using System.Collections.Generic;
/// <summary>
/// Base class for game states.
/// </summary>
abstract class GameState
{
    /// <summary>
    /// Used for processing input.
    /// </summary>
    public abstract void Physics(EInput input);

    public virtual void Process(float delta) { }

    public List<IEvent> events = new List<IEvent>(0);

    public Map map;

    public IRenderable[] hud = new IRenderable[0];

    public KinematicObject hero;
}