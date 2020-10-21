using System;
using Application.Content;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.UI
{
    public class TextBox
    {
        private string _text = "";
        private readonly Vector2 _position;
        private readonly SpriteFont _font;
        private readonly Rectangle _bounds;

        public TextBox(Vector2 position, SpriteFont font, int width)
        {
            _position = position;
            _font = font;

            var (_, fontY) = font.MeasureString("x");

            _bounds = new Rectangle((int) position.X, (int) position.Y, width, (int) (fontY + 20));

            SanctuaryGame.KeyboardDispatcher.SubscribeToAnyKeyPress(OnKeyPressed);
        }

        private void OnKeyPressed(Keys pressedKey)
        {
            if (pressedKey == Keys.Back)
            {
                _text = _text.Length > 0 ? _text.Remove(_text.Length - 1) : _text;
                return;
            }
            
            var character = GetCharacter(pressedKey);

            if (character == null)
            {
                return;
            }

            var newText = _text + character;
            
            if (_font.MeasureString(newText).X >= _bounds.Width - 20)
            {
                return;
            }

            _text = newText;
        }

        private static char? GetCharacter(Keys pressedKey) => pressedKey.ToChar(false);

        public void Draw(SpriteBatch spriteBatch)
        {
            var show = DateTime.Now.Millisecond % 1000 < 500;

            spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                _bounds, new Color(221, 190, 137));

            var tempText = show ? _text.Insert(_text.Length, "|") : _text;

            spriteBatch.DrawString(_font, tempText, _position + new Vector2(10, 10), Color.Black);

            if (SanctuaryGame.Debug)
            {
                DrawDebug(spriteBatch);
            }
        }

        public void DrawDebug(SpriteBatch spriteBatch)
        {
        }
    }
}