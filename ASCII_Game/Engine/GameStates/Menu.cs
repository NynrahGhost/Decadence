using System;
using System.Collections.Generic;
using System.Text;

namespace GameStates
{
    class Menu : GameState
    {
        int selection = 0;

        Image newGame = new Image.Rectangle(new Shader.RichText("Start new journey", new Color8fg(0, 0, 255)), new Vector2d16(17, 1), 200);
        Image loadGame = new Image.Rectangle(new Shader.RichText("Load game", new Color8fg(255, 255, 255)), new Vector2d16(9, 1), 200);
        Image test = new Image.Rectangle(new Shader.RichText("Run test", new Color8fg(255, 255, 255)), new Vector2d16(8, 1), 200);
        Image mapEditor = new Image.Rectangle(new Shader.RichText("Map editor", new Color8fg(255, 255, 255)), new Vector2d16(10, 1), 200);
        Image settings = new Image.Rectangle(new Shader.RichText("Settings", new Color8fg(255, 255, 255)), new Vector2d16(8, 1), 200);
        Image credits = new Image.Rectangle(new Shader.RichText("Credits", new Color8fg(255, 255, 255)), new Vector2d16(7, 1), 200);
        Image exit = new Image.Rectangle(new Shader.RichText("Exit", new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

        public Menu()
        {
            Shader plainShader = new Shader.Plain(new Color8fg(0, 0, 0), new Color8bg(0, 0, 0), ' ');
            Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(600, 300), 0);

            Shader logoShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\mainMenu.bms"), new Vector2d16(0, 0), new Vector2d16(51, 5));
            Image logo = new Image.Rectangle(logoShader, new Vector2d16(51, 5), 1);

            //Image mainMenuImage = new Image.Rectangle(mainMenu, new Vector2d16(30, 15), 126);

            Image team = new Image.Rectangle(new Shader.RichText("Made by Code0", new Color8fg(60, 60, 60)), new Vector2d16(14, 1), 200);

            Shader cursorShader = new Shader.Plain(new Color8fg(0, 0, 255), Color8bg.GetNull(), '>');
            Image cursor = new Image.Rectangle(cursorShader, new Vector2d16(1, 1), 127);

            Shape circle = new Shape.Circle(1);

            var tmp = new GameObject[]
            {
                new VisualObject(new Vector2d16(-100, -50), plainImage),

                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 25, Config.screenHeight / 5), logo),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 8 , Config.screenHeight / 5 + 8), newGame),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4 , Config.screenHeight / 5 + 10), loadGame),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4 , Config.screenHeight / 5 + 12), test),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 5 , Config.screenHeight / 5 + 14), mapEditor),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4 , Config.screenHeight / 5 + 16), settings),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 3 , Config.screenHeight / 5 + 18), credits),
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 2 , Config.screenHeight / 5 + 20), exit),
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
                    if (selection != 0)
                        --selection;
                    SetSelection();
                    break;
                case EInput.moveBackward:
                    if (selection != 6)
                        ++selection;
                    SetSelection();
                    break;
                case EInput.attack:
                case EInput.enter:
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
                    ((Shader.RichText)test.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 6, Config.screenHeight / 5 + 10));
                    ((Shader.RichText)loadGame.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 2:
                    ((Shader.RichText)loadGame.shader).foreground = new Color8fg(255, 255, 255);
                    ((Shader.RichText)mapEditor.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 12));
                    ((Shader.RichText)test.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 3:
                    ((Shader.RichText)test.shader).foreground = new Color8fg(255, 255, 255);
                    ((Shader.RichText)settings.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 14));
                    ((Shader.RichText)mapEditor.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 4:
                    ((Shader.RichText)mapEditor.shader).foreground = new Color8fg(255, 255, 255);
                    ((Shader.RichText)credits.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 6, Config.screenHeight / 5 + 16));
                    ((Shader.RichText)settings.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 5:
                    ((Shader.RichText)settings.shader).foreground = new Color8fg(255, 255, 255);
                    ((Shader.RichText)exit.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 5, Config.screenHeight / 5 + 18));
                    ((Shader.RichText)credits.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 6:
                    ((Shader.RichText)credits.shader).foreground = new Color8fg(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 4, Config.screenHeight / 5 + 20));
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
                    global::Game.gameState = new LoadGame();
                    break;
                case 2:
                    global::Game.gameState = new Test();
                    break;
                case 3:
                    global::Game.gameState = new Game();
                    break;
                case 4:
                    global::Game.gameState = new Settings();
                    break;
                case 5:
                    global::Game.gameState = new Credits();
                    break;
                case 6:
                    System.Environment.Exit(0);
                    break;
            }
        }
    }
}
