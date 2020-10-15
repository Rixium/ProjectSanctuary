using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View.Content;
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
        public bool Hovering { get; set; }

        public Action OnClick { get; set; }
        public int Width => _texture.Source.Width;

        public void Update(float delta)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Hovering)
            {
                spriteBatch.Draw(_hoverTexture.Texture, _position, _hoverTexture.Source, Color.White, 0f,
                    _hoverTexture.Origin, 1.1f,
                    SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(_texture.Texture, _position, _texture.Source, Color.White, 0f, _texture.Origin, 1f,
                    SpriteEffects.None, 0);
            }
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

        public void Click() => OnClick?.Invoke();

        public Rectangle Bounds => new Rectangle((int) (_position.X - _texture.Origin.X), (int)(_position.Y - _texture.Origin.Y), _texture.Source.Width,
            _texture.Source.Height);
        
        public bool Intersects(Rectangle rectangle) =>
            rectangle.Intersects(Bounds);
    }
}