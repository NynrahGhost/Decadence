abstract class GameState
{
    public abstract void Physics(EInput input);

    public QuadTree map;


    public class Intro
    {

    }

    public class Menu
    {

    }

    public class Game : GameState
    {
        /*public Game(QuadTree map)
        {

        }*/

        public override void Physics(EInput input)
        {
            switch (input)
            {
                case EInput.moveForward:
                    Renderer.worldPosition._2 -= 1;
                    break;
                case EInput.moveBackward:
                    Renderer.worldPosition._2 += 1;
                    break;
                case EInput.moveLeft:
                    Renderer.worldPosition._1 -= 2;
                    break;
                case EInput.moveRight:
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
            }
        }
    }

    public class Editor
    {

    }
}