using System;
using System.Collections.Generic;
using System.Text;

internal partial class Image
{
    /// <summary>
    /// Base class for GUI bars.<br/>
    /// Includes scroll bars and progress bars.
    /// </summary>
    public abstract class Bar : Image
    {
        public short length;
        public short progress;

        public byte endU;
        public byte endL;
        public byte mid;
        public byte frame;

        public Color8 endUfg = new Color8(255, 255, 255);
        public Color8 endLfg = new Color8(255, 255, 255);
        public Color8 midfg = new Color8(255, 255, 255);
        public Color8 framefg = new Color8(255, 255, 255);

        public Bar(short length, byte zIndex = 0, float percentage = 0)
        {
            this.length = length;
            SetProgressPercentage(percentage);
        }

        public abstract void SetProgressPercentage(float percentage);
    }

    /// <summary>
    /// Vertical scroll bar.
    /// </summary>
    public class ScrollBarV : Bar
    {
        public ScrollBarV(short length, byte zIndex = 127, float percentage = 0) : base(length, zIndex, percentage) { }

        private static string[,] endUpper = new string[,]
        {
            { "╔═╗",
              "║▲║",
              "╚╦╝" },
            { "┏═┓",
              "║▲║",
              "┗╦┛" },
            { "╭═╮",
              "║▲║",
              "╰╦╯" },
            { "╔═╗",
              "║▲║",
              "╠═╣" },
            { " ▄ ",
              " ▲ ",
              " █ " }
        };
        private static string[,] endLower = new string[,]
        {
            { "╔╩╗",
              "║▼║",
              "╚═╝" },
            { "┏╩┓",
              "║▼║",
              "┗═┛" },
            { "╭╩╮",
              "║▼║",
              "╰═╯" },
            { "╠═╣",
              "║▼║",
              "╚═╝" },
            { " █ ",
              " ▼ ",
              " ▀ " }
        };
        private static string[,] middle = new string[,]
        {
            { "┏╨┓",
              "┃ ┃",
              "┗╥┛" },
            { "╱╩╲",
              "║♦║",
              "╲╦╱" },
            { "╭╩╮",
              "║♦║",
              "╰╦╯" },
            { "╞═╡",
              "║♦║",
              "╞═╡" },
            { " ▄ ",
              " █ ",
              " ▀ " }
        };
        private static string[] body = new string[]
        {
            " ║ ",
            "│ │",
            " █ "
        };

        public override void SetProgressPercentage(float percentage)
        {
            if (percentage >= 1)
                progress = (short)(length - 6);
            else if (percentage <= 0)
                progress = 3;
            else
                progress = (short)((length - 9) * percentage + 3);
        }

        /// <summary>
        /// Sets specific preset to progress bar<br/>
        /// 0 - square, 1 - sci-fi, 2 - rounded, 3 - wide square, 4 - modern
        /// </summary>
        /// <param name="preset">0 - square, 1 - sci-fi, 2 - rounded, 3 - wide square, 4 - modern</param>
        /// <returns></returns>
        public ScrollBarV SetPreset(byte preset)
        {
            switch (preset)
            {
                case 0:
                    endU = 0;
                    endL = 0;
                    mid = 0;
                    frame = 0;
                    break;
                case 1:
                    endU = 1;
                    endL = 1;
                    mid = 1;
                    frame = 0;
                    break;
                case 2:
                    endU = 2;
                    endL = 2;
                    mid = 2;
                    frame = 0;
                    break;
                case 3:
                    endU = 3;
                    endL = 3;
                    mid = 3;
                    frame = 1;
                    break;
                case 4:
                    endU = 4;
                    endL = 4;
                    mid = 4;
                    frame = 2;
                    break;
            }
            return this;
        }

        public override Vector2d16 GetVisualBB()
        {
            return new Vector2d16((short)3, length);
        }

