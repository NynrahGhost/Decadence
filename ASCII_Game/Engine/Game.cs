using System;
using System.Threading;

class Game
{
    public static GameState gameState = new GameStates.Menu();
    public static bool running = true;

    public static EInput input = EInput.none;

    public Game()
    {
        Config.RestoreDefaults();
        Config.Load(@"Config\config.ini");
        //Config.Save(@"Config\congig.ini");

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
            game.Physics(delta);// * 0.0000001f);
            game.Render(delta);// * 0.0000001f);

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
        gameState.Process(delta);
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


        else if (key == Config.inventory)
        {
            input = EInput.inventory;
        }
        else if (key == Config.map)
        {
            input = EInput.map;
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

    inventory,
    map,

    enter,
    escape
}