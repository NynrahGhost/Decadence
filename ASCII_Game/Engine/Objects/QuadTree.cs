using System;
using System.Collections.Generic;
using System.Text;

class QuadTree
{
    protected Vector2d16 dimensions;

    protected Node root;

    protected QuadTree() { }

    public QuadTree(Vector2d16 dimensions, int depth, KinematicObject[] objects) : this(dimensions, depth)
    {
        FillTree(objects);
    }

    public QuadTree(Vector2d16 dimensions, int depth)
    {
        if(depth == 0)
        {
            root = new Leaf();
            return;
        }
        root = new Link(dimensions, dimensions/2, depth);
    }

    public void FillTree(KinematicObject[] objects)
    {
        foreach (KinematicObject obj in objects)
        {
            root.Add(obj);
        }
    }

    public TactileObject[] GetCollisions(KinematicObject obj)
    {
        List<TactileObject> result = new List<TactileObject>();

        root.GetCollisions(result, obj);

        return result.ToArray();
    }

    public void Add(GameObject obj)
    {
        root.Add(obj);
    }

    protected abstract class Node
    {
        public GameObject[] objects;

        public abstract void Add(GameObject obj);

        public abstract void GetCollisions(List<TactileObject> result, KinematicObject obj);

        protected void GetLocalCollisions(List<TactileObject> result, KinematicObject obj)
        {
            foreach (TactileObject gameObj in objects)
            {
                if (obj.Collide(gameObj))
                {
                    if (gameObj is Area)
                    {
                        ((Area)gameObj).Add(obj);
                    }
                    else
                    {
                        result.Add(gameObj);
                    }
                }
            }
        }
    
        protected void AddToArray(GameObject obj)
        {
            GameObject[] newArr = new GameObject[objects.Length];
            for (int i = 0; i < objects.Length; ++i)
                newArr[i] = objects[i];
            newArr[objects.Length] = obj;
        }
    }

    class Link : Node
    {
        public Node _1;
        public Node _2;
        public Node _3;
        public Node _4;

        public Vector2d16 position;

        public Link(Vector2d16 dimensions, Vector2d16 position, int depth)
        {
            this.position = position;
            if(depth-- == 0)
            {
                _1 = new Leaf();
                _2 = new Leaf();
                _3 = new Leaf();
                _4 = new Leaf();
            } 
            else
            {
                _1 = new Link(dimensions / 2, new Vector2d16(position._1 / 4, position._2 * 3 / 4), depth);
                _2 = new Link(dimensions / 2, new Vector2d16(position._1 * 3/ 4, position._2 * 3 / 4), depth);
                _3 = new Link(dimensions / 2, new Vector2d16(position._1 / 4, position._2 / 4), depth);
                _4 = new Link(dimensions / 2, new Vector2d16(position._1 * 3 / 4, position._2 / 4), depth);
            }

        }

        public override void Add(GameObject obj)
        {
            Vector2d16 bb = obj.GetBoundingBox();

            if ((obj.position._1 - position._1 < bb._1 >> 1) || (obj.position._2 - position._2 < bb._2 >> 1))
            {
                AddToArray(obj);
                return;
            }

            if (obj.position._1 < position._1)
                if (obj.position._2 > position._2)
                    _1.Add(obj);
                else
                    _3.Add(obj);
            else
                if (obj.position._2 > position._2)
                    _2.Add(obj);
                else
                    _4.Add(obj);
        }

        public override void GetCollisions(List<TactileObject> result, KinematicObject obj)
        {
            GetLocalCollisions(result, obj);

            if (obj.position._1 < position._1)
                if (obj.position._2 > position._2)
                    _1.GetCollisions(result, obj);
                else
                    _3.GetCollisions(result, obj);
            else
                if (obj.position._2 > position._2)
                    _2.GetCollisions(result, obj);
                else
                    _4.GetCollisions(result, obj);
        }
    }

    class Leaf : Node
    {
        public override void Add(GameObject obj)
        {
            AddToArray(obj);
        }

        public override void GetCollisions(List<TactileObject> result, KinematicObject obj)
        {
            GetLocalCollisions(result, obj);
        }
    }
}
