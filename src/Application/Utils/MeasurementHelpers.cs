using Microsoft.Xna.Framework;

namespace Application.Utils
{
    internal static class MeasurementHelpers
    {
        public static Vector2 Measure(int width, int height, float scale)
        {
            var outWidth = width * scale;
            var outHeight = height * scale;
            return new Vector2(outWidth, outHeight);
        }
    }
}