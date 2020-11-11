using System.Collections.Generic;
using Application.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI
{
    public enum CursorState
    {
        None,
        Cursor,
        Hand
    }

    public class Cursor
    {
        public CursorState ActiveState = CursorState.Cursor;

        private readonly Dictionary<CursorState, Rectangle> _cursorSources = new Dictionary<CursorState, Rectangle>
        {
            {CursorState.None, new Rectangle(0, 0, 0, 0)},
            {CursorState.Cursor, new Rectangle(64, 32, 9, 12)},
            {CursorState.Hand, new Rectangle(42, 51, 15, 17)}
        };

        private readonly Texture2D _cursorTexture;

        public Cursor(IContentChest contentChest) =>
            _cursorTexture = contentChest.Get<Texture2D>("UI/cursors");

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(_cursorTexture, SanctuaryGame.MouseManager.MouseBounds.Location.ToVector2(),
                _cursorSources[ActiveState], Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);

            spriteBatch.End();
        }
    }
}