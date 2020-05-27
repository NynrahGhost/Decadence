class Map : QuadTree, IResource
{
    public Map(string path)
    {
        
    }

    public void Save(string path)
    {
        //System.Text.Json.Json
        System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Create);
        
        //stream.Write();
    }
}