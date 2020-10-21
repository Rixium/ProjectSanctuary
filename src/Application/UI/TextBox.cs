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
        private MouseState _lastMouseState;
        public bool Selected { get; private set; }


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
            if (!Selected)
            {
                return;
            }

            if (pressedKey == Keys.Back)
            {
                _text = _text.Length > 0 ? _text.Remove(_text.Length - 1) : _text;
                return;
            }
            
            if (pressedKey == Keys.Enter)
            {
                Selected = false;
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

        public void Update(float delta)
        {
            var mouse = Mouse.GetState();
            var mouseRect = new Rectangle(mouse.Position, new Point(1, 1));
            if (mouse.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released)
            {
                Selected = mouseRect.Intersects(_bounds);
            }

            _lastMouseState = mouse;
        }

        private static char? GetCharacter(Keys pressedKey) => pressedKey.ToChar(false);

        public void Draw(SpriteBatch spriteBatch)
        {
            var show = Selected && DateTime.Now.Millisecond % 1000 < 500;

            spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                _bounds, new Color(221, 190, 137));

            if (Selected)
            {
                ShapeHelpers.DrawRectangle(spriteBatch, _bounds, Color.Green);
            }

            var tempText = show ? _text.Insert(_text.Length, "|") : _text;

            spriteBatch.DrawString(_font, tempText, _position + new Vector2(10, 10), Color.Black);

            if (SanctuaryGame.Debug)
            {
                DrawDebug(spriteBatch);
            }
        }

        public void DrawDebug(SpriteBatch spriteBatch) =>
            ShapeHelpers.DrawRectangle(spriteBatch, _bounds, Color.Red);
    }
}