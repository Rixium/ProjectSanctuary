using System;
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
        private double _hue = 50;
        private double _saturation = 50;
        private double _value = 50;
        private readonly int _segments;

        public ColorPicker(Vector2 position, int width, int segments)
        {
            _position = position;
            _width = width;
            _segments = segments;
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < _segments; i++)
            {
                var color = ColorHelpers.HsvToRgb((double) i / _segments * 360.0, 1, 1);
                spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                    new Rectangle((int) (_position.X + _width / _segments * i), (int) _position.Y, _width / _segments,
                        10), color);

                color = ColorHelpers.HsvToRgb(_hue / 100 * 306, (double) i / _segments, _value / 100);
                spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                    new Rectangle((int) (_position.X + _width / _segments * i), (int) _position.Y + 10, _width / _segments,
                        10), color);
                
                color = ColorHelpers.HsvToRgb(_hue / 100 * 360, _saturation / 100, (double) i / _segments);
                spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                    new Rectangle((int) (_position.X + _width / _segments * i), (int) _position.Y + 20, _width / _segments,
                        10), color);
            }
            
            spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                new Rectangle((int) (_position.X), (int) _position.Y + 40, 40,
                    40), ColorHelpers.HsvToRgb(_hue / 100 * 360, _saturation / 100, _value / 100));
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) _position.X, (int) _position.Y, _width, 10),
                Color.Red);
        }
    }
}