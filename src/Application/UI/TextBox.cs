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
        private readonly int _maxLength;
        private Rectangle _bounds;

        private int _carotPosition;

        public TextBox(Vector2 position, SpriteFont font, int maxLength = int.MaxValue)
        {
            _position = position;
            _font = font;
            _maxLength = maxLength;

            var fontSize = font.MeasureString("X");
            _bounds = new Rectangle((int) position.X, (int) position.Y, 100, (int) (fontSize.Y + 20));
            
            SanctuaryGame.KeyboardDispatcher.SubscribeToAnyKeyPress(OnKeyPressed);
        }

        private void OnKeyPressed(Keys pressedKey)
        {
            if (pressedKey == Keys.Back)
            {
                if (_carotPosition > 0)
                {
                    _text = _text.Remove(_carotPosition - 1, 1);
                    _carotPosition--;
                }
            }
            else
            {
                var character = GetCharacter(pressedKey);
                if (character != null)
                {
                    _text += character;
                    _carotPosition++;
                }
            }
        }

        private static char? GetCharacter(Keys pressedKey) => pressedKey.ToChar(false);

        public void Draw(SpriteBatch spriteBatch)
        {
            var show = DateTime.Now.Millisecond % 1000 < 500;

            spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                _bounds, new Color(221, 190, 137));

            var tempText = show ? _text.Insert(_carotPosition, "|") : _text;

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