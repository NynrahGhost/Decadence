﻿using System;

/// <summary>
/// Structure that represents a bundle of two short values.<br/>
/// Supposed to be used as coordinates.<br/>
/// Can be instantiated via tuples.<br/>
/// Example:<br/>
/// <example><code>
/// Vector2d16 vector = (16, 16)
/// </code></example>
/// </summary>
public struct Vector2d16
{
    public short _1;
    public short _2;

    public Vector2d16(short _1, short _2)
    {
        this._1 = _1;
        this._2 = _2;
    }

    public Vector2d16(int _1, int _2)
    {
        this._1 = (short)_1;
        this._2 = (short)_2;
    }

    public Vector2d16(float _1, float _2)
    {
        this._1 = (short)_1;
        this._2 = (short)_2;
    }

    public Vector2d16(double _1, double _2)
    {
        this._1 = (short)_1;
        this._2 = (short)_2;
    }

    /// <summary>
    /// Retrieves distance between two vectors.<br/>
    /// If you need to compare distances towards two separate vectors, use 
    /// <see cref="CompareDistance(Vector2d16, Vector2d16, Vector2d16)">CompareDistance</see> method.
    /// </summary>
    /// <returns></returns>
    public static double Distance(Vector2d16 vector1, Vector2d16 vector2)
    {
        return Math.Sqrt((vector1._1 - vector2._1) * (vector1._1 - vector2._1) + (vector1._2 - vector2._2) * (vector1._2 - vector2._2));
    }

    /// <summary>
    /// Retrieves distance between two vectors.<br/>
    /// If you need to compare distances towards two separate vectors, use 
    /// <see cref="CompareDistance(Vector2d16, Vector2d16, Vector2d16)">CompareDistance</see> method.
    /// </summary>
    /// <returns></returns>
    public double Distance(Vector2d16 vector)
    {
        return Math.Sqrt((_1 - vector._1) * (_1 - vector._1) + (_2 - vector._2) * (_2 - vector._2));
    }

    /// <summary>
    /// Optimized comparision of distances towards a vector.<br/>
    /// Doesn't use square root, thus can be used frequently without performance issues.<br/><br/>
    /// Returns 1, if <paramref name="to1"/> is further then <paramref name="to2"/>, -1 otherwise, and 0 if distances are equal.
    /// </summary>
    public int CompareDistance(Vector2d16 from, Vector2d16 to1, Vector2d16 to2)
    {
        int res =
           (from._1 - to1._1) * (from._1 - to1._1) + (from._2 - to1._2) * (from._2 - to1._2)
           -
           (from._1 - to2._1) * (from._1 - to2._1) + (from._2 - to2._2) * (from._2 - to2._2);

        if (res > 0)
            return 1;
        if (res < 0)
            return -1;
        return 0;
    }

    public static implicit operator Vector2d16((short _1, short _2) tuple)
    {
        return new Vector2d16(tuple._1, tuple._2);
    }

    public static Vector2d16 operator +(Vector2d16 addition1, Vector2d16 addition2)
    {
        return new Vector2d16(addition1._1 + addition2._1, addition1._2 + addition2._2);
    }

    public static Vector2d16 operator -(Vector2d16 addition1, Vector2d16 addition2)
    {
        return new Vector2d16(addition1._1 - addition2._1, addition1._2 - addition2._2);
    }

    public static implicit operator string(Vector2d16 vector)
    {
        return "("+vector._1+","+vector._2+")";
    }

    public static implicit operator Vector2d32(Vector2d16 vector)
    {
        return new Vector2d32(vector._1, vector._2);
    }

    public static Vector2d16 operator *(Vector2d16 vector, double multiplier)
    {
        return new Vector2d16(vector._1 * multiplier, vector._2 * multiplier);
    }

    public static Vector2d16 operator /(Vector2d16 vector, double divisor)
    {
        return new Vector2d16(vector._1 / divisor, vector._2 / divisor);
    }
}

