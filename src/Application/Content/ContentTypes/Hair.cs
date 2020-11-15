using System.Drawing;

namespace Application.Content.ContentTypes
{
    public class Hair
    {
        public string Name { get; }
        public Rectangle Source { get; }

        public Hair(string name, Rectangle source)
        {
            Name = name;
            Source = source;
        }
        
    }
}