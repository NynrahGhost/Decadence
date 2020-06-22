using System.Collections.Generic;

/// <summary>
/// Class used to describe object's symbol and color in specific position.<br/>
/// Use <see cref="Compute(Vector2d16, Vector2d16, Vector2d16)">Compute(Vector2d16, Vector2d16, Vector2d16)</see> to get
/// <see cref="Fragment8">Fragment8</see> of an object at specific position.
/// </summary>
abstract class Shader
{
    /// <summary>
    /// Calculates output symbol with it's color and background.
    /// </summary>
    /// <param name="current">Current processed point of an image.</param>
    /// <param name="start">The upper-left-most point of the image.</param>
    /// <param name="end">The lower-right-most point of the image.</param>
    /// <returns>Resulting fragment</returns>
    public abstract Fragment8 Compute(
        Vector2d16 current,
        Vector2d16 start,
        Vector2d16 end
        );

    /// <summary>
    /// Shader that fills an image with one symbol and corresponding foreground and background color.
    /// </summary>
    public class Plain : Shader
    {
        Color8fg foreground;
        Color8bg background;
        char symbol;

        public Plain(
            Color8fg foreground,
            Color8bg background,
            char symbol
        )
        {
            this.foreground = foreground;
            this.background = background;
            this.symbol = symbol;
        }

        public override Fragment8 Compute(
            Vector2d16 current,
            Vector2d16 start,
            Vector2d16 end
        )
        {
            return new Fragment8(foreground, background, symbol);
        }
    }

    /// <summary>
    /// Shader that fills an image with two colors, with one gradually morphing into another.
    /// </summary>
    public class Gradient : Shader
    {
        Color8fg foregroundStart;
        Color8fg foregroundEnd;
        Color8bg backgroundStart;
        Color8bg backgroundEnd;
        char symbol;

        public Gradient (
            Color8fg foregroundStart,
            Color8fg foregroundEnd,
            Color8bg backgroundStart,
            Color8bg backgroundEnd,
            char symbol
        )
        {
            this.foregroundStart = foregroundStart;
            this.foregroundEnd = foregroundEnd;
            this.backgroundStart = backgroundStart;
            this.backgroundEnd = backgroundEnd;
            this.symbol = symbol;
        }

        public override Fragment8 Compute(
            Vector2d16 current,
            Vector2d16 start,
            Vector2d16 end
        )
        {
            double lerp = (double)(current._1 - start._1) / (double)(end._1 - start._1);
            return new Fragment8(
                new Color8fg(
                    (byte)(foregroundStart.GetRed() - (foregroundStart.GetRed() - foregroundEnd.GetRed()) * lerp),
                    (byte)(foregroundStart.GetGreen() - (foregroundStart.GetGreen() - foregroundEnd.GetGreen()) * lerp),
                    (byte)(foregroundStart.GetBlue() - (foregroundStart.GetBlue() - foregroundEnd.GetBlue()) * lerp)
                    ),
                new Color8bg(
                    (byte)(backgroundStart.GetRed() - (backgroundStart.GetRed() - backgroundEnd.GetRed()) * lerp),
                    (byte)(backgroundStart.GetGreen() - (backgroundStart.GetGreen() - backgroundEnd.GetGreen()) * lerp),
                    (byte)(backgroundStart.GetBlue() - (backgroundStart.GetBlue() - backgroundEnd.GetBlue()) * lerp)
                    ),
                symbol);
        }
    }

    /// <summary>
    /// Shader that fills an image with multiple colors, each gradually morphing into the next one.
    /// </summary>
    public class PolyGradient
    {

    }

    /// <summary>
    /// Shader that references symbols from a text file.
    /// </summary>
    public class TextureSymbol : Shader
    {
        protected Atlas atlas;
        protected Vector2d32 start;
        protected Vector2d32 end;

        public TextureSymbol(Atlas atlas, Vector2d32 start, Vector2d32 end)
        {
            this.atlas = atlas;
            this.start = start;
            this.end = end;
        }

        public override Fragment8 Compute(Vector2d16 current, Vector2d16 start, Vector2d16 end)
        {
            return new Fragment8( new Color8fg(255,255,255), new Color8bg(0), (char)atlas.GetData(this.start - start + current));
        }
    }

    /// <summary>
    /// Shader that references background data from an image.
    /// </summary>
    public class TextureBackground : Shader
    {
        Atlas atlas;
        Vector2d32 start;
        Vector2d32 end;

        public TextureBackground(Atlas atlas, Vector2d32 start, Vector2d32 end)
        {
            this.atlas = atlas;
            this.start = start;
            this.end = end;
        }

