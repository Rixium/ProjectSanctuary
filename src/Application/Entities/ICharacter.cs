using Application.Content.ContentTypes;
using Microsoft.Xna.Framework;

namespace Application.Entities
{
    public interface ICharacter
    {
        Vector2 Position { get; set; }
        Head Head { get; set; }
        Hair Hair { get; set; }
        
    }
}