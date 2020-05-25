using System.Collections.Generic;
using System;

abstract class Renderer
{
    private static GameObject[] objects;
    public static Vector2d16 worldPosition = new Vector2d16(0, 0);

    public static int Width => Config.screenWidth;
    public static int Height => Config.screenHeight;

    public static Fragment8[,] buffer = new Fragment8[Config.screenHeight, Config.screenWidth];

    public static void SetObjects(List<GameObject> objects)
    {
        objects.Sort((x, y) => ((IRenderable)x).Image.zIndex.CompareTo(((IRenderable)y).Image.zIndex));
        Renderer.objects = objects.ToArray();
    }

    public static void Render()
    {
        for (int y = 0; y < Config.screenHeight; ++y)
        {
            for (int x = 0; x < Config.screenWidth; ++x)
            {
                //buffer[y, x] = new Fragment8(new Color8fg(0,0,0), new Color8bg(0,0,0), ' ');
            }
        }
        Console.Write(buffer[0,0]);

        foreach (GameObject obj in objects)
        {
            ((IRenderable)obj).Render(obj.position);
            Console.Write("Here: " + obj.GetBoundingBox());
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append(ANSII.CursorPosition(0, 0));

        int shift = 0;
        Color8fg fg = new Color8fg(0);
        Color8bg bg = new Color8bg(0);
        for (int y = 0; y < Config.screenHeight; ++y)
        {
            sb.Append(ANSII.CursorPosition(y, 0));
            shift = 0;

            for (int x = 0; x < Config.screenWidth; ++x)
            {
                Fragment8 current = buffer[y, x];

                if (current.IsNull())
                {
                    ++shift;
                    continue;
                }

                if (current.foreground != fg)
                {
                    sb.Append(fg);
                    fg = current.foreground;
                }
                if (current.background != bg)
                {
                    sb.Append(bg);
                    bg = current.background;
                }

                //sb.Append(fg+bg+current.symbol);
                if(shift>0)
                    sb.Append(ANSII.CursorRight(shift));
                sb.Append(fg + bg + current.symbol);
                shift = 0;
            }
        }
        sb.Append(ANSII.ResetInitial());
        Console.Write(sb.ToString());

        //Console.Write("\x1B[38;5;0]");

        Console.WriteLine(sb.Length);
    }
}