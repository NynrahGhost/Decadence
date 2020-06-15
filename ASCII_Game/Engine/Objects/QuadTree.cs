using System;
using System.Collections.Generic;
using System.Text;

class QuadTree
{
    protected Vector2d16 dimensions;

    protected Node root;

    protected QuadTree() { }

    public QuadTree(Vector2d16 dimensions, int depth, GameObject[] objects) : this(dimensions, depth)
    {
        FillTree(objects);
    }

    public QuadTree(Vector2d16 dimensions, int depth)
    {
        this.dimensions = dimensions;
        if(depth == 0)
        {
            root = new Leaf();
            return;
        }
        root = new Link(dimensions, dimensions / 2, depth);
    }

    public void FillTree(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            root.Add(obj);
        }
    }

    public ICollidable[] GetCollisions(KinematicObject obj)
    {
        List<ICollidable> result = new List<ICollidable>();

        root.GetCollisions(result, obj);

        return result.ToArray();
    }

    public IRenderable[] GetVisuals()
    {
        List<IRenderable> result = new List<IRenderable>();
        root.GetVisuals(result);
        return result.ToArray();
    }

    public void Add(GameObject obj)
    {
        root.Add(obj);
    }



    protected abstract class Node
    {
        public GameObject[] objects = new GameObject[0];

        public abstract void Add(GameObject obj);

        public abstract void GetCollisions(List<ICollidable> result, KinematicObject obj);

        public abstract void GetVisuals(List<IRenderable> result);

        protected void GetLocalCollisions(List<ICollidable> result, KinematicObject obj)
        {
            if (objects != null)
                foreach (GameObject gameObj in objects)
                {
                    if (gameObj is ICollidable)
                    {
                        ICollidable collidable = gameObj as ICollidable;
                        if (obj.Collide(collidable))
                        {
                            if (gameObj is Area)
                            {
                                ((Area)gameObj).Add(obj);
                            }
                            else
                            {
                                result.Add(collidable);
                            }
                        }
                    }
                }
        }

        protected void GetLocalVisuals(List<IRenderable> result)
        {
            //Console.WriteLine(objects.ToString());
            if (objects != null)
                foreach (GameObject gameObj in objects)
                {
                    if (gameObj is IRenderable)
                    {
                        IRenderable renderable = gameObj as IRenderable;
                        Vector2d16 aMin = (gameObj).position - (renderable.GetVisualBB());
                        Vector2d16 aMax = (gameObj).position + (renderable.GetVisualBB());
                        //Vector2d16 bMin = Renderer.worldPosition - Renderer.Dimensions * 0.5;
                        //Vector2d16 bMax = Renderer.worldPosition + Renderer.Dimensions * 0.5;
                        Vector2d16 bMin = Renderer.worldPosition;
                        Vector2d16 bMax = Renderer.worldPosition + Renderer.Dimensions;
                        if (
                            (aMin._1 <= bMax._1 & aMax._1 >= bMin._1) &&
                            (aMin._2 <= bMax._2 & aMax._2 >= bMin._2)
                        )
                            result.Add(renderable);
                    }
                }
        }

        protected void AddToArray(GameObject obj)
        {
            Console.WriteLine(objects.Length);
            GameObject[] newArr = new GameObject[objects.Length+1];
            for (int i = 0; i < objects.Length; ++i)
                newArr[i] = objects[i];
            newArr[objects.Length] = obj;
            objects = newArr;
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
            //Console.WriteLine(position);
            dimensions /= 2;
            this.position = position;
            if(depth-- <= 0)
            {
                _1 = new Leaf();
                _2 = new Leaf();
                _3 = new Leaf();
                _4 = new Leaf();
            } 
            else
            {
                _1 = new Link(dimensions, new Vector2d16(position._1 - dimensions._1 / 2, position._2 - dimensions._2 / 2), depth);
                _2 = new Link(dimensions, new Vector2d16(position._1 + dimensions._1 / 2, position._2 - dimensions._2 / 2), depth);
                _3 = new Link(dimensions, new Vector2d16(position._1 + dimensions._1 / 2, position._2 + dimensions._2 / 2), depth);
                _4 = new Link(dimensions, new Vector2d16(position._1 - dimensions._1 / 2, position._2 + dimensions._2 / 2), depth);

                /*_1 = new Link(dimensions / 2, new Vector2d16(position._1 + dimensions._1 / 4, position._2 + dimensions._2 * 3 / 4), depth);
                _2 = new Link(dimensions / 2, new Vector2d16(position._1 + dimensions._1 * 3/ 4, position._2 + dimensions._2 * 3 / 4), depth);
                _3 = new Link(dimensions / 2, new Vector2d16(position._1 + dimensions._1 / 4, position._2 + dimensions._2 / 4), depth);
                _4 = new Link(dimensions / 2, new Vector2d16(position._1 + dimensions._1 * 3 / 4, position._2 + dimensions._2 / 4), depth);*/
            }

        }

        public override void Add(GameObject obj)
        {
            Vector2d16 bb = obj.GetPhysicAABB();

            if ((obj.position._1 - position._1 < bb._1 >> 1) || (obj.position._2 - position._2 < bb._2 >> 1))
            {
                AddToArray(obj);
                //Console.WriteLine(position);
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

        public override void GetCollisions(List<ICollidable> result, KinematicObject obj)
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

        public override void GetVisuals(List<IRenderable> result)
        {
            GetLocalVisuals(result);
            //Console.WriteLine(objects.Length);
            if (Renderer.Dimensions._1 < position._1)
                if (Renderer.Dimensions._2 > position._2)
                    _1.GetVisuals(result);
                else
                    _3.GetVisuals(result);
            else
                if (Renderer.Dimensions._2 > position._2)
                _2.GetVisuals(result);
            else
                _4.GetVisuals(result);
        }
    }

    class Leaf : Node
    {
        public Leaf()
        {
            //Console.WriteLine("Leaf");
        }
        public override void Add(GameObject obj)
        {
            AddToArray(obj);
        }

        public override void GetCollisions(List<ICollidable> result, KinematicObject obj)
        {
            GetLocalCollisions(result, obj);
        }

        public override void GetVisuals(List<IRenderable> result)
        {
            GetLocalVisuals(result);
        }
    }
}