        public override void Render(Shader shader, Vector2d16 position)
        {
            int posX;
            int posY;

            for (int y = 3; y < length - 3; ++y)
                for (int x = 0; x < 3; ++x)
                {
                    posX = position._1 - Renderer.worldPosition._1 + x;
                    posY = position._2 - Renderer.worldPosition._2 + y;
                    if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                        continue;
                    Renderer.buffer[posY, posX] = new Fragment8(framefg, new Color8((byte)0, (byte)0, (byte)0), body[frame][x]);

                }

            for (int y = 0; y < 3; ++y)
                for (int x = 0; x < 3; ++x)
                {
                    posX = position._1 - Renderer.worldPosition._1 + x;
                    posY = position._2 - Renderer.worldPosition._2 + y;
                    if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                        continue;
                    Renderer.buffer[posY, posX] = new Fragment8(endUfg, new Color8((byte)0, (byte)0, (byte)0), endUpper[endU, y][x]);
                }

            for (int y = 0; y < 3; ++y)
                for (int x = 0; x < 3; ++x)
                {
                    posX = position._1 - Renderer.worldPosition._1 + x;
                    posY = position._2 - Renderer.worldPosition._2 + y + progress;
                    if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                        continue;
                    Renderer.buffer[posY, posX] = new Fragment8(midfg, new Color8((byte)0, (byte)0, (byte)0), middle[mid, y][x]);
                }

            for (int y = 0; y < 3; ++y)
                for (int x = 0; x < 3; ++x)
                {
                    posX = position._1 - Renderer.worldPosition._1 + x;
                    posY = position._2 - Renderer.worldPosition._2 + y + length - 3;
                    if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                        continue;
                    Renderer.buffer[posY, posX] = new Fragment8(endLfg, new Color8((byte)0, (byte)0, (byte)0), endLower[endL, y][x]);
                }
        }
    }

    /// <summary>
    /// Horizontal scroll bar.
    /// </summary>
    public class ScrollBarH : Bar
    {
        public ScrollBarH(short length, byte zIndex = 127, float percentage = 127) : base(length, zIndex, percentage) { }

        private static string[,] endUpper = new string[,]
        {
            { "╔════╗",
              "╚════╝"},
            { "╔─══─╗",
              "╚─══─╝" },
            { "╔════╦",
              "╚════╩"},
            { "╔─══─╦",
              "╚─══─╩" },
            { "╭╼━━╾╮",
              "╰╼━━╾╯" },
            { "▄▄▄▄▄ ",
              "▀▀▀▀▀ " },
            { "▄█▄▄ ▄",
              "▀ ▀▀█▀" }
        };
        private static string[,] endLower = new string[,]
        {
            { "╔════╗",
              "╚════╝"},
            { "╔─══─╗",
              "╚─══─╝" },
            { "╦════╗",
              "╩════╝"},
            { "╦─══─╗",
              "╩─══─╝" },
            { "╭╼━━╾╮",
              "╰╼━━╾╯" },
            { " ▄▄▄▄▄",
              " ▀▀▀▀▀" },
            { "▄█▄▄ ▄",
              "▀ ▀▀█▀" }
        };
        private static string[,] middle = new string[,]
        {
            { "╦════╦",
              "╩════╩"},
            { "╦─╤╤─╦",
              "╩─╧╧─╩" },
            { "╦═╮╭═╦",
              "╩═╯╰═╩"},
            { "╭═╦╦═╮",
              "╰═╩╩═╯" },
            { "╭╼┯┯╾╮",
              "╰╼┷┷╾╯" },
            { " ▄▄▄▄ ",
              " ▀▀▀▀ " }
        };
        private static string[,] body = new string[,]
        {
            { "__",
              "‾‾" },
            { "╼╾",
              "╼╾" },
            { "──",
              "──" },
            { "━━",
              "━━" },
            { "══",
              "══" },
            { "═─═",
              "═─═" },
            { "╭╮",
              "╰╯" },
            { "╭─╮",
              "╰─╯" },
            { "╦═╦",
              "╩═╩" },
            { "▄▄",
              "▀▀" },
            { "▄ ▄",
              "▀ ▀" },
            { " ▄ ",
              " ▀ " }
        };

        public override void SetProgressPercentage(float percentage)
        {
            if (percentage >= 1)
                progress = (short)(length - 6);
            else if (percentage <= 0)
                progress = 6;
            else
                progress = (short)((length - 12) * percentage + 6);
        }