/// <summary>
/// Structure that represents a bundle of two int values.<br/>
/// Supposed to be used as line & character in file reading.
/// Can be instantiated via tuples.<br/>
/// Example:<br/>
/// <example><code>
/// Vector2d32 vector = (32, 32)
/// </code></example>
/// </summary>
public struct Vector2d32
{
    public int _1;
    public int _2;

    public Vector2d32(short _1, short _2)
    {
        this._1 = _1;
        this._2 = _2;
    }

    public Vector2d32(int _1, int _2)
    {
        this._1 = _1;
        this._2 = _2;
    }

    public Vector2d32(float _1, float _2)
    {
        this._1 = (int)_1;
        this._2 = (int)_2;
    }

    public Vector2d32(double _1, double _2)
    {
        this._1 = (int)_1;
        this._2 = (int)_2;
    }

    public static double Distance(Vector2d32 vector1, Vector2d32 vector2)
    {
        return Math.Sqrt((vector1._1 - vector2._1) * (vector1._1 - vector2._1) + (vector1._2 - vector2._2) * (vector1._2 - vector2._2));
    }

    public double Distance(Vector2d32 vector)
    {
        return Math.Sqrt((_1 - vector._1) * (_1 - vector._1) + (_2 - vector._2) * (_2 - vector._2));
    }

    public static implicit operator Vector2d32((int _1, int _2) tuple)
    {
        return new Vector2d32(tuple._1, tuple._2);
    }

    public static Vector2d32 operator +(Vector2d32 addition1, Vector2d32 addition2)
    {
        return new Vector2d32(addition1._1 + addition2._1, addition1._2 + addition2._2);
    }

    public static Vector2d32 operator -(Vector2d32 addition1, Vector2d32 addition2)
    {
        return new Vector2d32(addition1._1 - addition2._1, addition1._2 - addition2._2);
    }

    public static Vector2d32 operator *(Vector2d32 vector, double multiplier)
    {
        return new Vector2d32(vector._1 * multiplier, vector._2 * multiplier);
    }

    public static Vector2d32 operator /(Vector2d32 vector, double divisor)
    {
        return new Vector2d32(vector._1 / divisor, vector._2 / divisor);
    }
}

/// <summary>
/// Structure that represents a bundle of two float values.<br/>
/// Can be instantiated via tuples.<br/>
/// Example:<br/>
/// <example><code>
/// Vector2d32f vector = (32f, 32f)
/// </code></example>
/// </summary>
public struct Vector2d32f
{
    public float _1;
    public float _2;

    public Vector2d32f(short _1, short _2)
    {
        this._1 = _1;
        this._2 = _2;
    }

    public Vector2d32f(int _1, int _2)
    {
        this._1 = _1;
        this._2 = _2;
    }

    public Vector2d32f(float _1, float _2)
    {
        this._1 = _1;
        this._2 = _2;
    }

    public Vector2d32f(double _1, double _2)
    {
        this._1 = (float)_1;
        this._2 = (float)_2;
    }

    public static double Distance(Vector2d32f vector1, Vector2d32f vector2)
    {
        return Math.Sqrt((vector1._1 - vector2._1) * (vector1._1 - vector2._1) + (vector1._2 - vector2._2) * (vector1._2 - vector2._2));
    }

    public double Distance(Vector2d32f vector)
    {
        return Math.Sqrt((_1 - vector._1) * (_1 - vector._1) + (_2 - vector._2) * (_2 - vector._2));
    }

    public static implicit operator Vector2d32f((float _1, float _2) tuple)
    {
        return new Vector2d32f(tuple._1, tuple._2);
    }

    public static Vector2d32f operator +(Vector2d32f addition1, Vector2d32f addition2)
    {
        return new Vector2d32f(addition1._1 + addition2._1, addition1._2 + addition2._2);
    }

    public static Vector2d32f operator -(Vector2d32f addition1, Vector2d32f addition2)
    {
        return new Vector2d32f(addition1._1 - addition2._1, addition1._2 - addition2._2);
    }

    public static Vector2d32f operator *(Vector2d32f vector, double multiplier)
    {
        return new Vector2d32f(vector._1 * multiplier, vector._2 * multiplier);
    }

