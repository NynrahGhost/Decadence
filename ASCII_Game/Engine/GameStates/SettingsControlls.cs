using System;
using System.Collections.Generic;
using System.Text;

namespace GameStates
{
    /// <summary>
    /// Menu -> Settings -> Controlls<br/>
    /// Allows user to change their controlls.
    /// </summary>
    class SettingsControlls : GameState
    {
        int selectionY = 0;
        int selectionX = 0;

        Image moveUp = new Image.Rectangle(new Shader.RichText("Move up", new Color8fg(0, 0, 255)), new Vector2d16(7, 1), 200);
        Image moveUpValue = new Image.Rectangle(new Shader.RichText(Config.moveForward.ToString(), new Color8fg(0, 0, 255)), new Vector2d16(4, 1), 200);

        Image moveDown = new Image.Rectangle(new Shader.RichText("Move down", new Color8fg(255, 255, 255)), new Vector2d16(9, 1), 200);
        Image moveDownValue = new Image.Rectangle(new Shader.RichText(Config.moveBackward.ToString(), new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

        Image moveLeft = new Image.Rectangle(new Shader.RichText("Move left", new Color8fg(255, 255, 255)), new Vector2d16(9, 1), 200);
        Image moveLeftValue = new Image.Rectangle(new Shader.RichText(Config.moveLeft.ToString(), new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

        Image moveRight = new Image.Rectangle(new Shader.RichText("Move right", new Color8fg(255, 255, 255)), new Vector2d16(10, 1), 200);
        Image moveRightValue = new Image.Rectangle(new Shader.RichText(Config.moveRight.ToString(), new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

        Image moveAltUp = new Image.Rectangle(new Shader.RichText("Move alt up", new Color8fg(255, 255, 255)), new Vector2d16(11, 1), 200);
        Image moveAltUpValue = new Image.Rectangle(new Shader.RichText(Config.moveAltForward.ToString(), new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

        Image moveAltDown = new Image.Rectangle(new Shader.RichText("Move alt down", new Color8fg(255, 255, 255)), new Vector2d16(14, 1), 200);
        Image moveAltDownValue = new Image.Rectangle(new Shader.RichText(Config.moveAltBackward.ToString(), new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

        Image moveAltLeft = new Image.Rectangle(new Shader.RichText("Move alt left", new Color8fg(255, 255, 255)), new Vector2d16(14, 1), 200);
        Image moveAltLeftValue = new Image.Rectangle(new Shader.RichText(Config.moveAltLeft.ToString(), new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

        Image moveAltRight = new Image.Rectangle(new Shader.RichText("Move alt right", new Color8fg(255, 255, 255)), new Vector2d16(14, 1), 200);
        Image moveAltRightValue = new Image.Rectangle(new Shader.RichText(Config.moveAltRight.ToString(), new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);



        Image save = new Image.Rectangle(new Shader.RichText("Save", new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);
        Image back = new Image.Rectangle(new Shader.RichText("Back", new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

        public SettingsControlls()
        {
            Shader plainShader = new Shader.Plain(new Color8fg(0, 0, 0), new Color8bg(0, 0, 0), ' ');
            Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(600, 300), 0);

            Shader logoShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\mainMenu.bms"), new Vector2d16(0, 6), new Vector2d16(47, 11));
            Image logo = new Image.Rectangle(logoShader, new Vector2d16(51, 5), 1);

            Image graphics = new Image.Rectangle(new Shader.RichText("Graphics", new Color8fg(255, 255, 255)), new Vector2d16(8, 1), 200);
            Image controls = new Image.Rectangle(new Shader.RichText("Controls", new Color8fg(0, 0, 255)), new Vector2d16(8, 1), 200);
            Image backOld = new Image.Rectangle(new Shader.RichText("Back", new Color8fg(255, 255, 255)), new Vector2d16(4, 1), 200);

            Image oldDecoration = new Image.Rectangle(new Shader.RichText(new string[] { "|", "|", "|", "|", "|" }, new Color8fg(255, 255, 255)), new Vector2d16(1, 5), 200);
            Image oldCursor = new Image.Rectangle(new Shader.RichText(new string[] { "> |" }, new Color8fg(0, 0, 255)), new Vector2d16(3, 1), 200);

            Image decoration = new Image.Rectangle(new Shader.RichText(new string[] { "|", "|", "|", "|", "|", "|", "|", "•", "•", "•", "|", "|", "|" }, new Color8fg(255, 255, 255)), new Vector2d16(1, 14), 200);
            Image decoration2 = new Image.Rectangle(new Shader.RichText(new string[] { "|", "|", "|", "|", "|", "|", "|" }, new Color8fg(255, 255, 255)), new Vector2d16(1, 7), 200);

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
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 27 , Config.screenHeight / 5 + 10), cursorOld),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 5, Config.screenHeight / 5 + 8), decoration),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 13, Config.screenHeight / 5 + 8), decoration2),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4, Config.screenHeight / 5 + 8), moveUp),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 7, Config.screenHeight / 5 + 8), moveUpValue),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4, Config.screenHeight / 5 + 10), moveDown),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 7, Config.screenHeight / 5 + 10), moveDownValue),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4, Config.screenHeight / 5 + 12), moveLeft),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 7, Config.screenHeight / 5 + 12), moveLeftValue),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4, Config.screenHeight / 5 + 14), moveRight),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 7, Config.screenHeight / 5 + 14), moveRightValue),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 14, Config.screenHeight / 5 + 8), moveAltUp),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 35, Config.screenHeight / 5 + 8), moveAltUpValue),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 14, Config.screenHeight / 5 + 10), moveAltDown),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 35, Config.screenHeight / 5 + 10), moveAltDownValue),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 14, Config.screenHeight / 5 + 12), moveAltLeft),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 35, Config.screenHeight / 5 + 12), moveAltLeftValue),

                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 14, Config.screenHeight / 5 + 14), moveAltRight),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 + 35, Config.screenHeight / 5 + 14), moveAltRightValue),


                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4 , Config.screenHeight / 5 + 18), save),
                    new VisualObject(new Vector2d16(Config.screenWidth / 2 - 4 , Config.screenHeight / 5 + 20), back)
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
                    if (selectionY != 0)
                        --selectionY;
                    SetSelection();
                    break;
                case EInput.moveBackward:
                    if (selectionY != 5)
                        ++selectionY;
                    SetSelection();
                    break;
                case EInput.moveLeft:
                    if (selectionX != 0)
                        --selectionX;
                    SetSelection();
                    break;
                case EInput.moveRight:
                    if (selectionX != 1)
                        ++selectionX;
                    SetSelection();
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

        public void SetSelection()
        {
            switch (selectionY)
            {
                case 0:
                    switch (selectionX)
                    {
                        case 0:
                            ClearSelection();
                            hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 8));
                            ((Shader.RichText)moveUp.shader).foreground = new Color8fg(0, 0, 255);
                            ((Shader.RichText)moveUpValue.shader).foreground = new Color8fg(0, 0, 255);
                            break;
                        case 1:
                            ClearSelection();
                            hero.Move(new Vector2d16(Config.screenWidth / 2 + 11, Config.screenHeight / 5 + 8));
                            ((Shader.RichText)moveAltUp.shader).foreground = new Color8fg(0, 0, 255);
                            ((Shader.RichText)moveAltUpValue.shader).foreground = new Color8fg(0, 0, 255);
                            break;
                    }
                    break;
                case 1:
                    switch (selectionX)
                    {
                        case 0:
                            ClearSelection();
                            hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 10));
                            ((Shader.RichText)moveDown.shader).foreground = new Color8fg(0, 0, 255);
                            ((Shader.RichText)moveDownValue.shader).foreground = new Color8fg(0, 0, 255);
                            break;
                        case 1:
                            ClearSelection();
                            hero.Move(new Vector2d16(Config.screenWidth / 2 + 11, Config.screenHeight / 5 + 10));
                            ((Shader.RichText)moveAltDown.shader).foreground = new Color8fg(0, 0, 255);
                            ((Shader.RichText)moveAltDownValue.shader).foreground = new Color8fg(0, 0, 255);
                            break;
                    }
                    break;
                case 2:
                    switch (selectionX)
                    {
                        case 0:
                            ClearSelection();
                            hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 12));
                            ((Shader.RichText)moveLeft.shader).foreground = new Color8fg(0, 0, 255);
                            ((Shader.RichText)moveLeftValue.shader).foreground = new Color8fg(0, 0, 255);
                            break;
                        case 1:
                            ClearSelection();
                            hero.Move(new Vector2d16(Config.screenWidth / 2 + 11, Config.screenHeight / 5 + 12));
                            ((Shader.RichText)moveAltLeft.shader).foreground = new Color8fg(0, 0, 255);
                            ((Shader.RichText)moveAltLeftValue.shader).foreground = new Color8fg(0, 0, 255);
                            break;
                    }
                    break;
                case 3:
                    switch (selectionX)
                    {
                        case 0:
                            ClearSelection();
                            hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 14));
                            ((Shader.RichText)moveRight.shader).foreground = new Color8fg(0, 0, 255);
                            ((Shader.RichText)moveRightValue.shader).foreground = new Color8fg(0, 0, 255);
                            break;
                        case 1:
                            ClearSelection();
                            hero.Move(new Vector2d16(Config.screenWidth / 2 + 11, Config.screenHeight / 5 + 14));
                            ((Shader.RichText)moveAltRight.shader).foreground = new Color8fg(0, 0, 255);
                            ((Shader.RichText)moveAltRightValue.shader).foreground = new Color8fg(0, 0, 255);
                            break;
                    }
                    break;
                case 4:
                    ClearSelection();
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 18));
                    ((Shader.RichText)save.shader).foreground = new Color8fg(0, 0, 255);
                    break;
                case 5:
                    ClearSelection();
                    hero.Move(new Vector2d16(Config.screenWidth / 2 - 7, Config.screenHeight / 5 + 20));
                    ((Shader.RichText)back.shader).foreground = new Color8fg(0, 0, 255);
                    break;
            }
        }

        private void ClearSelection()
        {
            ((Shader.RichText)moveUp.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveUpValue.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveDown.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveDownValue.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveLeft.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveLeftValue.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveRight.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveRightValue.shader).foreground = new Color8fg(255, 255, 255);

            ((Shader.RichText)moveAltUp.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveAltUpValue.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveAltDown.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveAltDownValue.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveAltLeft.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveAltLeftValue.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveAltRight.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)moveAltRightValue.shader).foreground = new Color8fg(255, 255, 255);

            ((Shader.RichText)save.shader).foreground = new Color8fg(255, 255, 255);
            ((Shader.RichText)back.shader).foreground = new Color8fg(255, 255, 255);
        }

        public void Action()
        {
            Renderer.worldPosition = new Vector2d16(0, 0);
            switch (selectionY)
            {
                case 0:
                    if (selectionX == 1)
                        Config.controlls[4] = ((Shader.RichText)moveAltUpValue.shader).GetText();
                    else
                        Config.controlls[4] = ((Shader.RichText)moveUpValue.shader).GetText();
                    break;
                case 1:
                    if (selectionX == 1)
                        Config.controlls[5] = ((Shader.RichText)moveAltDownValue.shader).GetText();
                    else
                        Config.controlls[5] = ((Shader.RichText)moveDownValue.shader).GetText();
                    break;
                case 2:
                    if (selectionX == 1)
                        Config.controlls[6] = ((Shader.RichText)moveAltLeftValue.shader).GetText();
                    else
                        Config.controlls[6] = ((Shader.RichText)moveLeftValue.shader).GetText();
                    break;
                case 3:
                    if (selectionX == 1)
                        Config.controlls[7] = ((Shader.RichText)moveAltRightValue.shader).GetText();
                    else
                        Config.controlls[7] = ((Shader.RichText)moveRightValue.shader).GetText();
                    break;
                case 4:
                    Config.controlls[3] = ((Shader.RichText)moveUpValue.shader).GetText();
                    Config.controlls[4] = ((Shader.RichText)moveDownValue.shader).GetText();
                    Config.controlls[5] = ((Shader.RichText)moveLeftValue.shader).GetText();
                    Config.controlls[6] = ((Shader.RichText)moveRightValue.shader).GetText();
                    Config.controlls[7] = ((Shader.RichText)moveUpValue.shader).GetText();
                    Config.controlls[8] = ((Shader.RichText)moveDownValue.shader).GetText();
                    Config.controlls[9] = ((Shader.RichText)moveLeftValue.shader).GetText();
                    Config.controlls[10] = ((Shader.RichText)moveRightValue.shader).GetText();


                    Renderer.Reload();

                    System.Console.SetWindowSize(Config.screenWidth, Config.screenHeight);

                    break;
                case 5:
                    global::Game.gameState = new Settings();
                    break;
            }
        }

        private char GetKey()
        {
            /*if (!System.Console.KeyAvailable)
            {
                input = EInput.none;
                return;
            }*/
            System.ConsoleKeyInfo keyInfo = System.Console.ReadKey(true);
            return keyInfo.KeyChar;
        }
    }
}
