using System;

abstract class Renderer
{
    private static IRenderable[] objects;
    public static Vector2d16 worldPosition = new Vector2d16(0, 0);

    public static Vector2d16 Dimensions => new Vector2d16(Config.screenWidth, Config.screenHeight);
    public static int Width => Config.screenWidth;
    public static int Height => Config.screenHeight;

    public static Fragment8[,] buffer = new Fragment8[Config.screenHeight, Config.screenWidth];
    private static VisualObject plain = new VisualObject(
        new Vector2d16() - Dimensions / 2, 
        new Image.Rectangle(
            new Shader.Plain(
                new Color8fg(0, 0, 0), 
                new Color8bg(0, 0, 0), ' '),
            Dimensions * 3,
            0)
        );

    public static void Reload()
    {
        buffer = new Fragment8[Config.screenHeight, Config.screenWidth];
    }

    public static void SetObjects(IRenderable[] objects)
    {
        //.Sort((x, y) => x.Image.zIndex.CompareTo(y.Image.zIndex));
        
        Array.Sort(objects, delegate (IRenderable _1, IRenderable _2) {
            return _1.Image.zIndex.CompareTo(_2.Image.zIndex);
        });
        //objects.OrderBy(IRenderable => IRenderable.Image.zIndex).ToArray<IRenderable>();
        Renderer.objects = objects;
        //Renderer.objects = objects.OrderBy(IRenderable => IRenderable.Image.zIndex).ToArray();
    }

    public static void Render()
    {
        plain.Render(plain.position + worldPosition);
        if (objects != null)
            foreach (IRenderable obj in objects)
            {
                obj.Render(((GameObject)obj).position);
            }
        Game.gameState.hero.Render(Game.gameState.hero.position);
        foreach (IRenderable obj in Game.gameState.hud)
        {
            obj.Render(((GameObject)obj).position + worldPosition);
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

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

                if(shift>0)
                    sb.Append(ANSII.CursorRight(shift));
                sb.Append(fg + bg + current.symbol);
                shift = 0;
            }
        }
        sb.Append(ANSII.ResetInitial());
        Console.Write(sb.ToString());
    }
}