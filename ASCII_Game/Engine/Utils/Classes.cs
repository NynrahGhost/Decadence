using System;
using System.Collections.Generic;
using System.Text;

/*unsafe class Wrapper<T>
{
    public T value;
}
/*
class Wrapper<T>
{
    public T value;

    public static object Interpolate(object start, object end, float progress, Func<float, float> function)
    {
        Wrapper<int> integer;
        switch (start)
        {
            case Byte i:
                return i.value.Interpolate(((Byte)end).value, progress, function);
            case Short i:
                return i.value.Interpolate(((Short)end).value, progress, function);
            case Integer i:
                return i.value.Interpolate(((Integer)end).value, progress, function);
            case Float i:
                return i.value.Interpolate(((Float)end).value, progress, function);
            case Double i:
                return i.value.Interpolate(((Double)end).value, progress, function);
        }
        throw new Exception("Attempted interpolation between different objects.");
    }
}*/

public interface Wrapper
{
    public unsafe object Value { get; set; }
}

public class Byte : Wrapper, IDisposable
{
    public unsafe object Value
    {
        get => *value;
        set
        {
            *this.value = (byte)value;
        }
    }

    public unsafe byte* value;

    public unsafe Byte(byte* value) { }

    public void Dispose() { }
}
public class Short : Wrapper, IDisposable
{
    public unsafe object Value
    {
        get => *value;
        set
        {
            *this.value = (short)value;
        }
    }

    public unsafe short* value;

    public unsafe Short(short* value) { }

    public void Dispose() { }
}
public class Integer : Wrapper, IDisposable
{
    public unsafe object Value
    {
        get => *value;
        set
        {
            *this.value = (int)value;
        }
    }

    public unsafe int* value;

    public unsafe Integer(int* value) { }

    public void Dispose() { }
}
public class Long : Wrapper, IDisposable
{
    public unsafe object Value
    {
        get => *value;
        set
        {
            *this.value = (long)value;
        }
    }

    public unsafe long* value;

    public unsafe Long(long* value) { }

    public void Dispose() { }
}
public class Float : Wrapper, IDisposable
{
    public unsafe object Value
    {
        get => *value;
        set
        {
            *this.value = (float)value;
        }
    }

    public unsafe float* value;

    public unsafe Float(float* value) { }

    public void Dispose() { }
}
public class Double : Wrapper, IDisposable
{
    public unsafe object Value
    {
        get => *value;
        set
        {
            *this.value = (double)value;
        }
    }

    public unsafe double* value;

    public unsafe Double(double* value) { }

    public void Dispose() { }
}
public class Vector2d16W : Wrapper, IDisposable
{
    public unsafe object Value
    {
        get => *value;
        set
        {
            *this.value = (Vector2d16)value;
        }
    }

    public unsafe Vector2d16* value;

    public unsafe Vector2d16W(Vector2d16* value) { Console.WriteLine(new Color8fg(255, 255, 255) + *value); }

    public void Dispose() { }
}