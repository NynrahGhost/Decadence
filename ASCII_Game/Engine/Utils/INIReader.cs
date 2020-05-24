using System.IO;
using System.Collections.Generic;

class INIReader
{
    public Dictionary<string, string> Read(string path)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        StreamReader input = new StreamReader(path);

        char ch;
        while((ch = (char)input.Read()) != -1)
        {

        }
        return dictionary;
    }
}