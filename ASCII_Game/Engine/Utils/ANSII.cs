using System;
using System.Collections.Generic;
using System.Text;

abstract class ANSII
{
    public static string CursorPosition(int line, int column)
    {
        return "\x1B[" + line + ';' + column + 'H';
    }

    public static string CursorPosition(Vector2d16 position)
    {
        return "\x1B[" + position._2 + ';' + position._1 + 'H';
    }

    public static string CursorUp(int value)
    {
        return "\x1B[" + value + 'A';
    }

    public static string CursorDown(int value)
    {
        return "\x1B[" + value + 'B';
    }

    public static string CursorRight(int value)
    {
        return "\x1B[" + value + 'C';
    }

    public static string CursorLeft(int value)
    {
        return "\x1B[" + value + 'D';
    }

    public static string ClearScreen => "\x1B[2J";
   

    public static string ResetInitial => "\x1Bc";
    

    public static string Bold => "\x1B[1m";
    public static string BoldOff => "\x1B[21m";

    public static string Faint => "\x1B[2m";
    public static string FaintOff => "\x1B[22m";

    public static string Italic => "\x1B[3m";
    public static string ItalicOff => "\x1B[23m";

    public static string Underline => "\x1B[4m";
    public static string UnderlineOff => "\x1B[24m";

    public static string Blink => "\x1B[5m";
    public static string BlinkOff => "\x1B[25m";

    public static string CrossedOut => "\x1B[5m";
    public static string CrossedOutOff => "\x1B[25m";

    public static string Reverse => "\x1B[7m";
    public static string ReverseOff => "\x1B[27m";

    public static string DefFont => "\x1B[10m";
    public static string AltFont(int n)
    {
        if (n > 10 | n < 1)
            return null;
        return "\x1B["+(n+10)+"m";
    }

    public static string Fraktur => "\x1B[20m";
    public static string FrakturOff => "\x1B[23m";

    public static string Reset => "\x1B[0m]";
}