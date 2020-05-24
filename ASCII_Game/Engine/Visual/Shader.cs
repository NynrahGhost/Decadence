using System.Text;

abstract class Shader
{
    public abstract Fragment8 Compute(
        Vector2d16 current,
        Vector2d16 start,
        Vector2d16 end
        );
}

namespace Shaders
{
    class Plain : Shader
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

    class Gradient : Shader
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

    class PolyGradient
    {

    }

    class Texture : Shader
    {
        Atlas atlas;
        Vector2d32 start;
        Vector2d32 end;

        public Texture(Atlas atlas, Vector2d32 start, Vector2d32 end)
        {
            this.atlas = atlas;
            this.start = start;
            this.end = end;
        }

        public override Fragment8 Compute(Vector2d16 current, Vector2d16 start, Vector2d16 end)
        {
            return new Fragment8( new Color8fg(0), new Color8bg(0), (char) atlas.GetData(this.start - start + current));
        }
    }
}
