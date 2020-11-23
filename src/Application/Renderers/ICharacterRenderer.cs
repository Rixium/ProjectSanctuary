using Application.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Renderers
{
    public interface ICharacterRenderer
    {
        void Render(SpriteBatch spriteBatch, ICharacter character);
    }
}