using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI
{
    internal class Panel
    {
        private readonly NineSlice _nineSlice;
        private readonly Rectangle _bounds;
        private readonly float _scale;

        public Panel(NineSlice nineSlice, Rectangle bounds, float scale)
        {
            _nineSlice = nineSlice;
            _bounds = bounds;
            _scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var topLeft = _nineSlice.Get(Segment.TopLeft);
            var top = _nineSlice.Get(Segment.Top);
            var topRight = _nineSlice.Get(Segment.TopRight);
            var right = _nineSlice.Get(Segment.Right);
            var bottomRight = _nineSlice.Get(Segment.BottomRight);
            var bottom = _nineSlice.Get(Segment.Bottom);
            var bottomLeft = _nineSlice.Get(Segment.BottomLeft);
            var left = _nineSlice.Get(Segment.Left);
            var center = _nineSlice.Get(Segment.Center);

            spriteBatch.Draw(_nineSlice.Texture,
                new Rectangle(_bounds.Left, _bounds.Top, (int) (topLeft.Width * _scale),
                    (int) (topLeft.Height * _scale)), topLeft, Color.White);

            spriteBatch.Draw(_nineSlice.Texture,
                new Rectangle((int) (_bounds.Right - topRight.Width * _scale), _bounds.Top,
                    (int) (topRight.Width * _scale), (int) (topRight.Height * _scale)), topRight,
                Color.White);

            spriteBatch.Draw(_nineSlice.Texture,
                new Rectangle(_bounds.Left, (int) (_bounds.Bottom - bottomLeft.Height * _scale),
                    (int) (bottomLeft.Width * _scale), (int) (bottomLeft.Height * _scale)),
                bottomLeft, Color.White);

            spriteBatch.Draw(_nineSlice.Texture,
                new Rectangle((int) (_bounds.Right - bottomRight.Width * _scale),
                    (int) (_bounds.Bottom - bottomRight.Height * _scale), (int) (bottomRight.Width * _scale),
                    (int) (bottomRight.Height * _scale)), bottomRight,
                Color.White);

            spriteBatch.Draw(_nineSlice.Texture,
                new Rectangle((int) (_bounds.Left + topLeft.Width * _scale), _bounds.Top,
                    (int) (_bounds.Width - topLeft.Width * _scale - topRight.Width * _scale),
                    (int) (top.Height * _scale)), top, Color.White);

            spriteBatch.Draw(_nineSlice.Texture,
                new Rectangle((int) (_bounds.Left + bottomLeft.Width * _scale),
                    (int) (_bounds.Bottom - bottom.Height * _scale),
                    (int) (_bounds.Width - bottomLeft.Width * _scale - bottomRight.Width * _scale),
                    (int) (bottom.Height * _scale)), bottom, Color.White);

            spriteBatch.Draw(_nineSlice.Texture,
                new Rectangle(_bounds.Left, (int) (_bounds.Top + topLeft.Height * _scale), (int) (left.Width * _scale),
                    (int) (_bounds.Height - topLeft.Height * _scale - bottomLeft.Height * _scale)), left, Color.White);

            spriteBatch.Draw(_nineSlice.Texture,
                new Rectangle((int) (_bounds.Right - right.Width * _scale),
                    (int) (_bounds.Top + topRight.Height * _scale), (int) (right.Width * _scale),
                    (int) (_bounds.Height - topRight.Height * _scale - bottomRight.Height * _scale)), right,
                Color.White);

            spriteBatch.Draw(_nineSlice.Texture,
                new Rectangle((int) (_bounds.Left + left.Width * _scale), (int) (_bounds.Top + top.Height * _scale),
                    (int) (_bounds.Width - left.Width * _scale - right.Width * _scale),
                    (int) (_bounds.Height - top.Height * _scale - bottom.Height * _scale)), center,
                Color.White);

            if (SanctuaryGame.Debug)
            {
                DrawDebug(spriteBatch);
            }
        }

        public void DrawDebug(SpriteBatch spriteBatch)
        {
            var topLeft = _nineSlice.Get(Segment.TopLeft);
            var top = _nineSlice.Get(Segment.Top);
            var topRight = _nineSlice.Get(Segment.TopRight);
            var right = _nineSlice.Get(Segment.Right);
            var bottomRight = _nineSlice.Get(Segment.BottomRight);
            var bottom = _nineSlice.Get(Segment.Bottom);
            var bottomLeft = _nineSlice.Get(Segment.BottomLeft);
            var left = _nineSlice.Get(Segment.Left);

            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle(_bounds.Left, _bounds.Top,
                (int) (topLeft.Width * _scale),
                (int) (topLeft.Height * _scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) (_bounds.Right - topRight.Width * _scale),
                _bounds.Top,
                (int) (topRight.Width * _scale), (int) (topRight.Height * _scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle(_bounds.Left,
                (int) (_bounds.Bottom - bottomLeft.Height * _scale),
                (int) (bottomLeft.Width * _scale), (int) (bottomLeft.Height * _scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) (_bounds.Right - bottomRight.Width * _scale),
                (int) (_bounds.Bottom - bottomRight.Height * _scale), (int) (bottomRight.Width * _scale),
                (int) (bottomRight.Height * _scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) (_bounds.Left + topLeft.Width * _scale),
                _bounds.Top,
                (int) (_bounds.Width - topLeft.Width * _scale - topRight.Width * _scale),
                (int) (top.Height * _scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) (_bounds.Left + bottomLeft.Width * _scale),
                (int) (_bounds.Bottom - bottom.Height * _scale),
                (int) (_bounds.Width - bottomLeft.Width * _scale - bottomRight.Width * _scale),
                (int) (bottom.Height * _scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch,
                new Rectangle(_bounds.Left, (int) (_bounds.Top + topLeft.Height * _scale), (int) (left.Width * _scale),
                    (int) (_bounds.Height - topLeft.Height * _scale - bottomLeft.Height * _scale)), Color.Red);

            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) (_bounds.Right - right.Width * _scale),
                (int) (_bounds.Top + topRight.Height * _scale), (int) (right.Width * _scale),
                (int) (_bounds.Height - topRight.Height * _scale - bottomRight.Height * _scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) (_bounds.Left + left.Width * _scale),
                (int) (_bounds.Top + top.Height * _scale),
                (int) (_bounds.Width - left.Width * _scale - right.Width * _scale),
                (int) (_bounds.Height - top.Height * _scale - bottom.Height * _scale)), Color.Red);
        }

        public Vector2 BottomLeft() => new Vector2(_bounds.X, _bounds.Y + _bounds.Height);

        public int Right() => _bounds.X + _bounds.Width;

        public int Top() => _bounds.Y;

        public Vector2 Center() => new Vector2(_bounds.X + _bounds.Width / 2f, _bounds.Y + _bounds.Height / 2f);
    }
}