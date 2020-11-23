using Application.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Renderers
{
    internal class CharacterRenderer : ICharacterRenderer
    {
        public void Render(SpriteBatch spriteBatch, ICharacter character)
        {
            var head = character.Head;
            if (head == null)
            {
                return;
            }

            spriteBatch.Draw(head.Texture, character.Position, head.Source, Color.White, 0f, new Vector2(head.Source.Width / 2f, head.Source.Height / 2f), 3f, SpriteEffects.None, 1f);
            
            var hair = character.Hair;
            if (hair == null)
            {
                return;
            }
            
            spriteBatch.Draw(hair.Texture, character.Position, hair.Source, Color.White, 0f, new Vector2(hair.Source.Width / 2f, hair.Source.Height / 2f), 3f, SpriteEffects.None, 1f);
        }
    }
}