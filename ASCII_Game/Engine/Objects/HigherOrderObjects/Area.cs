using System.Collections.Generic;

/// <summary>
/// Object used for invoking events based on character's interaction with it.<br/>
/// Events:<br/>
/// <see cref="OnEnter">OnEnter</see><br/>
/// <see cref="OnStay">OnStay</see><br/>
/// <see cref="OnExit">OnExit</see><br/>
/// </summary>
class Area : TactileObject
{
    public List<KinematicObject> objects;

    public System.Action<KinematicObject> OnEnter;
    public System.Action<KinematicObject> OnStay;
    public System.Action<KinematicObject> OnExit;

    public Area(
        Vector2d16 position,
        Shape shape,
        System.Action<KinematicObject> OnEnter,
        System.Action<KinematicObject> OnStay,
        System.Action<KinematicObject> OnExit
    ) : base(position, shape)
    {
        this.OnEnter = OnEnter;
        this.OnStay = OnStay;
        this.OnExit = OnExit;
    }

    public Area(
        Vector2d16 position,
        Shape shape,
        System.Action<KinematicObject> OnEnter,
        System.Action<KinematicObject> OnStay,
        System.Action<KinematicObject> OnExit,
        List<KinematicObject> objects
    ) : this(position, shape, OnEnter, OnStay, OnExit)
    {
        this.objects = objects;
    }

    public void Add(KinematicObject obj)
    {
        objects.Add(obj);
        OnEnter(obj);
    }
}