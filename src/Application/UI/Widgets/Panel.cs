using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    public class Panel : Widget
    {
        private readonly NineSlice _nineSlice;
        private readonly float _scale;

        public Panel(NineSlice nineSlice, Rectangle bounds, float scale)
        {
            _nineSlice = nineSlice;
            Bounds = bounds;
            _scale = scale;
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            _nineSlice.DrawRectangle(spriteBatch, Bounds, _scale);

            if (SanctuaryGame.Debug)
            {
                DrawDebug(spriteBatch);
            }
        }

        public override void DrawDebug(SpriteBatch spriteBatch) => _nineSlice.DrawDebug(spriteBatch, Bounds, _scale);
        
    }
}