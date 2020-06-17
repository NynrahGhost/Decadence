using System;

static class ArrayExtension
{
    /// <summary>
    /// Extend array by given number of elements.
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    public static Array Extend(this Array arr, int number)
    {
        arr.GetValue(0).GetType();
        Array res = new Array[arr.Length + number];
        arr.CopyTo(res, 0);
        return res;
    }

    /// <summary>
    /// Extend array by one element.
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    /*public static Array Extend(this Array arr)
    {
        Array res = new Array[arr.Length + 1];
        arr.CopyTo(res, 0);
        return res;
    }*/

    public static object[] Extend(this object[] arr)
    {
        object[] res = new object[arr.Length + 1];
        arr.CopyTo(res, 0);
        return res;
    }

    public static long[] Extend(this long[] arr)
    {
        long[] res = new long[arr.Length + 1];
        arr.CopyTo(res, 0);
        return res;
    }

    public static string ToString(this Fragment8[,] frag)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int y = 0; y < frag.Length; ++y)
            for (int x = 0; x < frag.Length; ++x)
                sb.Append(frag[y,x]);
        return sb.ToString();
    }
}

static class InterpolationPrimitive
{
    public static void Interpolate(object start, object end, float progress, Func<float, float> function)
    {
        switch (start)
        {
            case byte b:
                b.Interpolate((byte)end, progress, function);
                break;
            case short s:
                s.Interpolate((short)end, progress, function);
                break;
            case int i:
                i.Interpolate((int)end, progress, function);
                break;
            case float f:
                f.Interpolate((float)end, progress, function);
                break;
            case double d:
                d.Interpolate((double)end, progress, function);
                break;
        }
    }

    public static byte Interpolate(this byte begin, byte end, float progress, Func<float, float> function)
    {
        if (progress > 1)
            return end;
        if (progress < 0)
            return begin;
        return (byte)(begin + (end - begin) * function(progress));
    }

    public static short Interpolate(this short begin, short end, float progress, Func<float, float> function)
    {
        if (progress > 1)
            return end;
        if (progress < 0)
            return begin;
        return (short)(begin + (end - begin) * function(progress));
    }

    public static int Interpolate(this int begin, int end, float progress, Func<float, float> function)
    {
        if (progress > 1)
            return end;
        if (progress < 0)
            return begin;
        return (int)(begin + (end - begin) * function(progress));
    }

    public static long Interpolate(this long begin, long end, float progress, Func<float, float> function)
    {
        if (progress > 1)
            return end;
        if (progress < 0)
            return begin;
        return (long)(begin + (end - begin) * function(progress));
    }

    public static float Interpolate(this float begin, float end, float progress, Func<float, float> function)
    {
        if (progress > 1)
            return end;
        if (progress < 0)
            return begin;
        return begin + ((end - begin) * function(progress));
    }

    public static double Interpolate(this double begin, double end, float progress, Func<float, float> function)
    {
        if (progress > 1)
            return end;
        if (progress < 0)
            return begin;
        return begin + ((end - begin) * function(progress));
    }

    public static Vector2d16 Interpolate(this Vector2d16 begin, Vector2d16 end, float progress, Func<float, float> function)
    {
        if (progress > 1)
            return end;
        if (progress < 0)
            return begin;
        return begin + ((end - begin) * function(progress));
    }
}