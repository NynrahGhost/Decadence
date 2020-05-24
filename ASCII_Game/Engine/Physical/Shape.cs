using System;
using System.Collections.Generic;
using System.Text;

abstract class Shape
{
    public abstract Vector2d16 GetBoundingBox();

    public static bool Collide(Shape shape1, Vector2d16 pos1, Shape shape2, Vector2d16 pos2)
    {
        return false;// obj1.position;
    }

    public static bool Collide(Shape.Circle circle1, Vector2d16 pos1, Shape.Circle circle2, Vector2d16 pos2)
    {
        return pos1.Distance(pos2) < circle1.radius + circle2.radius;
    }

    public class Polygon { }

    public class Rectangle { }

    public class Triangle { }

    public class Circle : Shape
    {
        public short radius;

        public override Vector2d16 GetBoundingBox()
        {
            return new Vector2d16(radius*2,radius*2);
        }
    }
}