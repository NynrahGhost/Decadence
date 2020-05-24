using System.Collections.Generic;
using System;
using System.Text;
using System.IO;

class Renderer
{
    private GameObject[] objects;
    public Vector2d16 center = new Vector2d16(0, 0);

    public Renderer()
    {

    }

    public void SetObjects(List<GameObject> objects)
    {
        objects.Sort((x, y) => ((IRenderable)x).Image.zIndex.CompareTo(((IRenderable)y).Image.zIndex));
        this.objects = objects.ToArray();
    }

    public void Render()
    {
        var inputStream = Console.OpenStandardOutput();

        var writer = new StreamWriter(inputStream);
        //BufferedStream w = new BufferedStream(writer);

        foreach (GameObject obj in objects)
        {
            writer.Write(ANSII.CursorPosition(obj.position - center));
            writer.Write(((IRenderable)obj).Render());
        }

        writer.Flush();

        /*
        Console.Clear();
        StringBuilder sb = new StringBuilder();

        foreach (GameObject obj in objects)
        {
            sb.Append(ANSII.CursorPosition(obj.position-center));
            sb.Append(((IRenderable)obj).Render());
        }
        Console.Write(sb);
        sb.Clear();
        */

        /*
        Console.SetCursorPosition(0, 0);
        //Console.Clear();

        System.IO.TextWriter tw = new System.IO.StreamWriter(new System.IO.MemoryStream(1000));

        //tw.Write("\u001b0J");

        foreach (GameObject obj in objects)
        {
            SetPosition(obj.position);
            //Console.Write(((IRenderable)obj).Render());
            tw.Write(((IRenderable)obj).Render());
        }

        //tw.Close();
        Console.Write
        //tw.Flush();
        //Console.OpenStandardOutput() = tw;
        Console.SetOut(tw);
        tw.Flush();
        */
    }

    public void SetPosition(Vector2d16 position)
    {
        position -= center;
        Console.SetCursorPosition(position._1, position._2);
    }
}