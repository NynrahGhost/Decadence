using System;
using System.Collections.Generic;
using System.Text;

internal partial class GUI
{
    public static Shader.Plain plain = new Shader.Plain((255, 255, 255), (0, 0, 0), ' ');

    public static Shader.Plain lightH = new Shader.Plain((255, 255, 255), (0, 0, 0), '─');
    public static Shader.Plain lightV = new Shader.Plain((255, 255, 255), (0, 0, 0), '│');
    public static Shader.Plain lightUL = new Shader.Plain((255, 255, 255), (0, 0, 0), '┌');
    public static Shader.Plain lightUR = new Shader.Plain((255, 255, 255), (0, 0, 0), '┐');
    public static Shader.Plain lightLL = new Shader.Plain((255, 255, 255), (0, 0, 0), '┘');
    public static Shader.Plain lightLR = new Shader.Plain((255, 255, 255), (0, 0, 0), '└');

    public static Shader.Plain heavyH = new Shader.Plain((255, 255, 255), (0, 0, 0), '━');
    public static Shader.Plain heavyV = new Shader.Plain((255, 255, 255), (0, 0, 0), '┃');
    public static Shader.Plain heavyUL = new Shader.Plain((255, 255, 255), (0, 0, 0), '┏');
    public static Shader.Plain heavyUR = new Shader.Plain((255, 255, 255), (0, 0, 0), '┓');
    public static Shader.Plain heavyLL = new Shader.Plain((255, 255, 255), (0, 0, 0), '┛');
    public static Shader.Plain heavyLR = new Shader.Plain((255, 255, 255), (0, 0, 0), '┗');

    public static Shader.Plain doubleH = new Shader.Plain((255, 255, 255), (0, 0, 0), '═');
    public static Shader.Plain doubleV = new Shader.Plain((255, 255, 255), (0, 0, 0), '║');
    public static Shader.Plain doubleUL = new Shader.Plain((255, 255, 255), (0, 0, 0), '╔');
    public static Shader.Plain doubleUR = new Shader.Plain((255, 255, 255), (0, 0, 0), '╗');
    public static Shader.Plain doubleLL = new Shader.Plain((255, 255, 255), (0, 0, 0), '╝');
    public static Shader.Plain doubleLR = new Shader.Plain((255, 255, 255), (0, 0, 0), '╚');

    public static Shader.Plain roundUL = new Shader.Plain((255, 255, 255), (0, 0, 0), '╭');
    public static Shader.Plain roundUR = new Shader.Plain((255, 255, 255), (0, 0, 0), '╮');
    public static Shader.Plain roundLL = new Shader.Plain((255, 255, 255), (0, 0, 0), '╯');
    public static Shader.Plain roundLR = new Shader.Plain((255, 255, 255), (0, 0, 0), '╰');

    public class Container : GameObject, IRenderable
    {
        Vector2d16 dimensions;
        VisualObject[] borders;

        public Container(Vector2d16 position, Vector2d16 dimensions, bool rounded = false, byte borderType = 0, byte zIndex = 127) : base(position)
        {
            this.dimensions = dimensions;
            switch (borderType)
            {
                case 0:
                    break;
                case 1:
                    borders = new VisualObject[]
                    {
                        new VisualObject((0, 0), new Image.Rectangle(plain, (dimensions._1, dimensions._2))),

                        new VisualObject((0, 0), new Image.Rectangle(lightH, (dimensions._1, 1))),
                        new VisualObject((0, dimensions._2), new Image.Rectangle(lightH, (dimensions._1, 1))),
                        new VisualObject((0, 0), new Image.Rectangle(lightV, (1, dimensions._2))),
                        new VisualObject((dimensions._1, 0), new Image.Rectangle(lightV, (1, dimensions._2))),

                        new VisualObject((0, 0), new Image.Rectangle(lightUL, (1, 1))),
                        new VisualObject((dimensions._1, 0), new Image.Rectangle(lightUR, (1, 1))),
                        new VisualObject((0, dimensions._2), new Image.Rectangle(lightLR, (1, 1))),
                        new VisualObject((dimensions._1, dimensions._2), new Image.Rectangle(lightLL, (1, 1)))
                    };
                    break;
                case 2:
                    borders = new VisualObject[]
                    {
                        new VisualObject((0, 0), new Image.Rectangle(plain, (dimensions._1, dimensions._2))),

                        new VisualObject((0, 0), new Image.Rectangle(heavyH, (dimensions._1, 1))),
                        new VisualObject((0, dimensions._2), new Image.Rectangle(heavyH, (dimensions._1, 1))),
                        new VisualObject((0, 0), new Image.Rectangle(heavyV, (1, dimensions._2))),
                        new VisualObject((dimensions._1, 0), new Image.Rectangle(heavyV, (1, dimensions._2))),

                        new VisualObject((0, 0), new Image.Rectangle(heavyUL, (1, 1))),
                        new VisualObject( (dimensions._1, 0), new Image.Rectangle(heavyUR, (1, 1))),
                        new VisualObject((0, dimensions._2), new Image.Rectangle(heavyLR, (1, 1))),
                        new VisualObject((dimensions._1, dimensions._2), new Image.Rectangle(heavyLL, (1, 1)))
                    };
                    break;
                case 3:
                    borders = new VisualObject[]
                    {
                        new VisualObject((0, 0), new Image.Rectangle(plain, (dimensions._1, dimensions._2))),

                        new VisualObject((0, 0), new Image.Rectangle(doubleH, (dimensions._1, 1))),
                        new VisualObject((0, dimensions._2), new Image.Rectangle(doubleH, (dimensions._1, 1))),
                        new VisualObject((0, 0), new Image.Rectangle(doubleV, (1, dimensions._2))),
                        new VisualObject((dimensions._1, 0), new Image.Rectangle(doubleV, (1, dimensions._2))),

                        new VisualObject((0, 0), new Image.Rectangle(doubleUL, (1, 1))),
                        new VisualObject((dimensions._1, 0), new Image.Rectangle(doubleUR, (1, 1))),
                        new VisualObject((0, dimensions._2), new Image.Rectangle(doubleLR, (1, 1))),
                        new VisualObject((dimensions._1, dimensions._2), new Image.Rectangle(doubleLL, (1, 1)))
                    };
                    break;
            }
            if (rounded)
            {
                borders[5].Image.shader = roundUL;
                borders[6].Image.shader = roundUR;
                borders[7].Image.shader = roundLR;
                borders[8].Image.shader = roundLL;
            }
        }

        public Image Image => throw new NotImplementedException();

        public Vector2d16 GetVisualBB()
        {
            return dimensions;
        }

        public void Render(Vector2d16 position)
        {
            foreach (VisualObject border in borders)
                border.Render(border.position + position);
        }
    }
}