using System;
using System.Collections.Generic;
using System.Text;

public static class Convert
{
    public static Color8 FromHexToColor8(string color)
    {
        if(color[0] == '#')
            if(color.Length == 3)
            {
                byte res = (byte)(HexToDecimal(color[1]) * HexToDecimal(color[2]));
                return (res, res, res);
            }
            else
            {
                byte r = (byte)(HexToDecimal(color[1]) * HexToDecimal(color[2]));
                byte g = (byte)(HexToDecimal(color[3]) * HexToDecimal(color[4]));
                byte b = (byte)(HexToDecimal(color[5]) * HexToDecimal(color[6]));
                return (r, g, b);
            }
        return default;
    }

    public static byte HexToDecimal(char ch)
    {
        //To lower case
        if (ch > 96)
            ch -= ' ';
        switch (ch)
        {
            case '0':
                return 0;
            case '1':
                return 1;
            case '2':
                return 2;
            case '3':
                return 3;
            case '4':
                return 4;
            case '5':
                return 5;
            case '6':
                return 6;
            case '7':
                return 7;
            case '8':
                return 8;
            case '9':
                return 9;
            case 'A':
                return 10;
            case 'B':
                return 11;
            case 'C':
                return 12;
            case 'D':
                return 13;
            case 'E':
                return 14;
            case 'F':
                return 15;
        }
        return 0;
    }
}
