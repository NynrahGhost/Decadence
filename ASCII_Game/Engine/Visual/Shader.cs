using System.Collections.Generic;

abstract class Shader
{
    public abstract Fragment8 Compute(
        Vector2d16 current,
        Vector2d16 start,
        Vector2d16 end
        );

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

    public class PolyGradient
    {

    }

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

    public class Text : Shader
    {
        protected string[] text;

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
            return new Fragment8(Color8fg.GetNull(), Color8bg.GetNull(), text[current._2][current._1]);
        }
    }

    public class RichText : Text
    {
        public Color8fg foreground;

        public RichText(string text, Color8fg foreground) : base(text)
        {
            this.foreground = foreground;
        }

        public RichText(string[] text, Color8fg foreground) : base(text)
        {
            this.foreground = foreground;
        }

        public override Fragment8 Compute(Vector2d16 current, Vector2d16 start, Vector2d16 end)
        {
            if (text.Length <= current._2 || text[current._2].Length <= current._1)
                return Fragment8.GetNull();
            return new Fragment8(foreground, Color8bg.GetNull(), text[current._2][current._1]);
        }
    }
}
