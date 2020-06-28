using System;
using System.Collections.Generic;
using System.Text;

internal partial class Image
{
    /// <summary>
    /// Base class for text objects.
    /// </summary>
    public class Text : Rectangle
    {
        public Text(params string[] text)
        {
            int max = 0;
            foreach (string str in text)
                if (str.Length > max)
                    max = str.Length;

            dimensions = new Vector2d16(max, text.Length);
        }
    }

    /// <summary>
    /// Extended class for text objects.<br/><br/>
    /// Alignment can be set by putting /l, /r, /c at the beginning of the string.<br/><br/>
    /// Styles can be added by encapsulating text with tags.<br/>
    /// # - bold*<br/>
    /// | - faint*<br/>
    /// / - italic*<br/>
    /// _ - underlined<br/>
    /// ^ - blink*<br/>
    /// % - cross-out*<br/>
    /// ~ - video inverse<br/>
    /// @ - fraktur*<br/><br/>
    /// 
    /// <i>*Not supported in windows 10 console.</i>
    /// </summary>
    public class RichText : Rectangle
    {
        public RichText(params string[] text)
        {
            int max = 0;
            foreach (string str in text)
                if (str.Length > max)
                    max = str.Length;

            dimensions = new Vector2d16(max, text.Length);
        }
    }
}
