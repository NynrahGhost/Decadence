using System;
using System.Threading;

class Game
{
    IGameState currentState = new GameState.Intro();
    bool running = true;

    Config config = new Config();
    EInput input = EInput.none;

    Renderer renderer = new Renderer();

    public Game()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.SetWindowSize(config.screenWidth, config.screenHeight);
        Console.CursorVisible = false;

        System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>();

        Shader plainShader = new Shaders.Plain(new Color8fg(255, 0, 0), new Color8bg(0, 255, 255), ' ');
        Shader gradientShader = new Shaders.Gradient(new Color8fg(255, 0, 0), new Color8fg(0, 0, 255), new Color8bg(255, 0, 0), new Color8bg(0, 0, 255), ' ');


        Shader textureShader = new Shaders.Texture(new Atlas16(@"C:\Users\Ghost\source\repos\ASCII_Game\ASCII_Game\Textures\characters.bms"), new Vector2d32(0, 2), new Vector2d32(7, 7));

        Image textureImage = new Image.Rectangle(textureShader, new Vector2d16(7, 6));


        Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(20, 10));
        Image gradientImage = new Image.Rectangle(gradientShader, new Vector2d16(20, 10));
        Image lineImage = new Image.Line(plainShader, new Vector2d16(0, 0), new Vector2d16(5, 0), 1);
        Image curveBezierImage = new Image.BezierCurve(gradientShader, new Vector2d16(10, -5), new Vector2d16(10, -10), new Vector2d16(20, -10), 1);

        Image plainImage2 = new Image.Rectangle(plainShader, new Vector2d16(20, 10));

        Shader blackShader = new Shaders.Plain(new Color8fg(0, 0, 0), new Color8bg(0, 0, 0), ' ');
        Image blackImage = new Image.Rectangle(blackShader, new Vector2d16(80, 25), 0);

        list.Add(new VisualObject( new Vector2d16(10, 10), plainImage));
        list.Add(new VisualObject( new Vector2d16(15, 10), gradientImage));

        list.Add(new VisualObject(new Vector2d16(0, 0), blackImage));

        list.Add(new VisualObject(new Vector2d16(30, 10), plainImage2));

        list.Add(new VisualObject(new Vector2d16(30, 10), textureImage));

        renderer.SetObjects(list);
    }

    static void Main(string[] args)
    {
        Game game = new Game();

        long delta = DateTime.Now.Ticks;
        int framerate = 1000 / game.config.framerate;

        //Console.WriteLine("‗");
        //Console.WriteLine("♥");
        //Console.OutputEncoding = System.Text.Encoding.ASCII;
        //Thread.Sleep(500);
        while (game.running)
        {
            delta = DateTime.Now.Ticks - delta;

            game.Inputs();
            game.Physics(delta * 0.0000001f);
            game.Render(delta * 0.0000001f);
            
            delta = DateTime.Now.Ticks;
            Thread.Sleep(framerate);
        }
        
        Console.SetWindowSize(20, 10);
        Console.WriteLine("Hello World!");
        Console.SetCursorPosition(1, 1);
        Console.Write("\u001b[31m@");
        
    }

    static void Test()
    {
        Console.SetWindowSize(100, 50);
        //Console.WriteLine("\033[38;2;"+((char)0)+";"+ ((char)255) + ";" + ((char)0) + "mHello");

        //Console.WriteLine("\x1b[38;2;255;82;197;48;2;155;106;0mHello");
        //Console.SetCursorPosition(1, 1);
        //Console.Write("\u001b[31m@");
        //Console.Write("\u001b[31m@");

        Color8bg color = new Color8bg(255, 205, 0);
        //Console.Write(color.GetRed());

        Shader plainShader = new Shaders.Plain(new Color8fg(255, 0, 0), new Color8bg(0, 255, 255), ' ');
        Shader gradientShader = new Shaders.Gradient(new Color8fg(255, 0, 0), new Color8fg(0, 0, 255), new Color8bg(255, 0, 0), new Color8bg(0, 0, 255), ' ');

        Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(20, 40));
        Image gradientImage = new Image.Rectangle(gradientShader, new Vector2d16(20, 10));
        Image lineImage = new Image.Line(plainShader, new Vector2d16(0, 0), new Vector2d16(5, 0), 1);
        Image curveBezierImage = new Image.BezierCurve(gradientShader, new Vector2d16(10, -5), new Vector2d16(10, -10), new Vector2d16(20, -10), 1);


        //Console.Write(lineImage.Render().ToString());
        //Console.Write(curveBezierImage.Render().ToString());

        Console.Write('\t');
        Console.Write(plainImage.Render().ToString());
        Console.Write('\t');
        Console.Write(gradientImage.Render().ToString());
        Console.Write('\t');
        Console.Write('\t');
        Console.Write('\t');
        Console.Write(curveBezierImage.Render().ToString());
    }

    void Render(float delta)
    {
        renderer.Render();
    }

    void Physics(float delta)
    {
        switch (input)
        {
            case EInput.moveForward:
                renderer.center._2 -= 1;
                break;
            case EInput.moveBackward:
                renderer.center._2 += 1;
                break;
            case EInput.moveLeft:
                renderer.center._1 -= 2;
                break;
            case EInput.moveRight:
                renderer.center._1 += 2;
                break;
            case EInput.use:
                Console.Beep();
                break;
            case EInput.attack:
                Console.Beep();
                Console.Beep();
                Console.Beep();
                break;
        }
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

        if(key == config.moveForward)
        {
            input = EInput.moveForward;
        } 
        else if (key == config.moveBackward)
        {
            input = EInput.moveBackward;
        } 
        else if (key == config.moveLeft)
        {
            input = EInput.moveLeft;
        }
        else if (key == config.moveRight)
        {
            input = EInput.moveRight;
        }

        else if (key == config.use)
        {
            input = EInput.use;
        }
        else if (key == config.attack)
        {
            input = EInput.attack;
        }

        //Console.WriteLine(Console.ReadKey(true).KeyChar);
    }

}

interface IGameState
{

}

namespace GameState
{
    class Intro : IGameState
    {

    }

    class Menu : IGameState
    {

    }

    class Game : IGameState
    {

    }

    class Editor : IGameState
    {

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
    attack
}

[Flags]
enum EUserInput
{
    moveForward = 1,
    moveBackward = 2,
    moveLeft = 4,
    moveRight = 8,

    moveAltForward = 16,
    moveAltBackward = 32,
    moveAltLeft = 64,
    moveAltRight = 128,

    use = 256,
    attack = 512
}