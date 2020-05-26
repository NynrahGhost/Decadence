using System;
using System.Collections.Generic;
using System.Text;

interface Atlas : IResource
{
    public int GetData(Vector2d32 position);
}

struct Atlas8 : Atlas
{
    byte[,] data;

    public Atlas8(string fileName)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(ResourceLoader.root + @"Textures\"+fileName);
        int width = BitConverter.ToInt32(bytes, 0);
        int height = BitConverter.ToInt32(bytes, 0);

        data = new byte[height,width];

        for (int i = 8; i < bytes.Length; ++i)
        {
            data[(i - 8) / width,(i - 8) % width] = bytes[i];
        }
    }

    public int GetData(Vector2d32 position)
    {
        return data[position._1, position._2];
    }
}

struct Atlas16 : Atlas
{
    char[,] data;

    public Atlas16(string fileName)
    {
        string[] lines = System.IO.File.ReadAllLines(ResourceLoader.root + fileName);

        data = new char[lines[0].Length, lines.Length];

        for (int height = 0; height < lines.Length; ++height)
        {
            for (int width = 0; width < lines[0].Length; ++width)
            {
                data[width, height] = lines[height][width];
            }
        }
    }

    public int GetData(Vector2d32 position)
    {
        return data[position._1, position._2];
    }
}