using System;
using System.Collections.Generic;
using System.Text;

namespace GameStates
{
    class Credits : GameState
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
            if (input != EInput.none)
                global::Game.gameState = new Menu();
        }
    }
}