        public override Fragment8 Compute(Vector2d16 current, Vector2d16 start, Vector2d16 end)
        {
            System.Drawing.Color color = System.Drawing.Color.FromArgb(atlas.GetData(this.start - start + current));
            return new Fragment8(new Color8fg(0), new Color8bg(color.R, color.G, color.B), ' ');
        }
    }

    /// <summary>
    /// Shader that renders colored symbols.
    /// </summary>
    public class TextureColoredSymbol : TextureSymbol
    {
        Atlas atlasColor;
        Vector2d32 startColor;
        Vector2d32 endColor;

        public TextureColoredSymbol(Atlas atlas, Vector2d32 start, Vector2d32 end, Atlas atlasColor, Vector2d32 startColor, Vector2d32 endColor) : base(atlas, start, end)
        {
            this.atlasColor = atlasColor;
            this.startColor = startColor;
            this.endColor = endColor;
        }

        public override Fragment8 Compute(Vector2d16 current, Vector2d16 start, Vector2d16 end)
        {
            System.Drawing.Color color = System.Drawing.Color.FromArgb(atlas.GetData(this.start - start + current));
            return new Fragment8(new Color8fg(color.R, color.G, color.B), new Color8bg(0), (char)atlas.GetData(this.start - start + current));
        }
    }

    /// <summary>
    /// Base class for text objects.
    /// </summary>
    public class Text : Shader
    {
        protected string[] text;

        protected Text() { }

        public Text(string text)
        {
            this.text = text.Split('\n');
        }

        public Text(string[] text)
        {
            this.text = text;
        }

        public void SetText(string text)
        {
            this.text = text.Split('\n');
        }

        public void SetText(string[] text)
        {
            this.text = text;
        }

        public string GetText()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach(string str in text)
            {
                sb.Append(str);
                sb.Append('\n');
            }
            return sb.ToString();
        }

        public override Fragment8 Compute(Vector2d16 current, Vector2d16 start, Vector2d16 end)
        {
            if (text.Length <= current._2 || text[current._2].Length <= current._1)
                return Fragment8.GetNull();
            return new Fragment8(new Color8fg(255,255,255), Color8bg.GetNull(), text[current._2][current._1]);
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
    public class RichText : Text
    {
        public Color8fg foreground;

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
        public RichText(string text, Color8fg foreground)
        {
            int max = text.Length;
            if (text[0] == '/')
            {
                switch (text[1])
                {
                    case 'c':
                    case 'r':
                    case 'l':
                        this.text = new string[] { text.Substring(2).Trim() };
                        break;
                }
            }
            else
            {
                this.text = new string[] { text };
            }
            this.foreground = foreground;
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
        public RichText(string[] text, Color8fg foreground)
        {
            int max = 0;
            foreach (string str in text)
                if (str.Length > max)
                    max = str.Length;
            for(int i = 0; i < text.Length; ++i)
                if (text[i][0] == '/')
                {
                    switch (text[i][1])
                    {
                        case 'c':
                            text[i] = text[i].Substring(2).Trim();
                            text[i] = text[i].PadLeft((max - text[i].Length)/2 + text[i].Length).PadRight((max - text[i].Length) / 2 + text[i].Length);
                            break;
                        case 'r':
                            text[i] = text[i].Substring(2).Trim();
                            text[i] = text[i].PadLeft(max);
                            break;
                        case 'l':
                            text[i] = text[i].Substring(2).Trim();
                            text[i] = text[i].PadRight(max);
                            break;
                    }
                }
            this.text = text;
            this.foreground = foreground;
        }

        public override Fragment8 Compute(Vector2d16 current, Vector2d16 start, Vector2d16 end)
        {
            if (text.Length <= current._2 || text[current._2].Length <= current._1)
                return Fragment8.GetNull();
            switch (text[current._2][current._1])
            {
                case '#': //Bold
                    return new Fragment8(foreground, Color8bg.GetNull(), (char)0xDB80);
                case '|': //Faint
                    return new Fragment8(foreground, Color8bg.GetNull(), (char)0xDB81);
                case '/': //Italic
                    return new Fragment8(foreground, Color8bg.GetNull(), (char)0xDB82);
                case '_': //Underlined
                    return new Fragment8(foreground, Color8bg.GetNull(), (char)0xDB83);
                case '^': //Blink
                    return new Fragment8(foreground, Color8bg.GetNull(), (char)0xDB84);
                case '%': //Crossed-out
                    return new Fragment8(foreground, Color8bg.GetNull(), (char)0xDB85);
                case '~': //Video inverse
                    return new Fragment8(foreground, Color8bg.GetNull(), (char)0xDB86);
                case '@': //Fraktur
                    return new Fragment8(foreground, Color8bg.GetNull(), (char)0xDB87);
                default:
                    return new Fragment8(foreground, Color8bg.GetNull(), text[current._2][current._1]);
            }
        }
    }
}
