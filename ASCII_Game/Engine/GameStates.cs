abstract class GameState
{
    public abstract void Physics(EInput input);

    public Map map;

    public KinematicObject hero;

    public class Intro
    {

    }

    public class Menu : GameState
    {
        int selection = 0;

        Image newGame = new Image.Rectangle(new Shader.RichText("Start new journey", new Color8fg(0, 0, 255)), new Vector2d16(17, 1), 200);
        Image loadGame = new Image.Rectangle(new Shader.RichText("Load game", new Color8fg(255, 255, 255)), new Vector2d16(9, 1), 200);
        Image mapEditor = new Image.Rectangle(new Shader.RichText("Map editor", new Color8fg(255, 255, 255)), new Vector2d16(10, 1), 200);
        Image settings = new Image.Rectangle(new Shader.RichText("Settings", new Color8fg(255, 255, 255)), new Vector2d16(8, 1), 200);
        Image credits = new Image.Rectangle(new Shader.RichText("Credits", new Color8fg(255, 255, 255)), new Vector2d16(7, 1), 200);
        Image exit = new Image.Rectangle(new Shader.RichText("Exit", new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

        public Menu()
        {
            Shader plainShader = new Shader.Plain(new Color8fg(0, 0, 0), new Color8bg(0, 0, 0), ' ');
            Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(600, 300), 0);

            Shader logoShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\mainMenu.bms"), new Vector2d16(0,0), new Vector2d16(51, 5));
            Image logo = new Image.Rectangle(logoShader, new Vector2d16(51, 5), 1);

            //Image mainMenuImage = new Image.Rectangle(mainMenu, new Vector2d16(30, 15), 126);

            Image team = new Image.Rectangle(new Shader.RichText("Made by Code_0", new Color8fg(60, 60, 60)), new Vector2d16(14, 1), 200);

            Shader cursorShader = new Shader.Plain(new Color8fg(0,0,255), Color8bg.GetNull(), '>');
            Image cursor = new Image.Rectangle(cursorShader, new Vector2d16(1, 1), 127);

            Shape circle = new Shape.Circle(1);

            var tmp = new GameObject[]
            {
                new VisualObject(new Vector2d16(-100, -50), plainImage),

                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 25, Config.screenHeight / 5), logo),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 8 , Config.screenHeight / 5 + 8), newGame),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4 , Config.screenHeight / 5 + 10), loadGame),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 5 , Config.screenHeight / 5 + 12), mapEditor),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4 , Config.screenHeight / 5 + 14), settings),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 3 , Config.screenHeight / 5 + 16), credits),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 2 , Config.screenHeight / 5 + 18), exit),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 7 , Config.screenHeight / 5 * 4), team)
            };
            map = new Map("Test", Renderer.Dimensions, 0, tmp);
            hero = new KinematicObject(new Vector2d16(Config.screenWidth / 2 - 10, Config.screenHeight / 5 + 8), circle, cursor);
        }

        public override void Physics(EInput input)
        {
            Renderer.SetObjects(map.GetVisuals());
            switch (input)
            {
                case EInput.moveForward:
                    if(selection != 0)
                        --selection;
                    SetSelection();
                    break;
                case EInput.moveBackward:
                    if (selection != 5)
                        ++selection;
                    SetSelection();
                    break;
                case EInput.attack:
                    Action();
                    break;
                case EInput.escape:
                    System.Environment.Exit(0);
                    break;
            }
        }

        public void SetSelection()
        {
            switch (selection)
            {
                case 0:
                    ((Shader.RichText)loadGame.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 10, Config.screenHeight / 5 + 8));
                    ((Shader.RichText)newGame.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 1:
                    ((Shader.RichText)newGame.shader).foreground = new Color8fg(255, 255, 255);
                    ((Shader.RichText)mapEditor.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 6, Config.screenHeight / 5 + 10));
                    ((Shader.RichText)loadGame.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 2:
                    ((Shader.RichText)loadGame.shader).foreground = new Color8fg(255, 255, 255);
                    ((Shader.RichText)settings.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 12));
                    ((Shader.RichText)mapEditor.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 3:
                    ((Shader.RichText)mapEditor.shader).foreground = new Color8fg(255, 255, 255);
                    ((Shader.RichText)credits.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 6, Config.screenHeight / 5 + 14));
                    ((Shader.RichText)settings.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 4:
                    ((Shader.RichText)settings.shader).foreground = new Color8fg(255, 255, 255);
                    ((Shader.RichText)exit.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 5, Config.screenHeight / 5 + 16));
                    ((Shader.RichText)credits.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 5:
                    ((Shader.RichText)credits.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 4, Config.screenHeight / 5 + 18));
                    ((Shader.RichText)exit.shader).foreground = new Color8fg(0, 0, 255);
                    break;
            }
        }

        public void Action()
        {
            Renderer.worldPosition = new Vector2d16(0, 0);
            switch (selection)
            {
                case 0:
                    global::Game.gameState = new Game();
                    break;
                case 1:
                    global::Game.gameState = new Game();
                    break;
                case 2:
                    global::Game.gameState = new Game();
                    break;
                case 3:
                    global::Game.gameState = new Settings();
                    break;
                case 4:
                    global::Game.gameState = new Credits();
                    break;
                case 5:
                    System.Environment.Exit(0);
                    break;
            }
        }
    }
    
    public class Settings : GameState
    {
        int selection = 0;

        Image graphics = new Image.Rectangle(new Shader.RichText("Graphics", new Color8fg(0, 0, 255)), new Vector2d16(8, 1), 200);
        Image controls = new Image.Rectangle(new Shader.RichText("Controls", new Color8fg(255, 255, 255)), new Vector2d16(8, 1), 200);
        Image back = new Image.Rectangle(new Shader.RichText("Back", new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

        public Settings()
        {
            Shader plainShader = new Shader.Plain(new Color8fg(0, 0, 0), new Color8bg(0, 0, 0), ' ');
            Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(600, 300), 0);

            Shader logoShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\mainMenu.bms"), new Vector2d16(0, 6), new Vector2d16(47, 11));
            Image logo = new Image.Rectangle(logoShader, new Vector2d16(51, 5), 1);

            Image decoration = new Image.Rectangle(new Shader.RichText(new string[] { "|", "|", "|", "|", "|" }, new Color8fg(255, 255, 255)), new Vector2d16(1, 5), 200);

            Shader cursorShader = new Shader.RichText("> |", new Color8fg(0, 0, 255));
            Image cursor = new Image.Rectangle(cursorShader, new Vector2d16(3, 1), 127);

            Shape circle = new Shape.Circle(1);

            var tmp = new GameObject[]
            {
                new VisualObject(new Vector2d16(-100, -50), plainImage),

                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 25, Config.screenHeight / 5), logo),
                 new VisualObject(new Vector2d16(Config.screenWidth / 2 - 25, Config.screenHeight / 5 + 8), decoration),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 24 , Config.screenHeight / 5 + 8), graphics),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 24 , Config.screenHeight / 5 + 10), controls),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 24 , Config.screenHeight / 5 + 12), back)
            };
            map = new Map("Test", Renderer.Dimensions, 0, tmp);
            hero = new KinematicObject(new Vector2d16(Config.screenWidth / 2 - 27, Config.screenHeight / 5 + 8), circle, cursor);
        }

        public override void Physics(EInput input)
        {
            Renderer.SetObjects(map.GetVisuals());
            switch (input)
            {
                case EInput.moveForward:
                    if (selection != 0)
                        --selection;
                    SetSelection();
                    break;
                case EInput.moveBackward:
                    if (selection != 2)
                        ++selection;
                    SetSelection();
                    break;
                case EInput.attack:
                    Action();
                    break;
                case EInput.escape:
                    global::Game.gameState = new Menu();
                    break;
            }
        }

        public void SetSelection()
        {
            switch (selection)
            {
                case 0:
                    ((Shader.RichText)controls.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 27, Config.screenHeight / 5 + 8));
                    ((Shader.RichText)graphics.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 1:
                    ((Shader.RichText)graphics.shader).foreground = new Color8fg(255, 255, 255);
                    ((Shader.RichText)back.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 27, Config.screenHeight / 5 + 10));
                    ((Shader.RichText)controls.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 2:
                    ((Shader.RichText)controls.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 27, Config.screenHeight / 5 + 12));
                    ((Shader.RichText)back.shader).foreground = new Color8fg(0, 0, 255);
                    break;
            }
        }

        public void Action()
        {
            Renderer.worldPosition = new Vector2d16(0, 0);
            switch (selection)
            {
                case 0:
                    global::Game.gameState = new SettingsGraphics();
                    break;
                case 1:
                    global::Game.gameState = new Game();
                    break;
                case 2:
                    global::Game.gameState = new Menu();
                    break;
            }
        }


        public class SettingsGraphics : GameState
        {
            int selection = 0;

            Image height = new Image.Rectangle(new Shader.RichText("Height", new Color8fg(0, 0, 255)), new Vector2d16(8, 1), 200);
            Image width = new Image.Rectangle(new Shader.RichText("Width", new Color8fg(255, 255, 255)), new Vector2d16(8, 1), 200);
            Image fps = new Image.Rectangle(new Shader.RichText("Framerate", new Color8fg(255, 255, 255)), new Vector2d16(9, 1), 200);

            Image heightValue = new Image.Rectangle(new Shader.RichText(Config.screenHeight.ToString(), new Color8fg(0, 0, 255)), new Vector2d16(4, 1), 200);
            Image widthValue = new Image.Rectangle(new Shader.RichText(Config.screenWidth.ToString(), new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);
            Image fpsValue = new Image.Rectangle(new Shader.RichText(Config.framerate.ToString(), new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

            Image save = new Image.Rectangle(new Shader.RichText("Save", new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);
            Image back = new Image.Rectangle(new Shader.RichText("Back", new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

            public SettingsGraphics()
            {
                Shader plainShader = new Shader.Plain(new Color8fg(0, 0, 0), new Color8bg(0, 0, 0), ' ');
                Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(600, 300), 0);

                Shader logoShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\mainMenu.bms"), new Vector2d16(0, 6), new Vector2d16(47, 11));
                Image logo = new Image.Rectangle(logoShader, new Vector2d16(51, 5), 1);

                Image graphics = new Image.Rectangle(new Shader.RichText("Graphics", new Color8fg(0, 0, 255)), new Vector2d16(8, 1), 200);
                Image controls = new Image.Rectangle(new Shader.RichText("Controls", new Color8fg(255, 255, 255)), new Vector2d16(8, 1), 200);
                Image backOld = new Image.Rectangle(new Shader.RichText("Back", new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

                Image oldDecoration = new Image.Rectangle(new Shader.RichText(new string[] { "|", "|", "|", "|", "|" }, new Color8fg(255, 255, 255)), new Vector2d16(1, 5), 200);
                Image oldCursor = new Image.Rectangle(new Shader.RichText(new string[] { "> |" }, new Color8fg(0, 0, 255)), new Vector2d16(3, 1), 200);

                Image decoration = new Image.Rectangle(new Shader.RichText(new string[] { "|", "|", "|", "|", "|", "•", "•", "•", "|", "|", "|" }, new Color8fg(255, 255, 255)), new Vector2d16(1, 11), 200);

                Shader cursorShader = new Shader.RichText("> |", new Color8fg(0, 0, 255));
                Image cursor = new Image.Rectangle(cursorShader, new Vector2d16(3, 1), 127);
                Image cursorOld = new Image.Rectangle(cursorShader, new Vector2d16(3, 1), 201);

                Shape circle = new Shape.Circle(1);

                var tmp = new GameObject[]
                {
                    new VisualObject(new Vector2d16(-100, -50), plainImage),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 25, Config.screenHeight / 5), logo),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 25, Config.screenHeight / 5 + 8), oldDecoration),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 24 , Config.screenHeight / 5 + 8), graphics),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 24 , Config.screenHeight / 5 + 10), controls),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 24 , Config.screenHeight / 5 + 12), backOld),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 27 , Config.screenHeight / 5 + 8), cursorOld),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 5, Config.screenHeight / 5 + 8), decoration),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4, Config.screenHeight / 5 + 8), height),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4, Config.screenHeight / 5 + 10), width),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4 , Config.screenHeight / 5 + 12), fps),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 7, Config.screenHeight / 5 + 8), heightValue),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 7, Config.screenHeight / 5 + 10), widthValue),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 7 , Config.screenHeight / 5 + 12), fpsValue),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4 , Config.screenHeight / 5 + 16), save),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4 , Config.screenHeight / 5 + 18), back)
                };
                map = new Map("Test", Renderer.Dimensions, 0, tmp);
                hero = new KinematicObject(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 8), circle, cursor);
            }

            public override void Physics(EInput input)
            {
                Renderer.SetObjects(map.GetVisuals());
                switch (input)
                {
                    case EInput.moveForward:
                        if (selection != 0)
                            --selection;
                        SetSelection();
                        break;
                    case EInput.moveBackward:
                        if (selection != 4)
                            ++selection;
                        SetSelection();
                        break;
                    case EInput.moveRight:
                        IncrementSelection(true);
                        break;
                    case EInput.moveLeft:
                        IncrementSelection(false);
                        break;
                    case EInput.attack:
                        Action();
                        break;
                    case EInput.escape:
                        global::Game.gameState = new Settings();
                        break;
                }
            }

            private void IncrementSelection(bool sign)
            {
                int value;
                switch (selection)
                {
                    case 0:
                        value = int.Parse(((Shader.RichText)heightValue.shader).GetText());
                        ((Shader.RichText)heightValue.shader).SetText((value + (sign ? 4 : -4)).ToString());
                        break;
                    case 1:
                        value = int.Parse(((Shader.RichText)widthValue.shader).GetText());
                        ((Shader.RichText)widthValue.shader).SetText((value + (sign ? 4 : -4)).ToString());
                        break;
                    case 2:
                        value = int.Parse(((Shader.RichText)fpsValue.shader).GetText());
                        ((Shader.RichText)fpsValue.shader).SetText((value + (sign ? 1 : -1)).ToString());
                        break;
                }
            }

            public void SetSelection()
            {
                switch (selection)
                {
                    case 0:
                        ((Shader.RichText)width.shader).foreground = new Color8fg(255, 255, 255);
                        ((Shader.RichText)widthValue.shader).foreground = new Color8fg(255, 255, 255);
                        hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 8));
                        ((Shader.RichText)height.shader).foreground = new Color8fg(0, 0, 255);
                        ((Shader.RichText)heightValue.shader).foreground = new Color8fg(0, 0, 255);
                        break;
                    case 1:
                        ((Shader.RichText)height.shader).foreground = new Color8fg(255, 255, 255);
                        ((Shader.RichText)fps.shader).foreground = new Color8fg(255, 255, 255);
                        ((Shader.RichText)heightValue.shader).foreground = new Color8fg(255, 255, 255);
                        ((Shader.RichText)fpsValue.shader).foreground = new Color8fg(255, 255, 255);
                        hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 10));
                        ((Shader.RichText)width.shader).foreground = new Color8fg(0, 0, 255);
                        ((Shader.RichText)widthValue.shader).foreground = new Color8fg(0, 0, 255);
                        break;
                    case 2:
                        ((Shader.RichText)width.shader).foreground = new Color8fg(255, 255, 255);
                        ((Shader.RichText)widthValue.shader).foreground = new Color8fg(255, 255, 255);
                        ((Shader.RichText)save.shader).foreground = new Color8fg(255, 255, 255);
                        hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 12));
                        ((Shader.RichText)fps.shader).foreground = new Color8fg(0, 0, 255);
                        ((Shader.RichText)fpsValue.shader).foreground = new Color8fg(0, 0, 255);
                        break;
                    case 3:
                        ((Shader.RichText)fps.shader).foreground = new Color8fg(255, 255, 255);
                        ((Shader.RichText)fpsValue.shader).foreground = new Color8fg(255, 255, 255);
                        ((Shader.RichText)back.shader).foreground = new Color8fg(255, 255, 255);
                        hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 16));
                        ((Shader.RichText)save.shader).foreground = new Color8fg(0, 0, 255);
                        break;
                    case 4:
                        ((Shader.RichText)save.shader).foreground = new Color8fg(255, 255, 255);
                        hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 18));
                        ((Shader.RichText)back.shader).foreground = new Color8fg(0, 0, 255);
                        break;
                }
            }

            public void Action()
            {
                Renderer.worldPosition = new Vector2d16(0, 0);
                switch (selection)
                {
                    case 3:
                        Config.controlls[0] = int.Parse(((Shader.RichText)widthValue.shader).GetText());
                        Config.controlls[1] = int.Parse(((Shader.RichText)heightValue.shader).GetText());
                        Config.controlls[2] = int.Parse(((Shader.RichText)fpsValue.shader).GetText());

                        Renderer.Reload();

                        System.Console.SetWindowSize(Config.screenWidth, Config.screenHeight);

                        break;
                    case 4:
                        global::Game.gameState = new Settings();
                        break;
                }
            }
        }
    }

    public class Credits : GameState
    {
        Image credits = new Image.Rectangle(new Shader.RichText(new string[]{
            "              Screenwriter    Evhenyi Voitsekhovskyi", "",
            "Lead designer & programmer    Mykhailo Kutsenko", "",
            "     Designer & programmer    Nataliia Smalchenko", "",
            "                Programmer    Ivan Vasyliv"
        }, new Color8fg(255, 255, 255)), new Vector2d16(70, 7), 200);

        public Credits()
        {
            Shader plainShader = new Shader.Plain(new Color8fg(0, 0, 0), new Color8bg(0, 0, 0), ' ');
            Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(600, 300), 0);

            Shader logoShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\mainMenu.bms"), new Vector2d16(0, 12), new Vector2d16(53, 16));
            Image logo = new Image.Rectangle(logoShader, new Vector2d16(54, 4), 1);


            Shader cursorShader = new Shader.Plain(new Color8fg(0, 0, 255), Color8bg.GetNull(), ' ');
            Image cursor = new Image.Rectangle(cursorShader, new Vector2d16(1, 1), 127);

            Shape circle = new Shape.Circle(1);

            var tmp = new GameObject[]
            {
                new VisualObject(new Vector2d16(-100, -50), plainImage),

                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 25, Config.screenHeight / 5), logo),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 28 , Config.screenHeight / 2.5), credits)
            };
            map = new Map("Test", Renderer.Dimensions, 0, tmp);
            hero = new KinematicObject(new Vector2d16(0, 0), circle, cursor);
        }

        public override void Physics(EInput input)
        {
            Renderer.SetObjects(map.GetVisuals());
            if(input != EInput.none)
                global::Game.gameState = new Menu();
        }
    }
    
    public class Game : GameState
    {
        public Game()
        {
            Shader plainShader = new Shader.Plain(new Color8fg(0, 0, 0), new Color8bg(0, 0, 0), ' ');
            Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(600, 300), 0);
            Shader gradientShader = new Shader.Gradient(new Color8fg(255, 0, 0), new Color8fg(255, 255, 255), new Color8bg(255, 0, 0), new Color8bg(0, 0, 255), ' ');
            Image gradientImage = new Image.Rectangle(gradientShader, new Vector2d16(20, 10));
            Shader textureShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\characters.bms"), new Vector2d32(0, 2), new Vector2d32(7, 7));
            Image textureImage = new Image.Rectangle(textureShader, new Vector2d16(7, 6), 127);

            Shader earthShader = new Shader.TextureBackground(ResourceLoader.LoadResource<AtlasPNG>(@"Textures\earth.png"), new Vector2d32(0, 0), new Vector2d32(15, 15));
            Image earthImage = new Image.Rectangle(earthShader, new Vector2d16(30, 15), 126);

            Shape circle = new Shape.Circle(3);
            var tmp = new GameObject[]
            {
                new VisualObject(new Vector2d16(-100, -50), plainImage),
                new VisualObject(new Vector2d16(10, 0), earthImage),
                //new PhysicalObject(new Vector2d16(30,5), circle, gradientImage),
                new PhysicalObject(new Vector2d16(50,10), circle, gradientImage)
            };
            map = new Map("Test", new Vector2d16(2000,1000), 1, tmp);
            hero = new KinematicObject(new Vector2d16(40, 15), circle, textureImage);
        }

        public override void Physics(EInput input)
        {
            //if (input == EInput.none)
            Renderer.SetObjects(map.GetVisuals());
            switch (input)
            {
                case EInput.moveForward:
                    if(hero.Move(hero.position + new Vector2d16(0, -1)))
                        Renderer.worldPosition._2 -= 1;
                    break;
                case EInput.moveBackward:
                    if (hero.Move(hero.position + new Vector2d16(0, 1)))
                        Renderer.worldPosition._2 += 1;
                    break;
                case EInput.moveLeft:
                    if (hero.Move(hero.position + new Vector2d16(-2, 0)))
                        Renderer.worldPosition._1 -= 2;
                    break;
                case EInput.moveRight:
                    if (hero.Move(hero.position + new Vector2d16(2, 0)))
                        Renderer.worldPosition._1 += 2;
                    break;
                case EInput.use:
                    System.Console.Beep();
                    break;
                case EInput.attack:
                    System.Console.Beep();
                    System.Console.Beep();
                    System.Console.Beep();
                    break;
                case EInput.escape:
                    global::Game.gameState = new Menu();
                    Renderer.worldPosition = new Vector2d16(0, 0);
                    break;
            }
        }
    }

    public class Editor
    {

    }
}