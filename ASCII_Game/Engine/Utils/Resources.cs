using System;
using System.Collections.Generic;

abstract class ResourceLoader
{
    public static string root = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName+'\\';

    public static Dictionary<string, Atlas> atlasPool = new Dictionary<string, Atlas>();

    public static Image images;

    public static Shader shaders;

    public static T LoadResource<T>(string path)
    {
        string[] splittedPath = path.Split('\\');

        switch (splittedPath[0])
        {
            case "Textures":
                return (T)LoadAtlas(path);
            case "Maps":
                return (T)LoadMap(path);
        }

        return default(T);
    }

    public static IResource LoadResource(string path)
    {
        return LoadResource<IResource>(path);
    }

    public static IResource LoadAtlas(string fileName)
    {
        Atlas atlas;
        ResourceLoader.atlasPool.TryGetValue(fileName, out atlas);
        if (atlas != null)
            return atlas;

        string[] splittedName = fileName.Split('.');

        if (splittedName[1].Equals("bms"))
            atlas = new Atlas16(fileName);
        else
            atlas = new Atlas8(fileName);

        atlasPool.Add(fileName, atlas);

        return atlas;
    }

    public static IResource LoadMap(string fileName)
    {
        
        return null;
    }
}
 
interface IResource
{
    public static string notLoaded = "Resource not loaded";
}