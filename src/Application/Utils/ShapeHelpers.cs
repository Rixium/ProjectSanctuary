using Application.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NotImplementedException = System.NotImplementedException;

namespace Application.Utils
{
    internal static class ShapeHelpers
    {
        public static IContentChest ContentChest { get; set; }

        private static Texture2D _pixel;

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            _pixel ??= ContentChest.Get<Texture2D>("Utils/pixel");

            var (x, y, width, height) = rectangle;
            spriteBatch.Draw(_pixel, new Rectangle(x, y, width, 1), color);
            spriteBatch.Draw(_pixel, new Rectangle(x, y, 1, height), color);
            spriteBatch.Draw(_pixel, new Rectangle(x + width, y, 1, height), color);
            spriteBatch.Draw(_pixel, new Rectangle(x, y + height, width, 1), color);
        }

        public static void FillRectangle(SpriteBatch spriteBatch, in int x, in int y, in int width, in int height,
            Color color)
        {
            _pixel ??= ContentChest.Get<Texture2D>("Utils/pixel");
            spriteBatch.Draw(_pixel, new Rectangle(x, y, width, height), color);
        }
    }
}