using Microsoft.Xna.Framework;

namespace Application.Utils
{
    public static class RectangleExtensions
    {
        public static Rectangle Add(this Rectangle a, float x, float y, float width, float height) =>
            new Rectangle((int) (a.X + x), (int) (a.Y + y), (int) (a.Width + width), (int) (a.Height + height));
    }
}