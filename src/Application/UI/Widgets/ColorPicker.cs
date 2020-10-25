using Application.Content;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.UI.Widgets
{
    internal class ColorPicker : Widget
    {
        private readonly Vector2 _position;
        private readonly int _width;
        private float _hue = 50;
        private float _saturation = 50;
        private float _value = 50;
        private readonly int _segments;

        private Slider _hueSlider;
        private Slider _saturationSlider;
        private Slider _valueSlider;
        private Slider _dragged;

        private const int Margin = 30;
        private const int Height = 10;


        public ColorPicker(Vector2 position, int width, int segments, float scale)
        {
            _position = position;
            _width = width;
            _segments = segments;

            _hueSlider = AddChild(new Slider(position, 0, 100, _hue, width, scale));
            _saturationSlider = AddChild(new Slider(position + new Vector2(0, Height + Margin), 0, 100, _saturation,
                width, scale));
            _valueSlider = AddChild(new Slider(position + new Vector2(0, Height + Margin + Height + Margin), 0, 100,
                _value, width, scale));
        }

        public void Update()
        {
            if (SanctuaryGame.MouseManager.Dragging)
            {
                if (SanctuaryGame.MouseManager.MouseBounds.Intersects(_hueSlider.SliderBounds) ||
                    _dragged == _hueSlider)
                {
                    _hueSlider.IncreaseValue(SanctuaryGame.MouseManager.Drag.X / 2f);
                    _dragged = _hueSlider;
                    _hue = _hueSlider.GetValue();
                }
                else if (SanctuaryGame.MouseManager.MouseBounds.Intersects(_saturationSlider.SliderBounds) ||
                         _dragged == _saturationSlider)
                {
                    _saturationSlider.IncreaseValue(SanctuaryGame.MouseManager.Drag.X / 2f);
                    _dragged = _saturationSlider;
                    _saturation = _saturationSlider.GetValue();
                }
                else if (SanctuaryGame.MouseManager.MouseBounds.Intersects(_valueSlider.SliderBounds) ||
                         _dragged == _valueSlider)
                {
                    _valueSlider.IncreaseValue(SanctuaryGame.MouseManager.Drag.X / 2f);
                    _dragged = _valueSlider;
                    _value = _valueSlider.GetValue();
                }
            }
            else
            {
                _dragged = null;
            }
        }


        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < _segments; i++)
            {
                var color = ColorHelpers.HsvToRgb((double) i / _segments * 360.0, 1, 1);
                spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                    new Rectangle((int) (_position.X + _width / _segments * i), (int) _position.Y, _width / _segments,
                        Height), color);

                color = ColorHelpers.HsvToRgb(_hue / 100 * 306, (double) i / _segments, _value / 100);
                spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                    new Rectangle((int) (_position.X + _width / _segments * i), (int) _position.Y + Height + Margin,
                        _width / _segments,
                        Height), color);

                color = ColorHelpers.HsvToRgb(_hue / 100 * 360, _saturation / 100, (double) i / _segments);
                spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                    new Rectangle((int) (_position.X + _width / _segments * i),
                        (int) _position.Y + Height + Margin + Height + Margin, _width / _segments,
                        Height), color);
            }
            
            spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                new Rectangle((int) (_position.X), (int) _position.Y + Height + Margin + Height + Margin + Height + Margin, 40,
                    40), ColorHelpers.HsvToRgb(_hue / 100 * 360, _saturation / 100, _value / 100));
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) _position.X, (int) _position.Y, _width, 10),
                Color.Red);
        }
    }
}