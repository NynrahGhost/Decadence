using System;
using System.Threading;

class Game
{
    public static GameState gameState = new GameStates.Menu();
    public static bool running = true;

    public static EInput input = EInput.none;

    public static void Test()
    {
        System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>();

        Shader plainShader = new Shader.Plain(new Color8fg(255, 0, 0), new Color8bg(0, 255, 255), ' ');
        Shader gradientShader = new Shader.Gradient(new Color8fg(255, 0, 0), new Color8fg(255, 255, 255), new Color8bg(255, 0, 0), new Color8bg(0, 0, 255), ' ');


        Shader textureShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\characters.bms"), new Vector2d32(0, 2), new Vector2d32(7, 7));

        Image textureImage = new Image.Rectangle(textureShader, new Vector2d16(7, 6), 127);


        Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(20, 10));
        Image gradientImage = new Image.Rectangle(gradientShader, new Vector2d16(20, 10));
        Image lineImage = new Image.Line(plainShader, new Vector2d16(0, 0), new Vector2d16(5, 0), 1);
        Image curveBezierImage = new Image.BezierCurve(gradientShader, new Vector2d16(10, -5), new Vector2d16(10, -10), new Vector2d16(20, -10), 1);

        Image plainImage2 = new Image.Rectangle(plainShader, new Vector2d16(20, 10));

        Shader blackShader = new Shader.Plain(new Color8fg(0, 0, 0), new Color8bg(0, 0, 0), ' ');
        Image blackImage = new Image.Rectangle(blackShader, new Vector2d16(80, 25), 250);

        //list.Add(new VisualObject( new Vector2d16(-35, 5), plainImage));
        list.Add(new VisualObject(new Vector2d16(30, 10), gradientImage));

        //list.Add(new VisualObject(new Vector2d16(0, 0), blackImage));

        //list.Add(new VisualObject(new Vector2d16(30, 10), plainImage2));

        list.Add(new VisualObject(new Vector2d16(30, 5), textureImage));
        //list.Add(new VisualObject(new Vector2d16(0, 0), blackImage));

        //Renderer.SetObjects(list);
    }

    public Game()
    {
        Config.RestoreDefaults();
        Config.Load(@"Config\config.ini");
        Config.Save(@"Config\" + Config.profileName + ".ini");

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.SetWindowSize(Config.screenWidth, Config.screenHeight);
        Console.CursorVisible = false;

        //Test();
    }

    static void Main(string[] args)
    {
        Game game = new Game();
        
        long delta = DateTime.Now.Ticks;
        int framerate = 1000 / Config.framerate;

        //Console.WriteLine(JSON.ToString(JSON.ToJSON(new Game())));

        //map.

        //Console.WriteLine("‗");
        //Console.WriteLine("♥");
        //Console.OutputEncoding = System.Text.Encoding.ASCII;
        //Thread.Sleep(500);

        
        while (running)
        {
            delta = DateTime.Now.Ticks - delta;

            game.Inputs();
            game.Physics(delta * 0.0000001f);
            game.Render(delta * 0.0000001f);

            delta = DateTime.Now.Ticks;
            Thread.Sleep(framerate);
        }
        /**/
    }

    void Render(float delta)
    {
        Renderer.Render();
    }

    void Physics(float delta)
    {
        gameState.Physics(input);
    }

    void Inputs()
    {
        if (!Console.KeyAvailable)
        {
            input = EInput.none;
            return;
        }
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        char key = keyInfo.KeyChar;

        if(key == Config.moveForward)
        {
            input = EInput.moveForward;
        } 
        else if (key == Config.moveBackward)
        {
            input = EInput.moveBackward;
        } 
        else if (key == Config.moveLeft)
        {
            input = EInput.moveLeft;
        }
        else if (key == Config.moveRight)
        {
            input = EInput.moveRight;
        }


        if (key == Config.moveAltForward)
        {
            Console.Beep();
            input = EInput.moveAltForward;
        }
        else if (key == Config.moveAltBackward)
        {
            input = EInput.moveAltBackward;
        }
        else if (key == Config.moveAltLeft)
        {
            input = EInput.moveAltLeft;
        }
        else if (key == Config.moveAltRight)
        {
            input = EInput.moveAltRight;
        }


        else if (key == Config.use)
        {
            input = EInput.use;
        }
        else if (key == Config.attack)
        {
            input = EInput.attack;
        }

        else if (key == Config.enter)
        {
            input = EInput.enter;
        }
        else if (key == Config.escape)
        {
            input = EInput.escape;
        }
    }

}

enum EInput
{
    none,

    moveForward,
    moveBackward,
    moveLeft,
    moveRight,

    moveAltForward,
    moveAltBackward,
    moveAltLeft,
    moveAltRight,

    use,
    attack,

    enter,
    escape
}