using System;
using Application.Content;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    internal class ColorPicker : Widget
    {
        private readonly IContentChest _contentChest;
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
        
        public Action Changed { get; set; }
        
        public ColorPicker(IContentChest contentChest, Vector2 position, int width, int segments, float scale)
        {
            _contentChest = contentChest;
            _position = position;
            _width = width;
            _segments = segments;

            _hueSlider = AddChild(new Slider(contentChest, position, 0, 100, _hue, width, scale));
            _saturationSlider = AddChild(new Slider(contentChest, position + new Vector2(0, Height + Margin), 0, 100, _saturation,
                width, scale));
            _valueSlider = AddChild(new Slider(contentChest, position + new Vector2(0, Height + Margin + Height + Margin), 0, 100,
                _value, width, scale));

            _hueSlider.Changed += () =>
            {
                _hue = _hueSlider.GetValue();
                Changed?.Invoke();
            };
            _saturationSlider.Changed += () =>
            {
                _saturation = _saturationSlider.GetValue();
                Changed?.Invoke();
            };
            _valueSlider.Changed += () =>
            {
                _value = _valueSlider.GetValue();
                Changed?.Invoke();
            };
            
            Bounds = new Rectangle((int) position.X, (int) position.Y, width,
                Height + Margin + Height + Margin + Height);
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < _segments; i++)
            {
                var color = ColorHelpers.HsvToRgb((double) i / _segments * 360.0, 1, 1);
                spriteBatch.Draw(_contentChest.Get<Texture2D>("Utils/pixel"),
                    new Rectangle((int) (_position.X + _width / _segments * i), (int) _position.Y, _width / _segments,
                        Height), color);

                color = ColorHelpers.HsvToRgb(_hue / 100 * 306, (double) i / _segments, _value / 100);
                spriteBatch.Draw(_contentChest.Get<Texture2D>("Utils/pixel"),
                    new Rectangle((int) (_position.X + _width / _segments * i), (int) _position.Y + Height + Margin,
                        _width / _segments,
                        Height), color);

                color = ColorHelpers.HsvToRgb(_hue / 100 * 360, _saturation / 100, (double) i / _segments);
                spriteBatch.Draw(_contentChest.Get<Texture2D>("Utils/pixel"),
                    new Rectangle((int) (_position.X + _width / _segments * i),
                        (int) _position.Y + Height + Margin + Height + Margin, _width / _segments,
                        Height), color);
            }
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) _position.X, (int) _position.Y, _width, 10),
                Color.Red);
        }

        public Color GetColor() =>
            ColorHelpers.HsvToRgb(_hue / 100 * 360, _saturation / 100, _value / 100);

        public override bool MouseClick(Rectangle mouseRectangle)
        {
            if (mouseRectangle.Intersects(_hueSlider.SliderBounds))
            {
                _dragged = _hueSlider;
            } else if (mouseRectangle.Intersects(_saturationSlider.SliderBounds))
            {
                _dragged = _saturationSlider;
            } else if (mouseRectangle.Intersects(_valueSlider.SliderBounds))
            {
                _dragged = _valueSlider;
            }
            
            return base.MouseClick(mouseRectangle);
        }

        public override bool MouseMove(Rectangle mouseRectangle)
        {
            if (_dragged == null)
            {
                return false;
            }

            var normalised = mouseRectangle.X - _dragged.Bounds.Left;
            _dragged.SetValue(normalised * (100.0f / _width));

            return true;
        }

        public override bool MouseReleased(Rectangle mouseBounds)
        {
            _dragged = null;
            return base.MouseReleased(mouseBounds);
        }

        public void SetValues(float hue, float saturation, float value)
        {
            _hueSlider.SetValue(hue);
            _saturationSlider.SetValue(saturation);
            _valueSlider.SetValue(value);
        }
    }
}