using System;
using System.Collections.Generic;
using System.Text;

abstract class Shape
{
    public abstract Vector2d16 GetPhysicalBB();

    public abstract bool Collide(Vector2d16 ownPosition, Shape shape, Vector2d16 position);

    private static bool Collide(Circle circle1, Vector2d16 pos1, Circle circle2, Vector2d16 pos2)
    {
        return pos1.Distance(pos2) < circle1.radius + circle2.radius;
    }

    private static bool Collide(Circle circle, Vector2d16 pos1, Rectangle rectangle, Vector2d16 pos2)
    {
        //return pos1.Distance(pos2) < circle.radius + circle2.radius;
        return false;
    }

    private static bool Collide(Rectangle rectangle1, Vector2d16 pos1, Rectangle rectangle2, Vector2d16 pos2)
    {
        //return pos1.Distance(pos2) < circle.radius + circle2.radius;
        return false;
    }

    public class Polygon { }

    public class Rectangle : Shape
    {
        Vector2d16 dimensions;

        public Rectangle(Vector2d16 dimensions)
        {
            this.dimensions = dimensions;
        }

        public override bool Collide(Vector2d16 ownPosition, Shape shape, Vector2d16 position)
        {
            switch (shape)
            {
                case Circle c:
                    return Collide(c, position, this, ownPosition);
                case Rectangle r:
                    return Collide(this, ownPosition, r, position);
            }
            return false;
        }

        public override Vector2d16 GetPhysicalBB()
        {
            return dimensions;
        }
    }

    public class Triangle { }

    public class Circle : Shape
    {
        public short radius;

        public Circle(short radius)
        {
            this.radius = radius;
        }

        public override bool Collide(Vector2d16 ownPosition, Shape shape, Vector2d16 position)
        {
            switch (shape)
            {
                case Circle c:
                    return Collide(c, position, this, ownPosition);
                case Rectangle r:
                    return Collide(this, ownPosition, r, position);
            }
            return false;
        }

        public override Vector2d16 GetPhysicalBB()
        {
            return new Vector2d16(radius*2,radius*2);
        }
    }
}