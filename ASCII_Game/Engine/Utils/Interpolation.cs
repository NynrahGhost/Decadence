using System;
using System.Collections.Generic;
using System.Text;

class Interpolation
{
    public static void Interpolate(object animatable, Action<object, object> parameter, object start, object end, float progress, Func<float, float> function)
    {
        switch (start)
        {
            case byte t:
                parameter(animatable, t.Interpolate((byte)end, progress, function));
                break;
            case short t:
                parameter(animatable, t.Interpolate((short)end, progress, function));
                break;
            case int t:
                parameter(animatable, t.Interpolate((int)end, progress, function));
                break;
            case long t:
                parameter(animatable, t.Interpolate((long)end, progress, function));
                break;
            case float t:
                parameter(animatable, t.Interpolate((float)end, progress, function));
                break;
            case double t:
                parameter(animatable, t.Interpolate((double)end, progress, function));
                break;
            case Vector2d16 t:
                parameter(animatable, t.Interpolate((Vector2d16)end, progress, function));
                break;
        }
    }

    public unsafe static void Interpolate(Wrapper interpolatable, object start, object end, float progress, Func<float, float> function)
    {
        switch (interpolatable)
        {
            case Byte b:
                b.Value = ((byte)start).Interpolate(((byte)end), progress, function);
                break;
            case Short s:
                s.Value = ((short)start).Interpolate(((short)end), progress, function);
                break;
            case Integer i:
                i.Value = ((int)start).Interpolate(((int)end), progress, function);
                break;
            case Long l:
                l.Value = ((long)start).Interpolate(((long)end), progress, function);
                break;
            case Float f:
                f.Value = ((float)start).Interpolate(((float)end), progress, function);
                break;
            case Double d:
                d.Value = ((double)start).Interpolate(((double)end), progress, function);
                break;
        }
        throw new Exception("Attempted interpolation between different objects.");
    }
}