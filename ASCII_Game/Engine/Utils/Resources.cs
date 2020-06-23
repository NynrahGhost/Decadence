using System;
using System.Collections.Generic;

/// <summary>
/// Utility class for loading repetative or large data.
/// </summary>
abstract class ResourceLoader
{
    public static string root = GetRoot();
    //Environment.CurrentDirectory + '\\'; 
    //System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName+'\\';

    public static Dictionary<string, Atlas> atlasPool = new Dictionary<string, Atlas>();

    public static Image images;

    public static Shader shaders;

    private static string GetRoot()
    {
        string root;
#if DEBUG
        root = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + '\\';
#else
        root = Environment.CurrentDirectory + '\\';
#endif
        return root;
    }

    public static T LoadResource<T>(string path)
    {
        string[] splittedPath = path.Split('\\');

        switch (splittedPath[0])
        {
            case "Textures":
                return (T)(dynamic)LoadAtlas(path);
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
        atlasPool.TryGetValue(fileName, out atlas);
        if (atlas != null)
            return atlas;

        string[] splittedName = fileName.Split('.');

        switch (splittedName[1])
        {
            case "txt":
            case "bms":
                atlas = new Atlas16(fileName);
                break;
            case "png":
                atlas = new AtlasPNG(fileName);
                break;
            default:
                atlas = new Atlas8(fileName);
                break;
        }

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