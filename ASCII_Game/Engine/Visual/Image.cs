using System.Text;

/// <summary>
/// Class that represents shape of renderable object.
/// </summary>
internal abstract partial class Image : IRenderable
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

    /// <summary>
    /// Start rendering process, specifing object's position on map.
    /// </summary>
    /// <param name="position">Object's position on map.</param>
    public abstract void Render(Vector2d16 position);

    /// <summary>
    /// Retrieves bounding box of object's visual representation.
    /// </summary>
    public abstract Vector2d16 GetVisualBB();

    /// <summary>
    /// Class for a sequence of images, that change with animation rules.
    /// </summary>
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

    /// <summary>
    /// Class that contains a group of images. Helps recognize them as one.
    /// </summary>
    public class Group : Image, IRenderable
    {
        Image[] group;

        public Group(params Image[] group) 
        {
            this.group = group;
        }

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

    /// <summary>
    /// Non-rotatable rectangle with its dimensions.
    /// </summary>
    public class Rectangle : Image
    {
        public Vector2d16 dimensions;

        protected Rectangle() { }

        public Rectangle(Shader shader, Vector2d16 dimensions, byte zIndex = 127) : base(shader, zIndex)
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

    /// <summary>
    /// Rotatable rectangle with its dimensions.
    /// </summary>
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
}
