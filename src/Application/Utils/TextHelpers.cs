using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Utils
{
    internal class TextHelpers
    {
        public static float TextWidth(SpriteFont font, string text) => font.MeasureString(text).X;
        public static float TextHeight(SpriteFont font, string text) => font.MeasureString(text).Y;
        public static Vector2 TextSize(SpriteFont font, string text) => font.MeasureString(text);
    }
}