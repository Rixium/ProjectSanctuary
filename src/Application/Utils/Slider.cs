using Application.Content;
using Application.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Utils
{
    internal class Slider : Widget
    {
        private readonly Rectangle _sliderSource;
        private readonly Texture2D _texture;
        private readonly int _width;
        private readonly float _scale;

        private float _currentValue;
        private float _maxValue;
        private float _minValue;

        public Slider(Vector2 position, float minValue, float maxValue, float startingValue, int width, float scale)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _currentValue = startingValue;
            _width = width;
            _scale = scale;

            _sliderSource = new Rectangle(34, 189, 10, 10);
            _texture = ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons");

            var (x, y) = position;
            Bounds = new Rectangle((int) x, (int) y, width, 10);
        }

        private float XOffset() => _currentValue / _maxValue * _width;

        public void IncreaseValue(float increase)
        {
            _currentValue += increase;

            if (_currentValue < _minValue)
            {
                _currentValue = _minValue;
            }

            if (_currentValue > _maxValue)
            {
                _currentValue = _maxValue;
            }
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                SliderBounds,
                _sliderSource,
                Color.White
            );
        }

        public Rectangle SliderBounds => new Rectangle(
            (int) (Bounds.X + XOffset() - _sliderSource.Width / 2f * _scale),
            (int) (Bounds.Y + Bounds.Height / 2f - _sliderSource.Height * _scale / 2f),
            (int) (_sliderSource.Width * _scale),
            (int) (_sliderSource.Height * _scale));

        public override void DrawDebug(SpriteBatch spriteBatch) =>
            ShapeHelpers.DrawRectangle(spriteBatch, Bounds, Color.Red);

        public float GetValue() => _currentValue;
    }
}