using System;

/// <summary>
/// Class used for rendering objects on screen.<br/>
/// Use <see cref="SetObjects(IRenderable[])">SetObjects(IRenderable[])</see> to put objects for rendering.
/// </summary>
abstract class Renderer
{
    private static IRenderable[] objects;
    public static Vector2d16 worldPosition = new Vector2d16(0, 0);

    public static Vector2d16 Dimensions => new Vector2d16(Config.screenWidth, Config.screenHeight);
    public static int Width => Config.screenWidth;
    public static int Height => Config.screenHeight;

    public static Fragment8[,] buffer = new Fragment8[Config.screenHeight, Config.screenWidth];

    // false - off, true - on
    private static bool bold = false;
    private static bool faint = false;
    private static bool italic = false;
    private static bool underline = false;
    private static bool crossedOut = false;
    private static bool blink = false;
    private static bool reverse = false;
    private static bool fraktur = false;

    private static Image.Rectangle plainImage = new Image.Rectangle(Dimensions);
    private static Shader.Plain plainShader = new Shader.Plain((255,255,255), (0,0,0), ' ');


    public static void Reload()
    {
        buffer = new Fragment8[Config.screenHeight, Config.screenWidth];
    }

    public static void SetObjects(IRenderable[] objects)
    {
        /*
        Array.Sort(objects, delegate (IRenderable _1, IRenderable _2) {
            return _1.Image.zIndex.CompareTo(_2.Image.zIndex);
        });
        */

        Sort.QuickSort(objects);

        Renderer.objects = objects;
    }

    public static void Render()
    {
        plainImage.Render(plainShader, worldPosition);

        if (objects != null)
            foreach (IRenderable obj in objects)
            {
                obj.Render(obj.Position);
            }
        //Game.gameState.hero?.Render(Game.gameState.hero.position);
        foreach (IRenderable obj in Game.gameState.hud)
        {
            obj.Render(obj.Position + worldPosition);
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        int shift = 0;
        Color8 fg = new Color8(0);
        Color8 bg = new Color8(0);
        
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

                switch (current.symbol)
                {
                    case (char)0xDB80:
                        switch (bold)
                        {
                            case false:
                                sb.Append(ANSII.Bold);
                                bold = true;
                                continue;
                            case true:
                                sb.Append(ANSII.BoldOff);
                                bold = false;
                                continue;
                        }
                    case (char)0xDB81:
                        switch (faint)
                        {
                            case false:
                                sb.Append(ANSII.Faint);
                                faint = true;
                                continue;
                            case true:
                                sb.Append(ANSII.FaintOff);
                                faint = false;
                                continue;
                        }
                    case (char)0xDB82:
                        switch (italic)
                        {
                            case false:
                                sb.Append(ANSII.Italic);
                                italic = true;
                                continue;
                            case true:
                                sb.Append(ANSII.ItalicOff);
                                italic = false;
                                continue;
                        }
                    case (char)0xDB83:
                        switch (underline)
                        {
                            case false:
                                sb.Append(ANSII.Underline);
                                underline = true;
                                continue;
                            case true:
                                sb.Append(ANSII.UnderlineOff);
                                underline = false;
                                continue;
                        }
                    case (char)0xDB84:
                        switch (crossedOut)
                        {
                            case false:
                                sb.Append(ANSII.CrossedOut);
                                crossedOut = true;
                                continue;
                            case true:
                                sb.Append(ANSII.CrossedOutOff);
                                crossedOut = false;
                                continue;
                        }
                    case (char)0xDB85:
                        switch (blink)
                        {
                            case false:
                                sb.Append(ANSII.Blink);
                                blink = true;
                                continue;
                            case true:
                                sb.Append(ANSII.BlinkOff);
                                blink = false;
                                continue;
                        }
                    case (char)0xDB86:
                        switch (reverse)
                        {
                            case false:
                                sb.Append(ANSII.Reverse);
                                reverse = true;
                                continue;
                            case true:
                                sb.Append(ANSII.ReverseOff);
                                reverse = false;
                                continue;
                        }
                    case (char)0xDB87:
                        switch (fraktur)
                        {
                            case false:
                                sb.Append(ANSII.Fraktur);
                                fraktur = true;
                                continue;
                            case true:
                                sb.Append(ANSII.FrakturOff);
                                fraktur = false;
                                continue;
                        }
                }

                if (current.foreground != fg)
                {
                    sb.Append(fg.AsForeground());
                    fg = current.foreground;
                }
                if (current.background != bg)
                {
                    sb.Append(bg.AsBackground());
                    bg = current.background;
                }

                if(shift>0)
                    sb.Append(ANSII.CursorRight(shift));
                sb.Append(fg.AsForeground() + bg.AsBackground() + current.symbol);
                shift = 0;
            }
        }
        sb.Append(ANSII.ResetInitial);
        Console.Write(sb.ToString());
    }
}