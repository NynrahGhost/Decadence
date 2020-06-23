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

    private static bool Collide(Circle circle, Vector2d16 pos1, Polygon polygon, Vector2d16 pos2)
    {
        Vector2d16[] points1 = new Vector2d16[5]
        {
            new Vector2d16(-circle.radius, -circle.radius) + pos1,
            new Vector2d16(-circle.radius, circle.radius) + pos1,
            new Vector2d16(circle.radius, circle.radius) + pos1,
            new Vector2d16(circle.radius, -circle.radius) + pos1,
            new Vector2d16(-circle.radius, -circle.radius) + pos1
        };

        Vector2d16[] points2 = new Vector2d16[polygon.points.Length + 1];
        for (int i = 0; i < polygon.points.Length; ++i)
            points2[i] = polygon.points[i] + pos2;
        points2[points2.Length - 1] = points2[0];

        Func<Vector2d16, Vector2d16, Vector2d16, int> area = (a, b, c) => (b._1 - a._1) * (c._2 - a._2) - (b._2 - a._2) * (c._1 - a._1);
        Func<int, int, int, int, bool> intersect = (a, b, c, d) =>
        {
            if (a > b)
            {
                a += b; b = a - b; a -= b;
            }
            if (c > d)
            {
                c += d; d = c - d; c -= d;
            }
            return Math.Max(a, c) <= Math.Min(b, d);
        };

        for (int i = 0; i < points1.Length - 1; ++i)
            for (int j = 0; j < points2.Length - 1; ++j)
                if (
                    intersect(points1[i]._1, points1[i + 1]._1, points2[j]._1, points2[j + 1]._1) &&
                    intersect(points1[i]._2, points1[i + 1]._2, points2[j]._2, points2[j + 1]._2) &&
                    area(points1[i], points1[i + 1], points2[j]) *
                    area(points1[i], points1[i + 1], points2[j + 1]) <= 0 &&
                    area(points2[j], points2[j + 1], points1[i]) *
                    area(points2[j], points2[j + 1], points1[i + 1]) <= 0
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

    private static bool Collide(Rectangle rectangle, Vector2d16 pos1, Polygon polygon, Vector2d16 pos2)
    {
        Vector2d16[] points1 = new Vector2d16[5]
        {
            new Vector2d16(rectangle.dimensions._1 * -0.5, rectangle.dimensions._2 * -0.5) + pos1,
            new Vector2d16(rectangle.dimensions._1 * -0.5, rectangle.dimensions._2 * 0.5) + pos1,
            new Vector2d16(rectangle.dimensions._1 * 0.5, rectangle.dimensions._2 * 0.5) + pos1,
            new Vector2d16(rectangle.dimensions._1 * 0.5, rectangle.dimensions._2 * -0.5) + pos1,
            new Vector2d16(rectangle.dimensions._1 * -0.5, rectangle.dimensions._2 * -0.5) + pos1
        };

        Vector2d16[] points2 = new Vector2d16[polygon.points.Length + 1];
        for (int i = 0; i < polygon.points.Length; ++i)
            points2[i] = polygon.points[i] + pos2;
        points2[points2.Length - 1] = points2[0];

        Func<Vector2d16, Vector2d16, Vector2d16, int> area = (a, b, c) => (b._1 - a._1) * (c._2 - a._2) - (b._2 - a._2) * (c._1 - a._1);
        Func<int, int, int, int, bool> intersect = (a, b, c, d) =>
        {
            if (a > b)
            {
                a += b; b = a - b; a -= b;
            }
            if (c > d)
            {
                c += d; d = c - d; c -= d;
            }
            return Math.Max(a, c) <= Math.Min(b, d);
        };

        for (int i = 0; i < points1.Length - 1; ++i)
            for (int j = 0; j < points2.Length - 1; ++j)
                if (
                    intersect(points1[i]._1, points1[i + 1]._1, points2[j]._1, points2[j + 1]._1) &&
                    intersect(points1[i]._2, points1[i + 1]._2, points2[j]._2, points2[j + 1]._2) &&
                    area(points1[i], points1[i + 1], points2[j]) *
                    area(points1[i], points1[i + 1], points2[j + 1]) <= 0 &&
                    area(points2[j], points2[j + 1], points1[i]) *
                    area(points2[j], points2[j + 1], points1[i + 1]) <= 0
                    )
                    return true;
        return false;
    }

    private static bool Collide(Polygon polygon1, Vector2d16 pos1, Polygon polygon2, Vector2d16 pos2)
    {
        Vector2d16[] points1 = new Vector2d16[polygon1.points.Length + 1];
        for (int i = 0; i < polygon1.points.Length; ++i)
            points1[i] = polygon1.points[i] + pos1;
        points1[points1.Length - 1] = points1[0];

        Vector2d16[] points2 = new Vector2d16[polygon2.points.Length + 1];
        for (int i = 0; i < polygon2.points.Length; ++i)
            points2[i] = polygon2.points[i] + pos2;
        points2[points2.Length - 1] = points2[0];

        Func<Vector2d16, Vector2d16, Vector2d16, int> area = (a, b, c) => (b._1 - a._1) * (c._2 - a._2) - (b._2 - a._2) * (c._1 - a._1);
        Func<int, int, int, int, bool> intersect = (a, b, c, d) =>
        {
            if (a > b)
            {
                a += b; b = a - b; a -= b;
            }
            if (c > d)
            {
                c += d; d = c - d; c -= d;
            }
            return Math.Max(a, c) <= Math.Min(b, d);
        };

        for (int i = 0; i < points1.Length - 1; ++i)
            for (int j = 0; j < points2.Length - 1; ++j)
                if (
                    intersect(points1[i]._1, points1[i + 1]._1, points2[j]._1, points2[j + 1]._1) &&
                    intersect(points1[i]._2, points1[i + 1]._2, points2[j]._2, points2[j + 1]._2) &&
                    area(points1[i], points1[i + 1], points2[j]) *
                    area(points1[i], points1[i + 1], points2[j + 1]) <= 0 &&
                    area(points2[j], points2[j + 1], points1[i]) *
                    area(points2[j], points2[j + 1], points1[i + 1]) <= 0
                    )
                    return true;
        return false;
    }

    public class Polygon : Shape
    {
        public Vector2d16[] points;

        public Polygon(params Vector2d16[] points)
        {
            this.points = points;
        }

        public override bool Collide(Vector2d16 ownPosition, Shape shape, Vector2d16 position)
        {
            switch (shape)
            {
                case Circle c:
                    return Collide(c, position, this, ownPosition);
                case Rectangle r:
                    return Collide(r, position, this, ownPosition);
                case Polygon p:
                    return Collide(p, position, this, ownPosition);
            }
            return false;
        }

        public override Vector2d16 GetPhysicalBB()
        {
            short xMax = 0;
            short yMax = 0;
            foreach(Vector2d16 point in points)
            {
                if (point._1 > xMax)
                    xMax = point._1;
                if (point._2 > yMax)
                    yMax = point._2;
            }
            return new Vector2d16(xMax, yMax);
        }
    }

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
                case Polygon p:
                    return Collide(this, position, p, ownPosition);
            }
            return false;
        }

        public override Vector2d16 GetPhysicalBB()
        {
            return dimensions;
        }
    }

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
                case Polygon p:
                    return Collide(this, position, p, ownPosition);
            }
            return false;
        }

        public override Vector2d16 GetPhysicalBB()
        {
            return new Vector2d16(radius*2,radius*2);
        }
    }
}