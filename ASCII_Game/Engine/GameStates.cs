abstract class GameState
{
    public abstract void Physics(EInput input);

    public Map map;

    public KinematicObject hero;

    public class Intro
    {

    }

    public class Menu
    {

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
            }
        }
    }

    public class Editor
    {

    }
}