using System;
using System.Collections.Generic;
using System.Text;

namespace GameStates
{
    class MapTest : GameState
    {
        public MapTest(string path)
        {
            map = new Map(path);
        }

        public override void Physics(EInput input)
        {
            Renderer.SetObjects(map.GetVisuals());
            switch (input)
            {
                case EInput.moveForward:
                    if (hero.Move(hero.Position + new Vector2d16(0, -1)))
                        Renderer.worldPosition._2 -= 1;
                    break;
                case EInput.moveBackward:
                    if (hero.Move(hero.Position + new Vector2d16(0, 1)))
                        Renderer.worldPosition._2 += 1;
                    break;
                case EInput.moveLeft:
                    if (hero.Move(hero.Position + new Vector2d16(-2, 0)))
                        Renderer.worldPosition._1 -= 2;
                    break;
                case EInput.moveRight:
                    if (hero.Move(hero.Position + new Vector2d16(2, 0)))
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
                    Renderer.worldPosition = new Vector2d16(0, 0);
                    break;
            }
        }
    }
}
