using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI
{
    internal class TextBlock
    {
        private readonly string _text;
        private readonly Vector2 _position;
        private readonly SpriteFont _font;
        private readonly Color _color;
        private readonly Color? _border;
        private readonly Vector2 _textSize;

        public TextBlock(string text, Vector2 position, SpriteFont font, Color color, Color? border)
        {
            _text = text;
            _font = font;
            _color = color;
            _border = border;
            _textSize = font.MeasureString(text);
            _position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_border != null)
            {
                spriteBatch.DrawString(_font, _text,
                    new Vector2(_position.X - 1, _position.Y + 1),
                    _border.Value);
                spriteBatch.DrawString(_font, _text,
                    new Vector2(_position.X - 1, _position.Y - 1),
                    _border.Value);
                spriteBatch.DrawString(_font, _text,
                    new Vector2(_position.X + 1, _position.Y + 1),
                    _border.Value);
                spriteBatch.DrawString(_font, _text,
                    new Vector2(_position.X + 1, _position.Y - 1),
                    _border.Value);
            }

            spriteBatch.DrawString(_font, _text, _position, _color);

            if (SanctuaryGame.Debug)
            {
                DrawDebug(spriteBatch);
            }
        }

        public void DrawDebug(SpriteBatch spriteBatch) => ShapeHelpers.DrawRectangle(spriteBatch, Bounds, Color.Red);

        public Rectangle Bounds =>
            new Rectangle((int) _position.X, (int) _position.Y, (int) _textSize.X, (int) _textSize.Y);
    }
}