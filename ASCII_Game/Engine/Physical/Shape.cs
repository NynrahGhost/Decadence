using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Class that specifies shape of an object, that participates in collision detection.
/// </summary>
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
        //Console.WriteLine("HERE");
        Vector2d16 aMin = pos1;
        Vector2d16 aMax = pos1 + rectangle.dimensions;
        Vector2d16 bMin = pos2;
        Vector2d16 bMax = pos2 + circle.GetPhysicalBB();
        if (
            (aMin._1 <= bMax._1 & aMax._1 >= bMin._1) &&
            (aMin._2 <= bMax._2 & aMax._2 >= bMin._2)
        ) 
            return true; 
        return false;
    }

    private static bool Collide(Rectangle rectangle1, Vector2d16 pos1, Rectangle rectangle2, Vector2d16 pos2)
    {
        Vector2d16 aMin = pos1;
        Vector2d16 aMax = pos1 + rectangle1.dimensions;
        Vector2d16 bMin = pos2;
        Vector2d16 bMax = pos2 + rectangle2.dimensions;
        if (
            (aMin._1 <= bMax._1 & aMax._1 >= bMin._1) &&
            (aMin._2 <= bMax._2 & aMax._2 >= bMin._2)
        )
            return true;
        return false;
    }

    public class Polygon { }

    public class Rectangle : Shape
    {
        public Vector2d16 dimensions;

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