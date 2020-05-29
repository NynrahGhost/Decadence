using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

    public static string ToString(Dictionary<string, object> json)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var entry in json)
        {
            if (entry.Value.GetType() == json.GetType())
                foreach (var entry2 in ((System.Collections.Generic.Dictionary<string, object>)entry.Value))
                    sb.Append('\t' + entry2.Key + ": " + entry2.Value);
            else if (entry.Value.GetType() == (new System.Collections.Generic.List<object>()).GetType())
            {
                sb.Append(entry.Key + ": ");
                foreach (var entry3 in ((System.Collections.Generic.List<object>)entry.Value))
                    sb.Append(entry3 + " ");
            }
            else
                sb.Append(entry.Key + ": " + entry.Value);
            sb.Append('\n');
        }
        
        return sb.ToString();
    }

    class JSONObject : Dictionary<string, object>
    {
        
    }

    enum EState
    {
        key, value
    }
}