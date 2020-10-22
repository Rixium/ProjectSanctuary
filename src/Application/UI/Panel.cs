using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI
{
    public class Panel
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
            _nineSlice.DrawRectangle(spriteBatch, _bounds, _scale);

            if (SanctuaryGame.Debug)
            {
                DrawDebug(spriteBatch);
            }
        }

        public void DrawDebug(SpriteBatch spriteBatch) => _nineSlice.DrawDebug(spriteBatch, _bounds, _scale);

        public Vector2 BottomLeft() => new Vector2(Left(), Bottom());

        public int Right() => _bounds.X + _bounds.Width;

        public int Top() => _bounds.Y;

        public Vector2 Center() => new Vector2(_bounds.X + _bounds.Width / 2f, _bounds.Y + _bounds.Height / 2f);

        public int Left() => _bounds.X;

        public Vector2 BottomRight() => new Vector2(Right(), Bottom());

        private int Bottom() => _bounds.Y + _bounds.Height;
    }
}