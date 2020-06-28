using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

/// <summary>
/// Utility class for reading and writing, and manipulating JSON objects.
/// </summary>
abstract class JSON
{
    public static Dictionary<string, object> Read(string path)
    {
        StreamReader input = new StreamReader(ResourceLoader.root + path);

        return AnalyzeObject(input);
    }

    private static Dictionary<string, object> AnalyzeObject(StreamReader input)
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        EState state = 0;
        StringBuilder key = new StringBuilder();

        char ch;
        input.Read();

        // Somehow loop consumes '}' after AnalyzeValue is returned, so this variable prevents this.
        bool beforeValue = true;

        while ((ch = (char)input.Peek()) != '}' & input.Peek() != -1)
        {
            //if (ch == '}')
                //Console.WriteLine("HERE");

            switch (ch)
            {
                case '\n':
                    break;
                case '\r':
                    break;
                case '\t':
                    break;
                case ' ':
                    break;
                case ',':
                    break;
                case '{':
                    dictionary.Add(key.ToString(), AnalyzeObject(input));
                    key.Clear();
                    state = EState.key;
                    break;
                case '[':
                    dictionary.Add(key.ToString(), AnalyzeArray(input));
                    key.Clear();
                    state = EState.key;
                    break;
                case ':':
                    state = EState.value;
                    break;
                default:
                    if (state == EState.key)
                    {
                        if(ch!='"')
                            key.Append(ch);
                    }
                    if (state == EState.value)
                    {
                        dictionary.Add(key.ToString(), AnalyzeValue(input));
                        beforeValue = false;
                        state = EState.key;
                        key.Clear();
                    }
                    break;
            }
            if (beforeValue)
                input.Read();
            else
                beforeValue = true;
        }
        return dictionary;
    }

    private static List<object> AnalyzeArray(StreamReader input)
    {
        List<object> array = new List<object>();
        char ch;
        input.Read();

        // Somehow loop consumes ']' after AnalyzeValue is returned, so this variable prevents this.
        bool beforeValue = true;

        while ((ch = (char)input.Peek()) != ']' & input.Peek() != -1)
        {
            switch (ch)
            {
                case '\n':
                    break;
                case '\r':
                    break;
                case '\t':
                    break;
                case ' ':
                    break;
                case '{':
                    array.Add(AnalyzeObject(input));
                    break;
                case '[':
                    array.Add(AnalyzeArray(input));
                    break;
                case ',':
                    break;
                default:
                    array.Add(AnalyzeValue(input));
                    beforeValue = false;
                    break;
            }
            if (beforeValue)
                input.Read();
            else
                beforeValue = true;
        }
        return array;
    }

    private static object AnalyzeValue(StreamReader input)
    {
        StringBuilder value = new StringBuilder();
        char ch;
        while (input.Peek() != ',' & input.Peek() != '}' & input.Peek() != ']' & input.Peek() != -1)
        {
            ch = (char)input.Read();
            switch (ch)
            {
                case '\n':
                    break;
                case '\r':
                    break;
                case '\t':
                    break;
                case ' ':
                    break;
                default:
                    value.Append(ch);
                    break;
            }
        }
        //Console.WriteLine(value);
        int num;
        if (int.TryParse(value.ToString(), out num))
            return num;
        if (value[0] == '#')
            return Convert.FromHexToColor8(value.ToString());
        if (value[0] == '"' & value[value.Length-1] == '"')
            return value.Remove(0, 1).Remove(value.Length - 1, 1).ToString();
        if (value.Equals("true"))
            return true;
        if (value.Equals("false"))
            return false;
        if (value.Equals("null"))
            return null;
        throw new JSONFormatException("Invalid value: " + value);
    }

    public static StringBuilder ToString(Dictionary<string, object> json)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{\n");
        foreach (var entry in json)
        {
            sb.Append("\t\""+entry.Key+"\": ");
            switch (entry.Value)
            {
                case Dictionary<string, object> d:
                    sb.Append(ToString(d));
                    break;
                case List<object> l:
                    sb.Append(ToString(l));
                    break;
                default:
                    sb.Append(ToString(entry.Value));
                    break;
            }
            sb.Append("\n\r");
        }
        sb.Append("}");
        return sb;
    }

    public static StringBuilder ToString(List<object> array)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (var entry in array)
        {
            sb.Append(' ');
            switch (entry)
            {
                case Dictionary<string, object> d:
                    sb.Append(ToString(d));
                    break;
                case List<object> l:
                    sb.Append(ToString(l));
                    break;
                case object o:
                    sb.Append(ToString(o));
                    break;
            }
        }
        sb.Append(" ]");
        return sb;
    }

    public static StringBuilder ToString(object obj)
    {
        StringBuilder sb = new StringBuilder();
        switch (obj)
        {
            case string s:
                sb.Append('"' + s + '"');
                //sb.Append(s);
                break;
            default:
                sb.Append(obj);
                break;
        }
        return sb;
    }

    public static Dictionary<string, object> ToJSON(object obj)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        
        dict.Add("TypeName", obj.GetType().Name);
        
        foreach (FieldInfo mi in obj.GetType().GetFields())
        {
            if (mi.GetValue(obj).GetType().IsPrimitive &&
                mi.GetValue(obj).GetType().IsArray
                )
                dict.Add(mi.Name, mi.GetValue(obj));
            else
                dict.Add(mi.Name, ToJSON(mi.GetValue(obj)));
        }

        return dict;
    }

    public static void Write(Dictionary<string, object> json, string path)
    {

    }

    enum EState
    {
        key, value
    }
}

/*
interface JSONComponent { }

class JSONObject : Dictionary<string, JSONComponent>, JSONComponent
{
    public static implicit operator JSONObject(Dictionary<string, object> v)
    {
        return new JSONObject();
    }
}

class JSONArray : List<JSONComponent> , JSONComponent
{

}

class JSONText : JSONComponent
{
    private readonly string _1;

    private JSONText(string text)
    {
        _1 = text;
    }

    public static implicit operator string(JSONText text)
    {
        return text._1;
    }

    public static implicit operator JSONText(string text)
    {
        return new JSONText(text);
    }
}

class JSONNumber
*/