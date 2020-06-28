using System.Collections.Generic;
/// <summary>
/// Class that contains information about the map. Extends <see cref="QuadTree">QuadTree</see>
/// </summary>
class Map : QuadTree, IResource
{
    public string name;

    public KinematicObject entities;
    public KinematicObject player;

    public Image[] images;
    public Shader[] shaders;
    public Shape[] shapes;

    public Map(string path)
    {
        Dictionary<string, object> map = JSON.Read(path);
        object result = null;

        map.TryGetValue("n", out result);
        name = (string)result;

        map.TryGetValue("p", out result);
        List<object> parameters = (List<object>)result;
        List<object> position = (List<object>)parameters[0];
        short depth = (short)(int)parameters[1];

        dimensions = ((short)(int)position[0], (short)(int)position[1]);
        if (depth == 0)
        {
            root = new Leaf();
            return;
        }
        root = new Link(dimensions, dimensions / 2, depth);



        map.TryGetValue("i", out result);
        List<Image> images = new List<Image>();
        foreach (List<object> obj in ((List<object>)result))
        {
            images.Add(Image.FromJSON(obj));
        }
        this.images = images.ToArray();

        map.TryGetValue("s", out result);
        List<Shader> shaders = new List<Shader>();
        foreach (List<object> obj in ((List<object>)result))
        {
            shaders.Add(Shader.FromJSON(obj));
        }
        this.shaders = shaders.ToArray();

        map.TryGetValue("g", out result);
        List<Shape> shapes = new List<Shape>();
        foreach (List<object> obj in ((List<object>)result))
        {
            shapes.Add(Shape.FromJSON(obj));
        }
        this.shapes = shapes.ToArray();

        map.TryGetValue("o", out result);
        List<GameObject> objects = new List<GameObject>();
        foreach (List<object> obj in ((List<object>)result))
        {
            objects.Add(GameObject.FromJSON(obj));
        }
        FillTree(objects.ToArray());

        /*map.TryGetValue("e", out result);
        List<KinematicObject> entities = new List<KinematicObject>();
        foreach (object[] obj in ((object[])result))
        {
            entities.Add(KinematicObject.FromJSON(obj));
        }*/
    }

    public Map(string name, Vector2d16 dimensions, int depth, GameObject[] objects) : base(dimensions, depth, objects)
    {
        this.name = name;
    }

    public void Save(string path)
    {
        //System.Text.Json.Json
        System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Create);
        
        //stream.Write();
    }
}