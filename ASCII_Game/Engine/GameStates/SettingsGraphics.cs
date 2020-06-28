using System;
using System.Collections.Generic;
using System.Text;

namespace GameStates
{
    /// <summary>
    /// Menu -> Settings -> Graphics<br/>
    /// Allows user to change their graphics.
    /// </summary>
    class SettingsGraphics : GameState
    {
        int selection = 0;

        Image height = new Image.Rectangle(new Shader.RichText("Height", new Color8(0, 0, 255)), new Vector2d16(8, 1), 200);
        Image width = new Image.Rectangle(new Shader.RichText("Width", new Color8(255, 255, 255)), new Vector2d16(8, 1), 200);
        Image fps = new Image.Rectangle(new Shader.RichText("Framerate", new Color8(255, 255, 255)), new Vector2d16(9, 1), 200);

        Image heightValue = new Image.Rectangle(new Shader.RichText(Config.screenHeight.ToString(), new Color8(0, 0, 255)), new Vector2d16(4, 1), 200);
        Image widthValue = new Image.Rectangle(new Shader.RichText(Config.screenWidth.ToString(), new Color8(255, 255, 255)), new Vector2d16(4, 1), 200);
        Image fpsValue = new Image.Rectangle(new Shader.RichText(Config.framerate.ToString(), new Color8(255, 255, 255)), new Vector2d16(4, 1), 200);

        Image save = new Image.Rectangle(new Shader.RichText("Save", new Color8(255, 255, 255)), new Vector2d16(4, 1), 200);
        Image back = new Image.Rectangle(new Shader.RichText("Back", new Color8(255, 255, 255)), new Vector2d16(4, 1), 200);

        public SettingsGraphics()
        {
            Shader plainShader = new Shader.Plain(new Color8(0, 0, 0), new Color8(0, 0, 0), ' ');
            Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(600, 300), 0);

            Shader logoShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\mainMenu.bms"), new Vector2d16(0, 6), new Vector2d16(47, 11));
            Image logo = new Image.Rectangle(logoShader, new Vector2d16(51, 5), 1);

            Image graphics = new Image.Rectangle(new Shader.RichText("Graphics", new Color8(0, 0, 255)), new Vector2d16(8, 1), 200);
            Image controls = new Image.Rectangle(new Shader.RichText("Controls", new Color8(255, 255, 255)), new Vector2d16(8, 1), 200);
            Image backOld = new Image.Rectangle(new Shader.RichText("Back", new Color8(255, 255, 255)), new Vector2d16(4, 1), 200);

            Image oldDecoration = new Image.Rectangle(new Shader.RichText(new string[] { "|", "|", "|", "|", "|" }, new Color8(255, 255, 255)), new Vector2d16(1, 5), 200);
            Image oldCursor = new Image.Rectangle(new Shader.RichText(new string[] { "> |" }, new Color8(0, 0, 255)), new Vector2d16(3, 1), 200);

            Image decoration = new Image.Rectangle(new Shader.RichText(new string[] { "|", "|", "|", "|", "|", "•", "•", "•", "|", "|", "|" }, new Color8(255, 255, 255)), new Vector2d16(1, 11), 200);

            Shader cursorShader = new Shader.RichText("> |", new Color8(0, 0, 255));
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
                case EInput.enter:
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
                    ((Shader.RichText)width.shader).foreground = new Color8(255, 255, 255);
                    ((Shader.RichText)widthValue.shader).foreground = new Color8(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 8));
                    ((Shader.RichText)height.shader).foreground = new Color8(0, 0, 255);
                    ((Shader.RichText)heightValue.shader).foreground = new Color8(0, 0, 255);
                    break;
                case 1:
                    ((Shader.RichText)height.shader).foreground = new Color8(255, 255, 255);
                    ((Shader.RichText)fps.shader).foreground = new Color8(255, 255, 255);
                    ((Shader.RichText)heightValue.shader).foreground = new Color8(255, 255, 255);
                    ((Shader.RichText)fpsValue.shader).foreground = new Color8(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 10));
                    ((Shader.RichText)width.shader).foreground = new Color8(0, 0, 255);
                    ((Shader.RichText)widthValue.shader).foreground = new Color8(0, 0, 255);
                    break;
                case 2:
                    ((Shader.RichText)width.shader).foreground = new Color8(255, 255, 255);
                    ((Shader.RichText)widthValue.shader).foreground = new Color8(255, 255, 255);
                    ((Shader.RichText)save.shader).foreground = new Color8(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 12));
                    ((Shader.RichText)fps.shader).foreground = new Color8(0, 0, 255);
                    ((Shader.RichText)fpsValue.shader).foreground = new Color8(0, 0, 255);
                    break;
                case 3:
                    ((Shader.RichText)fps.shader).foreground = new Color8(255, 255, 255);
                    ((Shader.RichText)fpsValue.shader).foreground = new Color8(255, 255, 255);
                    ((Shader.RichText)back.shader).foreground = new Color8(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 16));
                    ((Shader.RichText)save.shader).foreground = new Color8(0, 0, 255);
                    break;
                case 4:
                    ((Shader.RichText)save.shader).foreground = new Color8(255, 255, 255);
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 18));
                    ((Shader.RichText)back.shader).foreground = new Color8(0, 0, 255);
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
