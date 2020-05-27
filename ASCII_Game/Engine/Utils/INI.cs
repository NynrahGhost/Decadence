using System.IO;
using System.Collections.Generic;
using System.Text;

abstract class INI
{
    public static Dictionary<string, string> Read(string path)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        StreamReader input = new StreamReader(ResourceLoader.root + path);

        EState state = 0;
        StringBuilder key = new StringBuilder();
        StringBuilder value = new StringBuilder();
        char ch;
        while(input.Peek() != -1)
        {
            ch = (char)input.Read();
            if (state == EState.comment)
                if(ch == '\n')
                {
                    state = EState.key;
                    continue;
                }
                else
                {
                    continue;
                }
            switch (ch)
            {
                case ' ':
                    break;
                case '\r':
                    break;
                case '\t':
                    break;
                case '#':
                    state = EState.comment;
                    break;
                case '=':
                    if (state == EState.key)
                        state = EState.value;
                    else
                        value.Append('=');
                    break;
                case '[':
                    if(state == EState.key)
                    {
                        state = EState.header;
                    }
                    break;
                case ']':
                    if (state == EState.header)
                    {
                        state = EState.afterheader;
                    }
                    break;
                case '\n':
                    if (state == EState.key)
                        throw new INIFormatException("Key has no corresponding value");
                    if (state == EState.value)
                        state = EState.key;
                    if (state == EState.afterheader)
                        state = EState.key;
                    if (key.Length != 0)
                    {
                        dictionary.Add(key.ToString(), value.ToString());
                        key.Clear();
                        value.Clear();
                    }
                    break;
                default :
                    if (state == EState.comment)
                        continue;
                    if (state == EState.value)
                        value.Append(ch);
                    if (state == EState.key)
                        key.Append(ch);
                    break;
            }
        }
        input.Close();

        if (key.Length != 0)
        {
            dictionary.Add(key.ToString(), value.ToString());
            key.Clear();
            value.Clear();
        }

        return dictionary;
    }

    public static void Write(string path, Dictionary<string, string> dict)
    {
        StringBuilder sb = new StringBuilder();
        foreach (KeyValuePair<string, string> entry in dict)
        {
            if (entry.Value.StartsWith('[') || entry.Key.StartsWith('#'))
                sb.Append(entry.Value);
            else
                sb.Append(entry.Key+"= "+entry.Value);
            sb.Append('\n');
        }

        StreamWriter input = new StreamWriter(ResourceLoader.root + path);
        input.Write(sb);
        input.Close();
    }

    enum EState
    {
        key, value, header, afterheader, comment
    }
}