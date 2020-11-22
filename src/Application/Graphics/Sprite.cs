using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Application.Graphics
{
    public class Sprite
    {

        public Texture2D Texture { get; }
        public Rectangle Source { get; }
        public Vector2 Origin { get; }
        public Vector2 Center => new Vector2(Source.Width, Source.Height) / 2.0f;

        public Sprite(Texture2D texture, Rectangle sourceRectangle, Vector2 origin)
        {
            Texture = texture;
            Source = sourceRectangle;
            Origin = origin;
        }


        public Sprite(Texture2D texture, Rectangle sourceRectangle) : this(texture, sourceRectangle, Vector2.Zero)
        { }

        public Sprite(Texture2D texture) : this(texture, new Rectangle(0, 0, texture.Width, texture.Height), Vector2.Zero) { }

    }
}