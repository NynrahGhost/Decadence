using System;

static class ArrayExtension
{
    public static string ToString(this Fragment8[,] frag)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int y = 0; y < frag.Length; ++y)
            for (int x = 0; x < frag.Length; ++x)
                sb.Append(frag[y,x]);
        return sb.ToString();
    }
}