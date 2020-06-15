using System.Text;

abstract class Image : IRenderable
{
    public Shader shader;
    public byte zIndex;

    Image IRenderable.Image => this;

    public Image() { }

    public Image(Shader shader, byte zIndex=127)
    {
        this.shader = shader;
        this.zIndex = zIndex;
    }

    public abstract void Render(Vector2d16 position);

    public abstract Vector2d16 GetVisualBB();

    public class Animated : Image, IRenderable
    {
        Image IRenderable.Image
        {
            get { return frames[current]; }
        }

        short current = 0;
        Image[] frames;

        public Animated(params Image[] frames) : base(null)
        {
            this.frames = frames;
        }

        public Animated(Shader shader, byte zIndex = 127) : base(shader, zIndex) { }

        public Animated(Shader shader, Image[] frames, byte zIndex = 127) : base(shader, zIndex)
        {
            this.frames = frames;
        }

        public override void Render(Vector2d16 position)
        {
            frames[current].Render(position);
        }

        public override Vector2d16 GetVisualBB()
        {
            return frames[current].GetVisualBB();
        }
    }

    public class Group : Image, IRenderable
    {
        Image[] group;

        public Group(Shader shader, byte zIndex = 127) : base(shader, zIndex) { }

        public Group(Shader shader, Image[] group, byte zIndex = 127) : base(shader, zIndex)
        {
            this.group = group;
        }

        public override void Render(Vector2d16 position)
        {
            foreach (Image image in group)
                image.Render(position);
        }

        public override Vector2d16 GetVisualBB()
        {
            Vector2d16 max = new Vector2d16(0, 0);
            foreach(Image image in group)
            {
                Vector2d16 current = image.GetVisualBB();
                if (current._1 * current._2 > max._1 * max._2)
                    max = current;
            }
            return max;
        }
    }

    class Polygon : Image
    {
        public Vector2d16[] points;

        public Polygon(Shader shader = null, params Vector2d16[] points) : base(shader)
        {
            this.points = points;
        }

        public override Vector2d16 GetVisualBB()
        {
            throw new System.NotImplementedException();
        }

        public override void Render(Vector2d16 position)
        {

        }
    }

    public class Rectangle : Image
    {
        public Vector2d16 dimensions;

        public Rectangle(Shader shader, Vector2d16 dimensions, byte zIndex = 0) : base(shader, zIndex)
        {
            this.dimensions = dimensions;
        }

        public override Vector2d16 GetVisualBB()
        {
            return dimensions;
        }

        public override void Render(Vector2d16 position)
        {
            Vector2d16 start = new Vector2d16(0,0);
            Vector2d16 end = dimensions;

            for (int y = 0; y < dimensions._2; ++y)
            {
                for (int x = 0; x < dimensions._1; ++x)
                {
                    int posX = position._1 - Renderer.worldPosition._1 + x;
                    int posY = position._2 - Renderer.worldPosition._2 + y;
                    if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                        continue;
                    Renderer.buffer[posY, posX] = shader.Compute(new Vector2d16(x, y), start, end);
                }
            }
        }
    }

    public class RectangleAngular : Rectangle
    {
        public float angle;

        public RectangleAngular(Shader shader, Vector2d16 dimensions, float angle) : base(shader, dimensions)
        {
            this.angle = angle;
        }
    }

    public class Triangle
    {
        public Vector2d16 dimensions;
    }

    public class TriangleAngular : Triangle
    {
        public float angle;
    }

    public class Line : Image
    {
        public Vector2d16 start;
        public Vector2d16 end;
        public byte density;

        public Line(Shader shader, Vector2d16 start, Vector2d16 end, byte density) : base(shader)
        {
            this.start = start;
            this.end = end;
            this.density = density;
        }

        public override Vector2d16 GetVisualBB()
        {
            return end - start;
        }

