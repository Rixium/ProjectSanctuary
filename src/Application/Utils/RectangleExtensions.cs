using Microsoft.Xna.Framework;

namespace Application.Utils
{
    public static class RectangleExtensions
    {
        
        public static Rectangle Add(this Rectangle a, int x, int y) => 
            new Rectangle(a.X + x, a.Y + y, a.Width, a.Height);
        
    }
}