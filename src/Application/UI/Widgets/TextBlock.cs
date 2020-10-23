using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    public class TextBlock : Widget
    {
        private readonly string _text;
        private Vector2 _position;
        private readonly SpriteFont _font;
        private readonly Color _color;
        private readonly Color? _border;

        public TextBlock(string text, Vector2 position, SpriteFont font, Color color, Color? border)
        {
            _text = text;
            _font = font;
            _color = color;
            _border = border;
            _position = position;

            var (x, y) = font.MeasureString(text);
            Bounds = new Rectangle((int) _position.X, (int) _position.Y, (int) x, (int) y);
        }

        public float Y
        {
            get => _position.Y;
            set
            {
                _position = new Vector2(_position.X, value);
                Bounds = new Rectangle((int) _position.X, (int) _position.Y, Bounds.Width, Bounds.Height);
            }
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
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
        }

        public override void DrawDebug(SpriteBatch spriteBatch) =>
            ShapeHelpers.DrawRectangle(spriteBatch, Bounds, Color.Red);
    }
}