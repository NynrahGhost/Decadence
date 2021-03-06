﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameStates
{
    /// <summary>
    /// Menu -> Run test<br/>
    /// Runs premade test map with predefined main hero's inputs.
    /// </summary>
    class Test : GameState
    {
        string actions = "wwwwwwwwwwwsssssaaaaaaaaaaaaaaaaaaawddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd";
        int frame = -1;

        bool constant = true;

        public Test()
        {
            Image tip = new Image.Rectangle(new Shader.RichText(new string[] {
            "            Press 'space' to stop and resume continious walking.",
            "When it's stopped, press any movement or controll buttons to walk one frame.",
            "                         Press ESC to leave test."
            }, new Color8(255,255,255)), new Vector2d16(67, 3), 126);
            hud = new IRenderable[]
            {
                new VisualObject(new Vector2d16(Config.screenWidth / 2 - 33, Config.screenHeight / 5 * 4), tip) //new Vector2d16(Config.screenWidth / 2, Config.screenHeight / 5 * 4)
            };

            Shader plainShader = new Shader.Plain(new Color8(0, 0, 0), new Color8(0, 0, 0), ' ');
            Image plainImage = new Image.Rectangle(plainShader, new Vector2d16(600, 300), 0);

            Shader gradientShader = new Shader.Gradient(new Color8(255, 0, 0), new Color8(255, 255, 255), new Color8(255, 0, 0), new Color8(0, 0, 255), ' ');
            Image gradientImage = new Image.Rectangle(gradientShader, new Vector2d16(20, 10));

            Shader characterShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\Textures_test.txt"), new Vector2d32(0, 95), new Vector2d32(2, 97));
            Shader buildingSmallShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\Textures_test.txt"), new Vector2d32(0, 42), new Vector2d32(33, 60));
            Shader buildingBigShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\Textures_test.txt"), new Vector2d32(0, 63), new Vector2d32(58, 81));
            Shader truckShader = new Shader.TextureSymbol(ResourceLoader.LoadResource<Atlas16>(@"Textures\Textures_test.txt"), new Vector2d32(0, 30), new Vector2d32(15, 39));

            Image characterImage = new Image.Rectangle(characterShader, new Vector2d16(3, 3), 128);
            Image buildingSmallImage = new Image.Rectangle(buildingSmallShader, new Vector2d16(33, 20), 127);
            Image buildingBigImage = new Image.Rectangle(buildingBigShader, new Vector2d16(81, 20), 127);
            Image truckImage = new Image.Rectangle(truckShader, new Vector2d16(15, 10), 127);

            //Shader earthShader = new Shader.TextureBackground(ResourceLoader.LoadResource<AtlasPNG>(@"Textures\earth.png"), new Vector2d32(0, 0), new Vector2d32(15, 15));
            //Image earthImage = new Image.Rectangle(earthShader, new Vector2d16(30, 15), 126);

            Shape circle = new Shape.Circle(1);
            Shape border = new Shape.Rectangle(new Vector2d16(31, 15));

            /*Shape carBorder = new Shape.Polygon(
                new Vector2d16(6,6),
                new Vector2d16(12,0),
                new Vector2d16(-4,2),
                //new Vector2d16(-2, 2),
                new Vector2d16(2,6)
            );*/

            Shape carBorder = new Shape.Polygon(
                new Vector2d16(0, 7),
                new Vector2d16(0, 9),
                new Vector2d16(8, 9),

                new Vector2d16(6, 0),
                new Vector2d16(14, 0),
                new Vector2d16(14, 3)
            );

            PhysicalObject character = new PhysicalObject(new Vector2d16(98, 16), circle, characterImage);

            var tmp = new GameObject[]
            {
                //new VisualObject(new Vector2d16(-100, -50), plainImage),
                //new PhysicalObject(new Vector2d16(50,10), circle, gradientImage),

                new TactileObject(new Vector2d16(70,13), border),
                new TactileObject(new Vector2d16(109,13), border),
                new TactileObject(new Vector2d16(134,13), border),
                //new TactileObject(new Vector2d16(50,70), border),

                new TactileObject(new Vector2d16(40, 20), carBorder),

                new VisualObject(new Vector2d16(40, 0), buildingSmallImage),
                new VisualObject(new Vector2d16(80, 0), buildingBigImage),
                new VisualObject(new Vector2d16(24, 14), truckImage),

                new PhysicalObject(new Vector2d16(98, 16), circle, characterImage)
                //new PhysicalObject(new Vector2d16(98, 16), circle, characterImage),
            };

            events.Add(new Animation(tmp[7]).
                SetProperties((object x, object y) => { ((GameObject)x).position = (Vector2d16)y; }).
                AddFrame(new Vector2d16(98, 20)).
                AddFrame(new Vector2d16(40, 20)).
                AddFrame(new Vector2d16(40, 20)).
                AddFrame(new Vector2d16(98, 20)).
                AddFrame(new Vector2d16(98, 20)).
                AddFunctions(Animation.liniar).
                AddFunctions(Animation.liniar).
                AddFunctions(Animation.liniar).
                AddFunctions(Animation.liniar).
                AddFunctions(Animation.liniar).
                AddTimespan(20000000L).
                AddTimespan(40000000L).
                AddTimespan(60000000L).
                AddTimespan(80000000L).
                AddTimespan(80000000L).
                SetActive(true));

            map = new Map("Test", new Vector2d16(2000, 1000), 1, tmp);
            hero = new KinematicObject(new Vector2d16(Config.screenWidth / 2, Config.screenHeight / 2), circle, characterImage);
            Renderer.SetObjects(map.GetVisuals());
        }

        public override void Physics(EInput input)
        {
            if (input == EInput.attack)
            {
                constant = !constant;
            }
            if (!constant & input == EInput.none)
            {
                return;
            }
            if(input == EInput.escape)
            {
                global::Game.gameState = new Menu();
                Renderer.worldPosition = new Vector2d16(0, 0);
                return;
            }
            Renderer.SetObjects(map.GetVisuals());
            if (frame == actions.Length-1)
                return;
            switch (actions[++frame])
            {
                case 'w':
                    if (hero.Move(hero.position + new Vector2d16(0, -1)))
                        Renderer.worldPosition._2 -= 1;
                    break;
                case 's':
                    if (hero.Move(hero.position + new Vector2d16(0, 1)))
                        Renderer.worldPosition._2 += 1;
                    break;
                case 'a':
                    if (hero.Move(hero.position + new Vector2d16(-2, 0)))
                        Renderer.worldPosition._1 -= 2;
                    break;
                case 'd':
                    if (hero.Move(hero.position + new Vector2d16(2, 0)))
                        Renderer.worldPosition._1 += 2;
                    break;
                case 'e':
                    System.Console.Beep();
                    break;
                case ' ':
                    System.Console.Beep();
                    System.Console.Beep();
                    System.Console.Beep();
                    break;
            }
        }

        public override void Process(float delta)
        {
            //((GameObject)hud[0]).position += (1, 0); 
            //Console.WriteLine(new Color8fg(255,255,255) + ((Animation)events[0]).time);
            foreach (IEvent e in events)
                if (e.IsActive())
                    e.Process(delta);
        }
    }
}

