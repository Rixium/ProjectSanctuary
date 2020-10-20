using System;
using System.Linq;
using System.Linq.Expressions;
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
        private KeyboardState _lastState;

        public TextBox(Vector2 position, SpriteFont font, int maxLength = int.MaxValue)
        {
            _position = position;
            _font = font;

            SanctuaryGame.KeyboardDispatcher.SubscribeToAnyKeyPress(OnKeyPressed);
        }

        private void OnKeyPressed(Keys pressedKey)
        {
            if (pressedKey == Keys.Back && _text.Length > 0)
            {
                _text = _text.Remove(_text.Length - 1);
            }
            else
            {
                var character = GetCharacter(pressedKey);
                if (character != null)
                {
                    _text += character;
                }
            }
        }

        private static char? GetCharacter(Keys pressedKey) => pressedKey.ToChar(false);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text, _position, Color.White);

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