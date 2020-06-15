using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

/// <summary>
/// Base interface for Atlas resource.<br/>
/// Represents 2d data structure.
/// </summary>
interface Atlas : IResource
{
    public int GetData(Vector2d32 position);
}

/// <summary>
/// Texture structure for reading 2d bitmap.
/// </summary>
struct Atlas8 : Atlas
{
    byte[,] data;

    public Atlas8(string fileName)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(ResourceLoader.root + @"Textures\" + fileName);
        int width = BitConverter.ToInt32(bytes, 0);
        int height = BitConverter.ToInt32(bytes, 4);

        data = new byte[height,width];

        for (int i = 8; i < bytes.Length; ++i)
        {
            data[(i - 8) / width,(i - 8) % width] = bytes[i];
        }
    }

    public int GetData(Vector2d32 position)
    {
        return data[position._2, position._1];
    }
}

/// <summary>
/// Texture structure for reading text files.
/// </summary>
struct Atlas16 : Atlas
{
    string[] data;

    public Atlas16(string fileName)
    {
        data = System.IO.File.ReadAllLines(ResourceLoader.root + fileName);
        /*
        data = new char[lines[0].Length, lines.Length];

        for (int height = 0; height < lines.Length; ++height)
        {
            for (int width = 0; width < lines[0].Length; ++width)
            {
                data[width, height] = lines[height][width];
            }
        }*/
    }

    public int GetData(Vector2d32 position)
    {
        if(data[position._2].Length > position._1)
            return data[position._2][position._1];
        return ' ';
    }
}

/// <summary>
/// Texture structure for reading png images.<br/>
/// Each pixel represents two symbols, so it would look like squere.
/// </summary>
struct AtlasPNG : Atlas
{
    Bitmap bitmap;

    public AtlasPNG(string fileName)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(ResourceLoader.root + fileName);
        bitmap = new Bitmap(image);
    }

    public AtlasPNG(Bitmap bitmap)
    {
        this.bitmap = bitmap;
    }

    public static implicit operator AtlasPNG1x1(AtlasPNG atlas) => new AtlasPNG1x1(atlas.bitmap);

    public int GetData(Vector2d32 position)
    {
        return bitmap.GetPixel(position._1 >> 1 % bitmap.Width, position._2 % bitmap.Height).ToArgb();
    }
}

/// <summary>
/// Texture structure for reading png images.<br/>
/// Each pixel represents one symbol, instead of two.
/// </summary>
struct AtlasPNG1x1 : Atlas
{
    Bitmap bitmap;

    public AtlasPNG1x1(string fileName)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(ResourceLoader.root + fileName);
        bitmap = new Bitmap(image);
    }

    public AtlasPNG1x1(Bitmap bitmap)
    {
        this.bitmap = bitmap;
    }

    public static implicit operator AtlasPNG(AtlasPNG1x1 atlas) => new AtlasPNG(atlas.bitmap);

    public int GetData(Vector2d32 position)
    {
        return bitmap.GetPixel(position._1 % bitmap.Width, position._2 % bitmap.Height).ToArgb();
    }

}