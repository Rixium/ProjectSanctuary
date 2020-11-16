using Microsoft.Xna.Framework;

namespace Application.Content.ContentTypes
{
    public class Head
    {
        public string Name { get; }
        public Rectangle Source { get; }

        public Head(string name, Rectangle source)
        {
            Name = name;
            Source = source;
        }
        
    }
}