        /// <summary>
        /// Sets specific preset to progress bar<br/>
        /// 0 - square, 1 - sci-fi, 2 - rounded, 3 - modern, 4 - wide square, 5 - wide sci-fi, 6 - wide rounded,<br/>
        /// 7 - circled rouded 1, 8 - circled rouded 2, 9 - disjunction, 10 - alternate modern,
        /// </summary>
        /// <param name="preset"></param>
        /// <returns></returns>
        public ScrollBarH SetPreset(byte preset)
        {
            switch (preset)
            {
                case 0:
                    endU = 0;
                    endL = 0;
                    mid = 0;
                    frame = 0;
                    break;
                case 1:
                    endU = 1;
                    endL = 1;
                    mid = 1;
                    frame = 0;
                    break;
                case 2:
                    endU = 4;
                    endL = 4;
                    mid = 4;
                    frame = 0;
                    break;
                case 3:
                    endU = 5;
                    endL = 5;
                    mid = 5;
                    frame = 9;
                    break;
                case 4:
                    endU = 2;
                    endL = 2;
                    mid = 0;
                    frame = 2;
                    break;
                case 5:
                    endU = 3;
                    endL = 3;
                    mid = 1;
                    frame = 2;
                    break;
                case 6:
                    endU = 4;
                    endL = 4;
                    mid = 4;
                    frame = 2;
                    break;
                case 7:
                    endU = 4;
                    endL = 4;
                    mid = 4;
                    frame = 7;
                    break;
                case 8:
                    endU = 4;
                    endL = 4;
                    mid = 4;
                    frame = 6;
                    break;
                case 9:
                    endU = 2;
                    endL = 2;
                    mid = 2;
                    frame = 2;
                    break;
                case 10:
                    endU = 6;
                    endL = 6;
                    mid = 5;
                    frame = 9;
                    break;
            }
            return this;
        }

        public override Vector2d16 GetVisualBB()
        {
            return new Vector2d16(length, (short)2);
        }

        public override void Render(Shader shader, Vector2d16 position)
        {
            int posX;
            int posY;

            for (int i = 6; i < length; i += body[frame, 0].Length)
                for (int x = 0; x < body[frame, 0].Length; ++x)
                    for (int y = 0; y < 2; ++y)
                    {
                        posX = position._1 - Renderer.worldPosition._1 + i + x;
                        posY = position._2 - Renderer.worldPosition._2 + y;
                        if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                            continue;
                        Renderer.buffer[posY, posX] = new Fragment8(framefg, new Color8((byte)0, (byte)0, (byte)0), body[frame, y][x]);
                    }

            for (int y = 0; y < 2; ++y)
                for (int x = 0; x < endUpper[endU, y].Length; ++x)
                {
                    posX = position._1 - Renderer.worldPosition._1 + x;
                    posY = position._2 - Renderer.worldPosition._2 + y;
                    if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                        continue;
                    Renderer.buffer[posY, posX] = new Fragment8(endUfg, new Color8((byte)0, (byte)0, (byte)0), endUpper[endU, y][x]);
                }

            for (int y = 0; y < 2; ++y)
                for (int x = 0; x < middle[mid, y].Length; ++x)
                {
                    posX = position._1 - Renderer.worldPosition._1 + x + progress;
                    posY = position._2 - Renderer.worldPosition._2 + y;
                    if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                        continue;
                    Renderer.buffer[posY, posX] = new Fragment8(midfg, new Color8((byte)0, (byte)0, (byte)0), middle[mid, y][x]);
                }

            for (int y = 0; y < 2; ++y)
                for (int x = 0; x < endLower[endL, y].Length; ++x)
                {
                    posX = position._1 - Renderer.worldPosition._1 + x + length;
                    posY = position._2 - Renderer.worldPosition._2 + y;
                    if (posX < 0 || posX >= (Renderer.Width) || posY < 0 || posY >= (Renderer.Height))
                        continue;
                    Renderer.buffer[posY, posX] = new Fragment8(endLfg, new Color8((byte)0, (byte)0, (byte)0), endLower[endL, y][x]);
                }
        }
    }
}