using System;
using System.Collections.Generic;
using System.Text;

namespace GameStates
{
    /// <summary>
    /// Menu -> Settings<br/>
    /// Allows user to change their preferances.
    /// </summary>
    class Settings : GameState
    {
        int selection = 0;

        Image graphics = new Image.Rectangle(new Shader.RichText("Graphics", new Color8(0, 0, 255)), new Vector2d16(8, 1), 200);
        Image controls = new Image.Rectangle(new Shader.RichText("Controls", new Color8(255, 255, 255)), new Vector2d16(8, 1), 200);
        Image back = new Image.Rectangle(new Shader.RichText("Back", new Color8(255, 255, 255)), new Vector2d16(4, 1), 200);

        public Settings()
        {
            Shader plainShader = new Shader.Plain(new Color8(0, 0, 0), new Color8(0, 0, 0), ' ');
            Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(600, 300), 0);

            Shader logoShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\mainMenu.bms"), new Vector2d16(0, 6), new Vector2d16(47, 11));
            Image logo = new Image.Rectangle(logoShader, new Vector2d16(51, 5), 1);

            Image decoration = new Image.Rectangle(new Shader.RichText(new string[] { "|", "|", "|", "|", "|" }, new Color8(255, 255, 255)), new Vector2d16(1, 5), 200);

            Shader cursorShader = new Shader.RichText("> |", new Color8(0, 0, 255));
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
                case EInput.enter:
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
                    ((Shader.RichText)controls.shader).foreground = new Color8(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 27, Config.screenHeight / 5 + 8));
                    ((Shader.RichText)graphics.shader).foreground = new Color8(0, 0, 255);
                    break;
                case 1:
                    ((Shader.RichText)graphics.shader).foreground = new Color8(255, 255, 255);
                    ((Shader.RichText)back.shader).foreground = new Color8(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 27, Config.screenHeight / 5 + 10));
                    ((Shader.RichText)controls.shader).foreground = new Color8(0, 0, 255);
                    break;
                case 2:
                    ((Shader.RichText)controls.shader).foreground = new Color8(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 27, Config.screenHeight / 5 + 12));
                    ((Shader.RichText)back.shader).foreground = new Color8(0, 0, 255);
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
                    global::Game.gameState = new SettingsControlls();
                    break;
                case 2:
                    global::Game.gameState = new Menu();
                    break;
            }
        }

    }
}
