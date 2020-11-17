using Microsoft.Xna.Framework;

namespace Application.Content.ContentTypes
{
    public class Eyes
    {
        public string Name { get; }
        public Rectangle Source { get; }

        public Eyes(string name, Rectangle source)
        {
            Name = name;
            Source = source;
        }
    }
}