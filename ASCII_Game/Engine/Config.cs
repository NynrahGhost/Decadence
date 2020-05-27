static class Config
{
    public static string profileName = "user1";
    private static object[] controlls = new object[]
    {
        100, 50, 15,
        'w', 's', 'a', 'd',
        'o', 'l', 'k', ';',
        'e', ' ', 'q', 'i', 'p'
    }; 

    public static int screenWidth => (int)controlls[0];
    public static int screenHeight => (int)controlls[1];

    public static int framerate => (int)controlls[2];

    public static char moveForward => (char)controlls[3];
    public static char moveBackward => (char)controlls[4];
    public static char moveLeft => (char)controlls[5];
    public static char moveRight => (char)controlls[6];


    public static char moveAltForward => (char)controlls[7];
    public static char moveAltBackward => (char)controlls[8];
    public static char moveAltLeft => (char)controlls[9];
    public static char moveAltRight => (char)controlls[10];


    public static char use => (char)controlls[11];
    public static char attack => (char)controlls[12];
    public static char altAttack => (char)controlls[13];
    public static char inventory => (char)controlls[14];
    public static char map => (char)controlls[15];

    public static char escape = (char)System.ConsoleKey.Escape;

    static Config()
    {
        RestoreDefaults();
    }

    public static void RestoreDefaults()
    {
        controlls = new object[]
        {
            100, 50, 15,
            'w', 's', 'a', 'd',
            'o', 'l', 'k', ';',
            'e', ' ', 'q', 'i', 'p'
        };
    }

    public static void Load(string path) 
    {
      //Dictionary<string,string>
        var dict = INI.Read(path);

        int counter = -1;
               //KeyValuePair<string, string>
        foreach (var entry in dict)
        {
            int intVal;
            if (int.TryParse(entry.Value, out intVal))
                controlls[++counter] = intVal;
            else if (entry.Value.Length > 1)
                if (entry.Value.Equals("space"))
                    controlls[++counter] = ' ';
                else
                    controlls[++counter] = entry.Value;
            else
                controlls[++counter] = entry.Value[0];
        }

        /*
        string value;
        if(dict.TryGetValue("Height", out value))
            controlls[0] = int.Parse(value);

        if (dict.TryGetValue("Width", out value))
            controlls[1] = int.Parse(value);

        if (dict.TryGetValue("Framerate", out value))
            controlls[2] = int.Parse(value);

        if (dict.TryGetValue("MoveUp", out value))
            controlls[0] = value[0];

        if (dict.TryGetValue("MoveDown", out value))
            moveBackward = value[0];

        if (dict.TryGetValue("MoveLeft", out value))
            moveLeft = value[0];

        if (dict.TryGetValue("MoveRight", out value))
            moveRight = value[0];

        if (dict.TryGetValue("MoveRight", out value))
            moveRight = value[0];

        if (dict.TryGetValue("Use", out value))
            use = value[0];

        if (dict.TryGetValue("Attack", out value))
            attack = value[0];
        */
    }

    public static void Save(string path)
    {
        var dict = new System.Collections.Generic.Dictionary<string, string>();

        dict.Add("[1", '[' + profileName + ']');
        dict.Add("[2", "[Video]");
        dict.Add("#1", "#========================================");
        dict.Add("Height", screenHeight.ToString());
        dict.Add("Widtht", screenWidth.ToString());
        dict.Add("[3", "[KeyMap]");
        dict.Add("#2", "#========================================");
        dict.Add("MoveUp", moveForward.ToString());
        dict.Add("MoveDown", moveBackward.ToString());
        dict.Add("MoveLeft", moveLeft.ToString());
        dict.Add("MoveRight", moveRight.ToString());
        dict.Add("#3", "#========================================");
        dict.Add("MoveAltUp", moveAltForward.ToString());
        dict.Add("MoveAltDown", moveAltBackward.ToString());
        dict.Add("MoveAltLeft", moveAltLeft.ToString());
        dict.Add("MoveAltRight", moveAltRight.ToString());
        dict.Add("#4", "#========================================");
        dict.Add("Use", use.ToString());
        dict.Add("Attack", attack.ToString());
        dict.Add("AltAttack", altAttack.ToString());
        dict.Add("Inventory", inventory.ToString());
        dict.Add("Map", map.ToString());

        INI.Write(path, dict);
    }
}