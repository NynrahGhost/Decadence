class Map : QuadTree, IResource
{
    string name;

    public Map(string path)
    {
        
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