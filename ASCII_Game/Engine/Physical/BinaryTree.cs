using System;
using System.Collections.Generic;
using System.Text;

class BinaryTree
{
    Node root;

    public BinaryTree(KinematicObject[] objects)
    {
        
    }

    public TactileObject[] GetCollisions(KinematicObject obj)
    {
        List<TactileObject> result = new List<TactileObject>();

        root.GetCollisions(result, obj);

        return result.ToArray();
    }

    abstract class Node
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
    }

    abstract class DividedNode : Node
    {
        public Node _1;
        public Node _2;
        public Vector2d16 position;
    }

    class HorizontalNode : DividedNode
    {
        public override void Add(GameObject obj)
        {
            if (obj.position._2 - position._2 < 10)
            {
                GameObject[] newArr = new GameObject[objects.Length];
                for (int i = 0; i < objects.Length; ++i)
                    newArr[i] = objects[i];
                newArr[objects.Length] = obj;
            }
            else
            {
                if (obj.position._2 > position._2)
                {
                    _2.Add(obj);
                }
                else
                {
                    _1.Add(obj);
                }
            }
        }

        public override void GetCollisions(List<TactileObject> result, KinematicObject obj)
        {
            GetLocalCollisions(result, obj);
            if(obj.position._2 > position._2)
            {
                _2.GetCollisions(result, obj);
            }
            else
            {
                _1.GetCollisions(result, obj);
            }
        }
    }

    class VerticalNode : DividedNode
    {
        public override void Add(GameObject obj)
        {
            if (obj.position._1 - position._1 < 10)
            {
                GameObject[] newArr = new GameObject[objects.Length];
                for (int i = 0; i < objects.Length; ++i)
                    newArr[i] = objects[i];
                newArr[objects.Length] = obj;
            }
            else
            {
                if (obj.position._1 > position._1)
                {
                    _2.Add(obj);
                }
                else
                {
                    _1.Add(obj);
                }
            }
        }

        public override void GetCollisions(List<TactileObject> result, KinematicObject obj)
        {
            GetLocalCollisions(result, obj);
            if (obj.position._1 > position._1)
            {
                _2.GetCollisions(result, obj);
            }
            else
            {
                _1.GetCollisions(result, obj);
            }
        }
    }

    class Leaf : Node
    {
        public override void Add(GameObject obj)
        {
            GameObject[] newArr = new GameObject[objects.Length];
            for (int i = 0; i < objects.Length; ++i)
                newArr[i] = objects[i];
            newArr[objects.Length] = obj;
        }

        public override void GetCollisions(List<TactileObject> result, KinematicObject obj)
        {
            GetLocalCollisions(result, obj);
        }
    }
}