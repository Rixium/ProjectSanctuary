using System;
using System.Collections.Generic;
using Application.Content;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.UI.Widgets
{
    public class TextBox : Widget
    {
        private string _text = "";
        private readonly SpriteFont _font;
        private readonly NineSlice _nineSlice;
        private bool Selected { get; set; }


        public TextBox(Vector2 position, SpriteFont font, int width)
        {
            _font = font;

            var (x, y) = position;
            Bounds = new Rectangle((int) x, (int) y, width, 30);

            SanctuaryGame.KeyboardDispatcher.SubscribeToAnyKeyPress(OnKeyPressed);


            _nineSlice = new NineSlice(ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons"),
                new Dictionary<Segment, Rectangle>
                {
                    {Segment.TopLeft, new Rectangle(233, 4, 1, 1)},
                    {Segment.Top, new Rectangle(234, 4, 1, 1)},
                    {Segment.TopRight, new Rectangle(235, 4, 1, 1)},
                    {Segment.Right, new Rectangle(235, 5, 1, 1)},
                    {Segment.BottomRight, new Rectangle(235, 6, 1, 1)},
                    {Segment.Bottom, new Rectangle(234, 6, 1, 1)},
                    {Segment.BottomLeft, new Rectangle(233, 6, 1, 1)},
                    {Segment.Left, new Rectangle(233, 5, 1, 1)},
                    {Segment.Center, new Rectangle(234, 5, 1, 1)},
                });
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

            if (_font.MeasureString(newText).X >= Bounds.Width - 20)
            {
                return;
            }

            _text = newText;
        }

        private static char? GetCharacter(Keys pressedKey) => pressedKey.ToChar(false);

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            var show = Selected && DateTime.Now.Millisecond % 1000 < 500;

            _nineSlice.DrawRectangle(spriteBatch, Bounds, 3f);

            if (Selected)
            {
                ShapeHelpers.DrawRectangle(spriteBatch, Bounds, Color.Green);
            }

            var tempText = show ? _text.Insert(_text.Length, "|") : _text;

            spriteBatch.DrawString(_font, tempText,
                new Vector2(Bounds.Left + 10, Bounds.Center.Y - _font.MeasureString(tempText).Y / 2f), Color.Black);
        }

        public override void DrawDebug(SpriteBatch spriteBatch) =>
            ShapeHelpers.DrawRectangle(spriteBatch, Bounds, Color.Red);

        public override bool MouseClick(Rectangle mouseRectangle)
        {
            Selected = SanctuaryGame.MouseManager.MouseBounds.Intersects(Bounds);
            return Selected;
        }
    }
}