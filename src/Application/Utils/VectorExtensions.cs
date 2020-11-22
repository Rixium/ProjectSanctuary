using Microsoft.Xna.Framework;

namespace Application.Utils
{
    internal static class VectorExtensions
    {
        public static Vector2 Add(this Vector2 vector2, float x, float y) => new Vector2(vector2.X + x, vector2.Y + y);
    }
}