
using Application.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        
    }
}