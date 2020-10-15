using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View.Graphics;

namespace ProjectSanctuary.View.UI
{
    public class TexturedButton : IClickable
    {
        private readonly Sprite _texture;
        private readonly Sprite _hoverTexture;
        private readonly Vector2 _position;

        public TexturedButton(Sprite texture, Sprite hoverTexture, Vector2 position)
        {
            _texture = texture;
            _hoverTexture = hoverTexture;
            _position = position;
        }

        public int Height => _texture.Source.Height;

        public int Width => _texture.Source.Width;

        public void Update(float delta)
        {
        }

        public void Draw(SpriteBatch spriteBatch) =>
            spriteBatch.Draw(_texture.Texture, _position, _texture.Source, Color.White, 0f, _texture.Origin ,1f,
                SpriteEffects.None, 0);
    }
}