    public static Vector2d32f operator /(Vector2d32f vector, double divisor)
    {
        return new Vector2d32f(vector._1 / divisor, vector._2 / divisor);
    }
}

/// <summary>
/// Structure that represents a bundle of two double values.
/// /// Can be instantiated via tuples.<br/>
/// Example:<br/>
/// <example><code>
/// Vector2d64f vector = (64., 64.)
/// </code></example>
/// </summary>
public struct Vector2d64f
{
    public double _1;
    public double _2;

    public Vector2d64f(short _1, short _2)
    {
        this._1 = _1;
        this._2 = _2;
    }

    public Vector2d64f(int _1, int _2)
    {
        this._1 = _1;
        this._2 = _2;
    }

    public Vector2d64f(double _1, double _2)
    {
        this._1 = _1;
        this._2 = _2;
    }

    public static double Distance(Vector2d64f vector1, Vector2d64f vector2)
    {
        return Math.Sqrt((vector1._1 - vector2._1) * (vector1._1 - vector2._1) + (vector1._2 - vector2._2) * (vector1._2 - vector2._2));
    }

    public double Distance(Vector2d64f vector)
    {
        return Math.Sqrt((_1 - vector._1) * (_1 - vector._1) + (_2 - vector._2) * (_2 - vector._2));
    }

    public static implicit operator Vector2d64f((double _1, double _2) tuple)
    {
        return new Vector2d64f(tuple._1, tuple._2);
    }

    public static Vector2d64f operator +(Vector2d64f addition1, Vector2d64f addition2)
    {
        return new Vector2d64f(addition1._1 + addition2._1, addition1._2 + addition2._2);
    }

    public static Vector2d64f operator -(Vector2d64f addition1, Vector2d64f addition2)
    {
        return new Vector2d64f(addition1._1 - addition2._1, addition1._2 - addition2._2);
    }

    public static Vector2d64f operator *(Vector2d64f vector, double multiplier)
    {
        return new Vector2d64f(vector._1 * multiplier, vector._2 * multiplier);
    }

    public static Vector2d64f operator /(Vector2d64f vector, double divisor)
    {
        return new Vector2d64f(vector._1 / divisor, vector._2 / divisor);
    }
}

/// <summary>
/// Structure that represents byte value as angle (0-360).<br/>
/// Currently not supported.
/// </summary>
public struct Angle
{
    private byte angle;

    public Angle(double angle)
    {
        this.angle = (byte) (angle / 360 * 255);       
    }

    public static implicit operator double(Angle angle)
    {
        return 360 / 255 * angle.angle;
    }

    public static implicit operator Angle(double angle)
    {
        return new Angle(angle);
    }
}

/// <summary>
/// Interface for common color operations.<br/>
/// Not used.
/// </summary>
public interface Color
{
    public bool IsNull();

    public byte GetRed();

    public byte GetGreen();

    public byte GetBlue();
}

/// <summary>
/// Structure that represents an 8-bit color.<br/>
/// Due to the way of ASCII escape sequences handling foreground and background colors,<br/>
/// they're divided in two different structs.
/// </summary>
public struct Color8 : Color
{
    byte color;

    public Color8(byte color)
    {
        this.color = color;
    }

    public Color8(byte r, byte g, byte b)
    {
        r /= 51; g /= 51; b /= 51;
        color = (byte)(r * 36 + g * 6 + b + 16);
    }

    public static Color8 GetNull()
    {
        return new Color8(0);
    }

    public bool IsNull()
    {
        return color == 0;
    }

    public byte GetRed()
    {
        return (byte)((color - 16) / 36 % 6 * 51);
    }

    public byte GetGreen()
    {
        return (byte)((color - 16) / 6 % 6 * 51);
    }

    public byte GetBlue()
    {
        return (byte)((color - 16) % 6 * 51);
    }

    public static implicit operator Color8(string str)
    {
        return Convert.FromHexToColor8(str);
    }

