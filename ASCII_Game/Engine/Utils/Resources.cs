using System;
using System.Collections.Generic;
using System.Text;

abstract class ResourceLoader
{
    public static Dictionary<uint, string> stringPool = new Dictionary<uint, string>();

    public static Dictionary<string, Atlas> symbolTextures = new Dictionary<string, Atlas>();
    public static Dictionary<string, Atlas> foregroundTextures = new Dictionary<string, Atlas>();
    public static Dictionary<string, Atlas> backgroundTextures = new Dictionary<string, Atlas>();

    public IResource LoadResource(string path)
    {
        return null;
    }
}

interface IResource
{
    public static string notLoaded = "Resource not loaded";
}

struct String : IResource
{
    private uint id;

    private String(uint id)
    {
        this.id = id;
    }

    public static implicit operator String(string str)
    {
        if (ResourceLoader.stringPool.ContainsValue(str))
        {
            string tmp;
            foreach (uint key in ResourceLoader.stringPool.Keys)
            {
                ResourceLoader.stringPool.TryGetValue(key, out tmp);
                if (tmp.Equals(str))
                    return new String(key);
            }
        }
        for (uint key = 0; key < uint.MaxValue - 1; ++key)
            if (!ResourceLoader.stringPool.ContainsKey(key))
            {
                ResourceLoader.stringPool.Add(key, str);
                return new String(key);
            }
        throw new ResourceException("String pool is full!");
    }

    public static implicit operator string(String str)
    {
        string res;
        if (ResourceLoader.stringPool.TryGetValue(str.id, out res))
        {
            return res;
        }
        else
        {
            return IResource.notLoaded;
        }
    }
}