using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Content.ContentTypes
{
    public class Hair
    {
        public string Name { get; }
        public Rectangle FullSource { get; }
        // The source of the facing direction
        public Rectangle Source =>
            new Rectangle(FullSource.X, FullSource.Y, FullSource.Width, (int) (FullSource.Height / 3f));
        public Texture2D Texture { get; set; }

        public Hair(string name, Rectangle source, Texture2D texture)
        {
            Name = name;
            FullSource = source;
            Texture = texture;
        }
        
    }
}