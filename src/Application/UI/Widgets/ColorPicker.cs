using Application.Content;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    internal class ColorPicker : Widget
    {
        private readonly Vector2 _position;
        private readonly int _width;
        private double _hue;
        private double _saturation;
        private double _value;

        public ColorPicker(Vector2 position, int width)
        {
            _position = position;
            _width = width;
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < _width; i++)
            {
                var color = ColorHelpers.HsvToRgb(i / (double) _width * 360.0, 1, 1);
                spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                    new Rectangle((int) (_position.X + _width / _width * i), (int) _position.Y, _width / _width,
                        10), color);
            }
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) _position.X, (int) _position.Y, _width, 10),
                Color.Red);
        }
    }
}