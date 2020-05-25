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

    public static string ClearScreen()
    {
        return "\x1B[2J";
    }

    public static string ResetInitial()
    {
        return "\x1Bc";
    }
}