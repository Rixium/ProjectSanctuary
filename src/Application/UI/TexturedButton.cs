using System;
using Application.Graphics;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI
{
    public class TexturedButton : IClickable
    {
        private readonly Sprite _texture;
        private readonly Sprite _hoverTexture;
        private readonly Vector2 _position;
        private readonly float _scale;

        public TexturedButton(Sprite texture, Sprite hoverTexture, Vector2 position, float scale)
        {
            _texture = texture;
            _hoverTexture = hoverTexture;
            _position = position;
            _scale = scale;
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
                spriteBatch.Draw(_hoverTexture.Texture, Bounds, _hoverTexture.Source, Color.White, 0f,
                    Vector2.Zero,
                    SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(_texture.Texture, Bounds, _texture.Source, Color.White, 0f, Vector2.Zero,
                    SpriteEffects.None, 0);
            }

            if (SanctuaryGame.Debug)
            {
                DrawDebug(spriteBatch);
            }
        }

        public void DrawDebug(SpriteBatch spriteBatch) => ShapeHelpers.DrawRectangle(spriteBatch, Bounds, Color.Red);

        public void Click()
        {
            OnClick?.Invoke();
            Hovering = false;
        }

        public Rectangle Bounds =>
            new Rectangle(
                (int) (_position.X - _texture.Origin.X * _scale),
                (int) (_position.Y - _texture.Origin.Y * _scale),
                (int) (_texture.Source.Width * _scale),
                (int) (_texture.Source.Height * _scale)
            );

        public bool Intersects(Rectangle rectangle) =>
            rectangle.Intersects(Bounds);
    }
}