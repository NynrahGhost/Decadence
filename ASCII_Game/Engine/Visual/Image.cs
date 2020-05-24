using System.Text;

abstract class Image
{
    public Shader shader;
    public byte zIndex;

    public Image(Shader shader, byte zIndex=127)
    {
        this.shader = shader;
    }

    public abstract StringBuilder Render();


    

    public class Animated : Image, IRenderable
    {
        Image IRenderable.Image
        {
            get { return frames[current]; }
        }

        short current;
        Image[] frames;

        public Animated(Shader shader, byte zIndex = 127) : base(shader, zIndex) { }

        public Animated(Shader shader, Image[] frames, byte zIndex = 127) : base(shader, zIndex)
        {
            this.frames = frames;
        }

        public override StringBuilder Render()
        {
            return frames[current].Render();
        }
    }

    class Polygon : Image
    {
        public Vector2d16[] points;

        public Polygon(Shader shader = null, params Vector2d16[] points) : base(shader)
        {
            this.points = points;
        }

        public override StringBuilder Render()
        {
            return null;
        }
    }

    public class Rectangle : Image
    {
        public Vector2d16 dimensions;

        public Rectangle(Shader shader, Vector2d16 dimensions, byte zIndex = 0) : base(shader, zIndex)
        {
            this.dimensions = dimensions;
        }

        public override StringBuilder Render()
        {
            StringBuilder sb = new StringBuilder();

            Vector2d16 start = new Vector2d16(0,0);
            Vector2d16 end = dimensions;

            for (int y = 0; y < dimensions._2; ++y)
            {
                for (int x = 0; x < dimensions._1; ++x)
                {
                    sb.Append(shader.Compute(new Vector2d16(x, y), start, end));
                }
                sb.Append("\u001b[1B\u001b["+dimensions._1+'D');
            }
            sb.Append("\u001b["+ dimensions._2+'A');
            return sb;
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

        public override StringBuilder Render()
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

            return sb;
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

        public override StringBuilder Render()
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
            return sb;
        }
    }
}
