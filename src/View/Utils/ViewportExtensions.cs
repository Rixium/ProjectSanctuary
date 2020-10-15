using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View.Utils
{
    public static class ViewportExtensions
    {
        public static Vector2 Center(this Viewport viewport) => 
            new Vector2(viewport.Width, viewport.Height) / 2.0f;
    }
}