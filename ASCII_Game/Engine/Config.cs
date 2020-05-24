class Config
{
    public readonly int screenWidth = 100;
    public readonly int screenHeight = 35;

    public readonly int framerate = 15;

    public readonly char moveForward = 'w';
    public readonly char moveBackward = 's';
    public readonly char moveLeft = 'a';
    public readonly char moveRight = 'd';

    /*
    char MoveAltForward = 'w';
    char moveBackward = 's';
    char moveLeft = 'a';
    char moveRight = 'd';
    */

    public readonly char use = 'e';
    public readonly char attack = ' ';

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