class Config
{
    public static int screenWidth;
    public static int screenHeight;

    public static int framerate;

    public static char moveForward;
    public static char moveBackward;
    public static char moveLeft;
    public static char moveRight;


    public static char moveAltForward = (char)System.ConsoleKey.UpArrow;
    public static char moveAltBackward = (char)System.ConsoleKey.DownArrow;
    public static char moveAltLeft = (char)System.ConsoleKey.LeftArrow;
    public static char moveAltRight = (char)System.ConsoleKey.RightArrow;
    

    public static char use;
    public static char attack;

    public static char escape = (char)System.ConsoleKey.Escape;

    static Config()
    {
        RestoreDefaults();
    }

    public static void RestoreDefaults()
    {
        screenWidth = 100;
        screenHeight = 50;

        framerate = 15;

        moveForward = 'w';
        moveBackward = 's';
        moveLeft = 'a';
        moveRight = 'd';

        use = 'e';
        attack = ' ';
    }

    public Config() { }

    public Config(string path) 
    {
        
    }

    public Config(char[] settings)
    {
        moveForward = settings[0];
        moveBackward = settings[1];
        moveLeft = settings[2];
        moveRight = settings[3];
    }
}