using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// Class for handling interpolation between choosen properties.
/// </summary>
public class Animation : IEvent
{
    bool active = false;
    public float Time { get; set; }

    public enum Interpolator
    {
        /// <summary>Interpolate evenly. Values will be clipped in [0..1] range.</summary>
        Linear,
        /// <summary>Interpolate slow, then fast. Values will be clipped in [0..1] range.</summary>
        Square,
        /// <summary>Interpolate fast, then slow. Values will be clipped in [0..1] range.</summary>
        Root,
        /// <summary>Interpolate instantly. If x > 0.5 outputs 1, otherwise 0.</summary>
        Instant
    }

    List<Interpolator> functions = new List<Interpolator>();

    List<float> timespan = new List<float>();
    List<Vector2d16> frames = new List<Vector2d16>();

    private GameObject animated;

    //public unsafe Wrapper[] properties = new Wrapper[0];

    /// <summary>
    /// Creates base instance of Animation class.<br/>
    /// Further filling is done via chain calls.<br/>
    /// Use <see cref="AddFrames">AddFrames</see> to add new key frame and <see cref="AddFunctions">AddFunctions</see> to define their interpolations.<br/>
    /// They doesn't need to go one after another, first set of functions will refer to first frame,<br/>
    /// second to second, and so on.<br/>
    /// <see cref="SetActive">SetActive(true)</see> should be used after finishing initialization.
    /// </summary>
    public Animation(in GameObject animated)
    {
        this.animated = animated;
    }

    public bool IsActive()
    {
        return active;
    }

    public Animation SetActive(bool active)
    {
        this.active = active;
        return this;
    }

    public Animation AddFrames(params Vector2d16[] frames)
    {
        foreach (Vector2d16 v in frames) this.frames.Add(v);
        return this;
    }

    public Animation AddFunctions(params Interpolator[] functions)
    {
        foreach (Interpolator f in functions) this.functions.Add(f);
        return this;
    }

    public Animation AddTimespans(params float[] times)
    {
        foreach (float f in times) timespan.Add(f);
        return this;
    }

    public void Process(float delta)
    {
        Time += delta;
        Time %= timespan[timespan.Count - 1];
        int current = timespan.FindIndex(f => f > Time);
        float progress = 0;
        if (current == 0 || current == timespan.Count - 1) progress = Time / timespan[current];
        else progress = (Time - timespan[current - 1]) / (timespan[current] - timespan[current - 1]);
        animated.position = Interpolate(frames[current], frames[(current + 1) % timespan.Count], progress, functions[current]);
    }

    private Vector2d16 Interpolate(Vector2d16 start, Vector2d16 end, float progress, Interpolator intr)
    {
        if (progress > 1)
            return end;
        if (progress < 0)
            return start;
        float mult = 0;
        switch (intr)
        {
            case Interpolator.Linear:
                mult = Clip(progress);
                break;
            case Interpolator.Square:
                mult = Clip(progress)*Clip(progress);
                break;
            case Interpolator.Root:
                mult = 1 - (Clip(progress) - 1) * (Clip(progress) - 1);
                break;
            case Interpolator.Instant:
                mult = progress > 0.5 ? 1 : 0;
                break;
        }
        return start + ((end - start) * mult);
    }

    private static float Clip(float x) { if (x < 0) return 0; if (x > 1) return 1; return x; }
}