    public static implicit operator Color8((byte r, byte g, byte b) tuple)
    {
        return new Color8(tuple.r, tuple.g, tuple.b);
    }

    public string AsForeground()
    {
        if (color == 0)
            return null;
        return "\u001b[38;5;" + color + 'm';
    }

    public string AsBackground()
    {
        if (color == 0)
            return null;
        return "\u001b[48;5;"+ color +'m';
    }

    public static bool operator ==(Color8 color1, Color8 color2)
    {
        return color1.color == color2.color;
    }

    public static bool operator !=(Color8 color1, Color8 color2)
    {
        return color1.color != color2.color;
    }

    public static bool operator ==(Color8 color, int integer)
    {
        return color.color == integer;
    }

    public static bool operator !=(Color8 color, int integer)
    {
        return color.color != integer;
    }
}

/// <summary>
/// Structure that represents a 24-bit foreground color.<br/>
/// Currently not supported.
/// </summary>
public struct Color24fg : Color
{
    byte red;
    byte green;
    byte blue;

    public Color24fg(int r, int g, int b)
    {
        red = (byte)r;
        green = (byte)g;
        blue = (byte)b;
    }

    public byte GetRed()
    {
        return red;
    }

    public byte GetGreen()
    {
        return green;
    }

    public byte GetBlue()
    {
        return blue;
    }

    public bool IsNull()
    {
        return red + green + blue == 0;
    }

    public static implicit operator string(Color24fg color)
    {
        return "\u001b[38;2;" + color.red + ';' + color.green + ';' + color.blue + 'm';
    }
}

/// <summary>
/// Structure that represents a 24-bit background color.<br/>
/// Currently not supported.
/// </summary>
public struct Color24bg : Color
{
    byte red;
    byte green;
    byte blue;

    public Color24bg(int r, int g, int b)
    {
        red = (byte) r;
        green = (byte)g;
        blue = (byte) b;
    }

    public byte GetRed()
    {
        return red;
    }

    public byte GetGreen()
    {
        return green;
    }

    public byte GetBlue()
    {
        return blue;
    }

    public bool IsNull()
    {
        return red + green + blue == 0;
    }

    public static implicit operator string(Color24bg color)
    {
        return "\u001b[48;2;"+color.red+';'+ color.green +';'+ color.blue +'m';
    }
}

/// <summary>
/// Structure that holds information about a symbol, its color and background.<br/>
/// Uses 8-bit color.
/// </summary>
public struct Fragment8
{
    public Color8 foreground;
    public Color8 background;
    public char symbol;

    public static Fragment8 nullFragment = new Fragment8(Color8.GetNull(), Color8.GetNull(), (char)0);

    public static Fragment8 GetNull()
    {
        //return new Fragment8(Color8fg.GetNull(), Color8bg.GetNull(), ' ');
        return nullFragment;
    }

    public bool IsNull()
    {
        return foreground.IsNull() && background.IsNull() && (symbol == 0);
    }

    public Fragment8(Color8 foreground, Color8 background, char symbol)
    {
        this.foreground = foreground;
        this.background = background;
        this.symbol = symbol;
    }

    public static implicit operator string(Fragment8 fragment)
    {
        return fragment.foreground.AsForeground() + fragment.background.AsBackground() + fragment.symbol;
    }
}

/// <summary>
/// Structure that holds information about a symbol, its color and background.<br/>
/// Uses 24-bit color.
/// </summary>
public struct Fragment24
{
    public Color24fg foreground;
    public Color24bg background;
    public char symbol;

    public static Fragment24 nullFragment = new Fragment24(new Color24fg(0,0,0), new Color24bg(0,0,0), (char)0);

    public bool IsNull()
    {
        return foreground.IsNull() && background.IsNull() && (symbol == 0);
    }

    public Fragment24(Color24fg foreground, Color24bg background, char symbol)
    {
        this.foreground = foreground;
        this.background = background;
        this.symbol = symbol;
    }

    public static implicit operator string(Fragment24 fragment)
    {
        return fragment.foreground + fragment.background + fragment.symbol;
    }
}