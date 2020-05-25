using System;

struct Vector2d16
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

    public static double Distance(Vector2d16 vector1, Vector2d16 vector2)
    {
        return Math.Sqrt((vector1._1 - vector2._1) * (vector1._1 - vector2._1) + (vector1._2 - vector2._2) * (vector1._2 - vector2._2));
    }

    public double Distance(Vector2d16 vector)
    {
        return Math.Sqrt((_1 - vector._1) * (_1 - vector._1) + (_2 - vector._2) * (_2 - vector._2));
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

struct Vector2d32
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

struct Vector2d32f
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

struct Vector2d64f
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

struct Angle
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

struct Color8fg
{
    byte color;

    public Color8fg(byte color) 
    {
        this.color = color;
    }

    public Color8fg(int r, int g, int b)
    {
        r /= 51; g /= 51; b /= 51;
        color = (byte)(r * 36 + g * 6 + b + 16);
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

    public static implicit operator string(Color8fg color)
    {
        if (color.color == 0)
            return null;
        return "\u001b[38;5;"+color.color+'m';
    }

    public static bool operator ==(Color8fg color, int integer)
    {
        return color.color == integer;
    }

    public static bool operator !=(Color8fg color, int integer)
    {
        return color.color != integer;
    }
}

struct Color8bg
{
    byte color;

    public Color8bg(byte color)
    {
        this.color = color;
    }

    public Color8bg(byte r, byte g, byte b)
    {
        r /= 51; g /= 51; b /= 51;
        color = (byte)(r * 36 + g * 6 + b + 16);
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

    public static implicit operator string(Color8bg color)
    {
        if (color.color == 0)
            return null;
        return "\u001b[48;5;"+ color.color +'m';
    }

    public static bool operator ==(Color8bg color, int integer)
    {
        return color.color == integer;
    }

    public static bool operator !=(Color8bg color, int integer)
    {
        return color.color != integer;
    }
}

struct Color24fg
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

    public static implicit operator string(Color24fg color)
    {
        return "\u001b[38;2;" + color.red + ';' + color.green + ';' + color.blue + 'm';
    }
}

struct Color24bg
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

    public static implicit operator string(Color24bg color)
    {
        return "\u001b[48;2;"+color.red+';'+ color.green +';'+ color.blue +'m';
    }
}

struct Fragment8
{
    public Color8fg foreground;
    public Color8bg background;
    public char symbol;

    public static Fragment8 nullFragment = new Fragment8(new Color8fg(0), new Color8bg(0), (char)0);

    public bool IsNull()
    {
        return foreground.IsNull() && background.IsNull() && (symbol == 0);
    }

    public Fragment8(Color8fg foreground, Color8bg background, char symbol)
    {
        this.foreground = foreground;
        this.background = background;
        this.symbol = symbol;
    }

    public static implicit operator string(Fragment8 fragment)
    {
        return fragment.foreground + fragment.background + fragment.symbol;
    }
}

