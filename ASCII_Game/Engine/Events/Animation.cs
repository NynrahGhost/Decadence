using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// Class for handling interpolation between choosen properties.
/// </summary>
public class Animation : IEvent
{
    bool active = false;
    public float time = 0;

    Func<float, float>[][] functions;
    /// <summary>Interpolate evenly. Values will be clipped in [0..1] range.</summary>
    public static readonly Func<float, float> liniar = (float x) => { return x; };
    /// <summary>Interpolate slow, then fast. Values will be clipped in [0..1] range.</summary>
    public static readonly Func<float, float> square = (float x) => { x = x < 0 ? 0 : (x > 1 ? 1 : x); return x * x; };
    /// <summary>Interpolate fast, then slow. Values will be clipped in [0..1] range.</summary>
    public static readonly Func<float, float> root = (float x) => { x = x < 0 ? 0 : (x > 1 ? 1 : x);  return 1 - (x - 1) * (x - 1); };
    /// <summary>Interpolate instantly. If x > 0.5 outputs 1, otherwise 0.</summary>
    public static readonly Func<float, float> instant = (float x) => { return x > 0.5 ? 1 : 0; };

    float[] timespan;
    object[][] frames;

    object animated;
    public Action<object, object>[] properties;

    //public unsafe Wrapper[] properties = new Wrapper[0];

    /// <summary>
    /// Creates base instance of Animation class.<br/>
    /// Further filling is done via chain calls.<br/>
    /// Always start with <see cref="SetProperties">SetProperties</see> method.<br/>
    /// Use <see cref="AddFrame">AddFrame</see> to add new key frame and <see cref="AddFunctions">AddFunctions</see> to define their interpolations.<br/>
    /// They doesn't need to go one after another, first set of functions will refer to first frame,<br/>
    /// second to second, and so on.<br/>
    /// <see cref="SetActive">SetActive(true)</see> should be used after finishing initialization.
    /// </summary>
    public Animation(object animated)
    {
        this.animated = animated;
    }

    public bool IsActive()
    {
        return active;
    }

    public Animation SetProperties(params Action<object, object>[] properties)
    {
        this.properties = properties;
        return this;
    }

    public Animation SetPhase(float time)
    {
        this.time = time;
        return this;
    }

    public Animation SetActive(bool active)
    {
        this.active = active;
        return this;
    }

    public Animation AddFrame(params object[] frame)
    {
        if (frame.Length != properties.Length)
            throw new ArgumentException("Invalid number of parameters. Got " + frame.Length + ", expected " + properties.Length);
        frames = Extend(frames);
        frames[frames.Length-1] = frame;
        return this;
    }

    public Animation AddFunctions(params Func<float, float>[] functions)
    {
        if (functions.Length != properties.Length)
            throw new ArgumentException("Invalid number of parameters. Got " + functions.Length + ", expected " + properties.Length);
        this.functions = Extend(this.functions);
        this.functions[this.functions.Length - 1] = functions;
        return this;
    }

    public Animation AddTimespan(float time)
    {
        timespan = Extend(timespan);
        timespan[timespan.Length - 1] = time;
        return this;
    }

    public void Process(float delta)
    {
        time += delta;

        if(time > timespan[timespan.Length - 1])
        {
            time %= timespan[timespan.Length - 1];
        }

        int current = -1;
        for(int i = 0; i < timespan.Length; ++i)
        {
            if (timespan[i] > time)
            {
                current = i;
                break;
            }
        }

        if (current == 0)
        {
            for (int x = 0; x < frames[0].Length; ++x)
                Interpolation.Interpolate(
                    animated,
                    properties[x],
                    frames[current][x],
                    frames[current + 1][x],
                    time / timespan[current],
                    functions[current][x]);
            return;
        }

        if (current == timespan.Length-1)
        {
            for (int x = 0; x < frames[0].Length; ++x)
                Interpolation.Interpolate(
                    animated,
                    properties[x],
                    frames[current][x],
                    frames[0][x],
                    time / timespan[current],
                    functions[current][x]);
            return;
        }

        for (int x = 0; x < frames[0].Length; ++x)
            Interpolation.Interpolate(
                animated, 
                properties[x], 
                frames[current][x], 
                frames[current + 1][x], 
                (time - timespan[current - 1])/(timespan[current] - timespan[current-1]), 
                functions[current][x]);
    }

    private static T[] Extend<T>(T[] arr)
    {
        if (arr == null)
            return new T[1];
        T[] res = new T[arr.Length + 1];
        arr.CopyTo(res, 0);
        return res;
    }
}
