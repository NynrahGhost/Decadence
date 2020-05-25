﻿using System.Text;

abstract class Image : IRenderable
{
    public Shader shader;
    public byte zIndex;

    Image IRenderable.Image => this;

    public Image(Shader shader, byte zIndex=127)
    {
        this.shader = shader;
    }

    public abstract void Render(Vector2d16 position);


    

    public class Animated : Image, IRenderable
    {
        Image IRenderable.Image
        {
            get { return frames[current]; }
        }

        short current = 0;
        Image[] frames;

        public Animated(Shader shader, byte zIndex = 127) : base(shader, zIndex) { }

        public Animated(Shader shader, Image[] frames, byte zIndex = 127) : base(shader, zIndex)
        {
            this.frames = frames;
        }

        public override void Render(Vector2d16 position)
        {
            frames[current].Render(position);
        }
    }

    class Polygon : Image
    {
        public Vector2d16[] points;

        public Polygon(Shader shader = null, params Vector2d16[] points) : base(shader)
        {
            this.points = points;
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

            /*for (int y = -(dimensions._2 << 1); y < dimensions._2 << 1; ++y)
            {
                for (int x = -(dimensions._1 << 1); x < dimensions._1 << 1; ++x)
                {
                    int posX = Renderer.worldPosition._1 - position._1 + x;
                    int posY = Renderer.worldPosition._2 - position._2 + y;
                    if (posX < Renderer.Width << 1 || posY < Renderer.Height << 1)
                        continue;
                    Renderer.buffer[posX, posY] = shader.Compute(new Vector2d16(x, y) + (dimensions*0.5), start, end);
                }
            }*/
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