        public override void Render(Vector2d16 position)
        {
            StringBuilder sb = new StringBuilder();

            double m = (start._1 + end._1) / (start._2 + end._2);
            double b = start._2 - m * start._1;

            Vector2d16 previous = start;
            Vector2d16 current;
            for (short x = start._1; x < end._1; ++x)
            {
                current = new Vector2d16(x, (short)(m * x + b));
                if (current._1 - previous._1 > 0)
                {
                    sb.Append("\u001b[1C");
                } 
                else if (current._1 - previous._1 < 0)
                {
                    sb.Append("\u001b[1D");
                }
                if (current._2 - previous._2 > 0)
                {
                    sb.Append("\u001b[1A");
                }
                else if (current._2 - previous._2 < 0)
                {
                    sb.Append("\u001b[1B");
                }
                sb.Append(shader.Compute(current, start, end));
                previous = current;
            }
        }
    }

    public class BezierCurve : Image
    {
        public Vector2d16 start;
        public Vector2d16 middle;
        public Vector2d16 end;
        public byte density;

        public BezierCurve(Shader shader, Vector2d16 start, Vector2d16 middle, Vector2d16 end, byte density) : base(shader)
        {
            this.start = start;
            this.middle = middle;
            this.end = end;
            this.density = density;
        }

        public override Vector2d16 GetVisualBB()
        {
            return end - start;
        }

        public override void Render(Vector2d16 position)
        {
            StringBuilder sb = new StringBuilder();

            Vector2d16 previous = start;
            Vector2d16 current;
            for (float t = 0; t < 1; t += 0.05f)
            {
                current = new Vector2d16(
                        (1 - t) * (1 - t) * start._1 + 2 * t * (1 - t) * middle._1 + t * t * end._1,
                        (1 - t) * (1 - t) * start._2 + 2 * t * (1 - t) * middle._2 + t * t * end._2
                    );
                //System.Console.WriteLine(current);
                if (!((current._1 == previous._1) && (current._2 == previous._2)))
                {
                    if (current._1 > previous._1)
                    {
                        sb.Append("\u001b[1C");
                    }
                    if (current._1 < previous._1)
                    {
                        sb.Append("\u001b[1D");
                    }
                    if (current._2 > previous._2)
                    {
                        sb.Append("\u001b[1A");
                    }
                    if (current._2 < previous._2)
                    {
                        sb.Append("\u001b[1B");
                    }
                    sb.Append(shader.Compute(current, start, end));
                }
                previous = current;
            }
        }
    }


    public class ProgressBarV : Image
    {
        public short length;
        public short progress;

        public byte endU;
        public byte endL;
        public byte mid;
        public byte frame;

        public Color8fg endUfg = new Color8fg(255,255,255);
        public Color8fg endLfg = new Color8fg(255, 255, 255);
        public Color8fg midfg = new Color8fg(255, 255, 255);
        public Color8fg framefg = new Color8fg(255, 255, 255);

        public ProgressBarV(short length, byte endU = 0 , byte endL = 0, byte mid = 0 , byte frame = 0, float percentage = 0, byte zIndex = 0)
        {
            this.length = length;
            this.endU = endU;
            this.endL = endL;
            this.mid = mid;
            this.frame = frame;
            SetProgressPercentage(percentage);
        }

