using System.Linq;
using Application.Graphics;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    internal class HorizontalSelector : Widget
    {
        private readonly Vector2 _position;
        private string[] _options;
        private readonly int _width;
        private readonly SpriteFont _font;
        private readonly int _height;
        private Rectangle _backgroundBounds;

        private int _selectedIndex = 0;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value > _options.Length - 1)
                {
                    _selectedIndex = 0;
                }
                else if (value < 0)
                {
                    _selectedIndex = _options.Length - 1;
                }
                else
                {
                    _selectedIndex = value;
                }
            }
        }

        public HorizontalSelector(Vector2 position, string[] options, int width, Sprite leftArrow, Sprite rightArrow,
            SpriteFont font, float scale)
        {
            _position = position;
            _options = options;
            _width = width;
            _font = font;
            _height = (int) (leftArrow.Source.Height * scale);

            var leftButton = AddChild(new TexturedButton(leftArrow, leftArrow, position, scale));
            var rightButton = AddChild(new TexturedButton(rightArrow, rightArrow,
                position.Add(width - rightArrow.Source.Width * scale, 0), scale));

            _backgroundBounds = new Rectangle(
                (int) (position.X + leftArrow.Source.Width * scale),
                (int) position.Y,
                (int) (width - leftArrow.Source.Width * scale - rightArrow.Source.Width * scale),
                _height
            );

            rightButton.OnClick += () => { SelectedIndex++; };

            leftButton.OnClick += () => { SelectedIndex--; };
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            ShapeHelpers.FillRectangle(spriteBatch, _backgroundBounds.X, _backgroundBounds.Y, _backgroundBounds.Width,
                _backgroundBounds.Height,
                Color.Black * 0.4f);
            spriteBatch.DrawString(_font, _options[_selectedIndex],
                new Vector2(_backgroundBounds.X + 5, _backgroundBounds.Y + 5), Color.White);
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {
        }
    }
}