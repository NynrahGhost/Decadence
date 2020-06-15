using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace GameStates
{
    class LoadGame : GameState
    {
        int selection = 0;
        List<VisualObject> list = new List<VisualObject>();


        public LoadGame()
        {
            //Shader plainShader = new Shader.Plain(new Color8fg(0, 0, 0), new Color8bg(0, 0, 0), ' ');
            //Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(600, 300), 0);
            //list.Add(new VisualObject(new Vector2d16() - Renderer.Dimensions * 0.5, plainImage));
            list.Add(null);


            Shader.TextureSymbol logo = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\mainMenu.bms"), new Vector2d32(0, 17), new Vector2d32(59, 20));
            list.Add(new VisualObject(new Vector2d16(Config.screenWidth / 2 - 30, Config.screenHeight / 5), new Image.Rectangle(logo, new Vector2d16(59, 4))));
            
            Shader cursorShaderLeft = new Shader.RichText(new string[] { "  _", " / ", " ) ", "(  ", " ) ", " \\_"}, new Color8fg(255, 255, 255));
            Shader cursorShaderRight = new Shader.RichText(new string[] { "_  ", " \\ ", " ( ", "  )", " ( ", "_/ "}, new Color8fg(255, 255, 255));
            
            Image cursorLeft = new Image.Rectangle(cursorShaderLeft, new Vector2d16(3, 7));
            Image cursorRight = new Image.Rectangle(cursorShaderRight, new Vector2d16(3, 7));

            list.Add(new VisualObject(new Vector2d16(Config.screenWidth / 2 + 14, Config.screenHeight / 5 + 14), cursorRight));

            Shader.TextureSymbol frame = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\mainMenu.bms"), new Vector2d32(53, 0), new Vector2d32(77, 5));

            DirectoryInfo info = new DirectoryInfo(ResourceLoader.root + "\\Data\\Saves");
            FileInfo[] files = info.GetFiles().OrderByDescending(p => p.LastWriteTime).ToArray();
            for (int i = 0; i < files.Length; ++i)
            {
                StringBuilder sb = new StringBuilder();

                DateTime dateTime = files[i].LastWriteTime;

                if (dateTime.Hour < 10)
                    sb.Append(' ');
                sb.Append(dateTime.Hour);
                sb.Append(':');
                sb.Append(dateTime.Minute);
                sb.Append(' ');
                sb.Append(dateTime.Day);
                sb.Append('.');
                sb.Append(dateTime.Month);
                sb.Append('.');
                sb.Append(dateTime.Year);

                string date = sb.ToString();

                string name = files[i].Name.Split('.')[0];

                sb = new StringBuilder();
                sb.Append(new string(' ', 11 - name.Length / 2));
                sb.Append(name);
                sb.Append(new string(' ', 11 - name.Length / 2));
                name = sb.ToString();

                list.Add(new VisualObject(new Vector2d16(Config.screenWidth / 2 - 12, Config.screenHeight / 5 + 8 + i * 6), new Image.Rectangle(frame, new Vector2d16(24, 6))));
                list.Add(new VisualObject(new Vector2d16(Config.screenWidth / 2 - 6, Config.screenHeight / 5 + 12 + i * 6), new Image.Rectangle(
                    new Shader.RichText(date, new Color8fg(255, 255, 255)), new Vector2d16(16, 1), 128)));
                list.Add(new VisualObject(new Vector2d16(Config.screenWidth / 2 - 11, Config.screenHeight / 5 + 10 + i * 6), new Image.Rectangle(
                    new Shader.RichText(name, new Color8fg(255, 255, 255)), new Vector2d16(22, 1), 128)));
            }
            Image.ProgressBarV progressBar = new Image.ProgressBarV(19)
            {
                zIndex = 128
            };
            progressBar.SetPreset(1);
            hud = new IRenderable[]
            {
                new VisualObject(new Vector2d16(Config.screenWidth / 2 + 22, Config.screenHeight / 2 - 4), progressBar),
                //new VisualObject(new Vector2d16(Config.screenWidth / 2 - 25, Config.screenHeight / 2 - 4), progressBar)
            };

            SetSelection();

            Shape circle = new Shape.Circle(1);

            map = new Map("Test", Renderer.Dimensions, 0, list.ToArray());
            hero = new KinematicObject(new Vector2d16(Config.screenWidth / 2 - 17, Config.screenHeight / 5 + 14), circle, cursorLeft);
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
                    if (selection != list.Count / 3 - 2)
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
            ((Image.ProgressBarV)hud[0].Image).SetProgressPercentage(selection / (float)(list.Count / 3 - 2));
        }

        public void SetSelection()
        {
            if (list.Count <= 6)
                return;

            for (int index = 1; index < list.Count / 3; ++index)
            {
                list[index * 3].position = new Vector2d16(Config.screenWidth * 2, Config.screenHeight * 2);
                list[index * 3 + 1].position = new Vector2d16(Config.screenWidth * 2, Config.screenHeight * 2);
                list[index * 3 + 2].position = new Vector2d16(Config.screenWidth * 2, Config.screenHeight * 2);
            }

            if (selection == 0)
            {
                list[3].position = new Vector2d16(Config.screenWidth / 2 - 12, Config.screenHeight / 5 + 14);
                list[4].position = new Vector2d16(Config.screenWidth / 2 - 6, Config.screenHeight / 5 + 18);
                list[5].position = new Vector2d16(Config.screenWidth / 2 - 11, Config.screenHeight / 5 + 16);

                list[6].position = new Vector2d16(Config.screenWidth / 2 - 12, Config.screenHeight / 5 + 20);
                list[7].position = new Vector2d16(Config.screenWidth / 2 - 6, Config.screenHeight / 5 + 24);
                list[8].position = new Vector2d16(Config.screenWidth / 2 - 11, Config.screenHeight / 5 + 22);
            } 
            else if(selection == (list.Count / 3) - 2)
            {
                int index = (list.Count / 3 - 2) * 3;

                list[index].position = new Vector2d16(Config.screenWidth / 2 - 12, Config.screenHeight / 5 + 8);
                list[index + 1].position = new Vector2d16(Config.screenWidth / 2 - 6, Config.screenHeight / 5 + 12);
                list[index + 2].position = new Vector2d16(Config.screenWidth / 2 - 11, Config.screenHeight / 5 + 10);

                list[index + 3].position = new Vector2d16(Config.screenWidth / 2 - 12, Config.screenHeight / 5 + 14);
                list[index + 4].position = new Vector2d16(Config.screenWidth / 2 - 6, Config.screenHeight / 5 + 18);
                list[index + 5].position = new Vector2d16(Config.screenWidth / 2 - 11, Config.screenHeight / 5 + 16);
            }
            else
            {
                list[selection * 3].position = new Vector2d16(Config.screenWidth / 2 - 12, Config.screenHeight / 5 + 8);
                list[selection * 3 + 1].position = new Vector2d16(Config.screenWidth / 2 - 6, Config.screenHeight / 5 + 12);
                list[selection * 3 + 2].position = new Vector2d16(Config.screenWidth / 2 - 11, Config.screenHeight / 5 + 10);

                list[(selection + 1) * 3].position = new Vector2d16(Config.screenWidth / 2 - 12, Config.screenHeight / 5 + 14);
                list[(selection + 1) * 3 + 1].position = new Vector2d16(Config.screenWidth / 2 - 6, Config.screenHeight / 5 + 18);
                list[(selection + 1) * 3 + 2].position = new Vector2d16(Config.screenWidth / 2 - 11, Config.screenHeight / 5 + 16);

                list[(selection + 2) * 3].position = new Vector2d16(Config.screenWidth / 2 - 12, Config.screenHeight / 5 + 20);
                list[(selection + 2) * 3 + 1].position = new Vector2d16(Config.screenWidth / 2 - 6, Config.screenHeight / 5 + 24);
                list[(selection + 2) * 3 + 2].position = new Vector2d16(Config.screenWidth / 2 - 11, Config.screenHeight / 5 + 22);
            }
        }

        public void Action()
        {
            Renderer.worldPosition = new Vector2d16(0, 0);
        }
    }
}
