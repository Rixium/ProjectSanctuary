using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Content.ContentTypes
{
    public class Head
    {
        public string Name { get; }

        // The source of the facing direction
        public Rectangle Source =>
            new Rectangle(FullSource.X, FullSource.Y, FullSource.Width, (int) (FullSource.Height / 3f));

        public Rectangle FullSource { get; set; }
        public Texture2D Texture { get; }

        public Head(string name, Rectangle source, Texture2D texture)
        {
            Name = name;
            FullSource = source;
            Texture = texture;
        }
    }
}