        private static string[,] endUpper = new string[,]
        {
            { "╔═╗",
              "║▲║",
              "╚╦╝" },
            { "┏═┓",
              "║▲║",
              "┗╦┛" },
            { "╭═╮",
              "║▲║",
              "╰╦╯" },
            { "╔═╗",
              "║▲║",
              "╠═╣" },
            { " ▄ ",
              " ▲ ",
              " █ " }
        };
        private static string[,] endLower = new string[,]
        {
            { "╔╩╗",
              "║▼║",
              "╚═╝" },
            { "┏╩┓",
              "║▼║",
              "┗═┛" },
            { "╭╩╮",
              "║▼║",
              "╰═╯" },
            { "╠═╣",
              "║▼║",
              "╚═╝" },
            { " █ ",
              " ▼ ",
              " ▀ " }
        };
        private static string[,] middle = new string[,]
        {
            { "┏╨┓",
              "┃ ┃",
              "┗╥┛" },
            { "╱╩╲",
              "║♦║",
              "╲╦╱" },
            { "╭╩╮",
              "║♦║",
              "╰╦╯" },
            { "╞═╡",
              "║♦║",
              "╞═╡" },
            { " ▄ ",
              " █ ",
              " ▀ " }
        };
        private static string[] body = new string[]
        {
            " ║ ",
            "│ │",
            " █ "
        };

        public void SetProgressPercentage(float percentage)
        {
            if (percentage >= 1)
                progress = (short)(length - 6);
            else if (percentage <= 0)
                progress = 3;
            else
                progress = (short) ((length - 9) * percentage + 3);
        }

        /// <summary>
        /// Sets specific preset to progress bar<br/>
        /// 0 - square, 1 - sci-fi, 2 - rounded, 3 - wide square, 4 - modern
        /// </summary>
        /// <param name="preset">0 - square, 1 - sci-fi, 2 - rounded, 3 - wide square, 4 - modern</param>
        /// <returns></returns>
        public ProgressBarV SetPreset(byte preset)
        {
            switch (preset)
            {
                case 0:
                    endU = 0;
                    endL = 0;
                    mid = 0;
                    frame = 0;
                    break;
                case 1:
                    endU = 1;
                    endL = 1;
                    mid = 1;
                    frame = 0;
                    break;
                case 2:
                    endU = 2;
                    endL = 2;
                    mid = 2;
                    frame = 0;
                    break;
                case 3:
                    endU = 3;
                    endL = 3;
                    mid = 3;
                    frame = 1;
                    break;
                case 4:
                    endU = 4;
                    endL = 4;
                    mid = 4;
                    frame = 2;
                    break;
            }
            return this;
        }

        public override Vector2d16 GetVisualBB()
        {
            return new Vector2d16((short) 3, length);
        }

        public override void Render(Vector2d16 position)
        {
            int posX;
            int posY;

            for (int y = 3; y < length-3; ++y)
                for (int x = 0; x < 3; ++x)
                {
                    posX = position._1 - Renderer.worldPosition._1 + x;
                    posY = position._2 - Renderer.worldPosition._2 + y;
                    if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                        continue;
                    Renderer.buffer[posY, posX] = new Fragment8(framefg, new Color8bg(0, 0, 0), body[frame][x]);

                }

            for (int y = 0; y < 3; ++y)
                for (int x = 0; x < 3; ++x)
                {
                    posX = position._1 - Renderer.worldPosition._1 + x;
                    posY = position._2 - Renderer.worldPosition._2 + y;
                    if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                        continue;
                    Renderer.buffer[posY, posX] = new Fragment8(endUfg, new Color8bg(0, 0, 0), endUpper[endU, y][x]);
                }

            for (int y = 0; y < 3; ++y)
                for (int x = 0; x < 3; ++x)
                {
                    posX = position._1 - Renderer.worldPosition._1 + x;
                    posY = position._2 - Renderer.worldPosition._2 + y + progress;
                    if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                        continue;
                    Renderer.buffer[posY, posX] = new Fragment8(midfg, new Color8bg(0, 0, 0), middle[mid, y][x]);
                }

            for (int y = 0; y < 3; ++y)
                for (int x = 0; x < 3; ++x)
                {
                    posX = position._1 - Renderer.worldPosition._1 + x;
                    posY = position._2 - Renderer.worldPosition._2 + y + length-3;
                    if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                        continue;
                    Renderer.buffer[posY, posX] = new Fragment8(endLfg, new Color8bg(0,0,0), endLower[endL, y][x]);
                }
        }
    }
}
