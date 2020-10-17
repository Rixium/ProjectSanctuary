using Application.Content;
using Application.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI
{
    public class Image
    {
        private readonly Sprite _texture;
        private readonly Vector2 _position;
        private readonly float _scale;

        public Image(Sprite texture, Vector2 position, float scale)
        {
            _texture = texture;
            _position = position;
            _scale = scale;
        }

        public int Height => _texture.Source.Height;
        public int Width => _texture.Source.Width;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture.Texture, _position, _texture.Source, Color.White, 0f, _texture.Origin, _scale,
                SpriteEffects.None, 0);
        }

        public void DrawDebug(SpriteBatch spriteBatch)
        {
            var pixel = ContentChest.Instance.Get<Texture2D>("Utils/pixel");
            var bounds = Bounds;
            spriteBatch.Draw(pixel, new Rectangle(bounds.X, bounds.Y, bounds.Width, 1), Color.Red);
            spriteBatch.Draw(pixel, new Rectangle(bounds.X, bounds.Y, 1, bounds.Height), Color.Red);
            spriteBatch.Draw(pixel, new Rectangle(bounds.X + bounds.Width, bounds.Y, 1, bounds.Height), Color.Red);
            spriteBatch.Draw(pixel, new Rectangle(bounds.X, bounds.Y + bounds.Height, bounds.Width, 1), Color.Red);
        }

        public Rectangle Bounds =>
            new Rectangle(
                (int) (_position.X - _texture.Origin.X * _scale),
                (int) (_position.Y - _texture.Origin.Y * _scale),
                (int) (_texture.Source.Width * _scale),
                (int) (_texture.Source.Height * _scale)
            );

        public Vector2 Position => _position;
    }
}