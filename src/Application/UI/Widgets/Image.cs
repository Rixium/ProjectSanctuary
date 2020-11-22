using Application.Graphics;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    public class Image : Widget
    {
        private readonly Sprite _texture;
        private readonly float _scale;

        public Image(Sprite texture, Vector2 position, float scale)
        {
            _texture = texture;
            _scale = scale;

            Bounds =
                new Rectangle(
                    (int) (position.X),
                    (int) (position.Y),
                    (int) (_texture.Source.Width * scale),
                    (int) (_texture.Source.Height * scale)
                );
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture.Texture, Bounds, _texture.Source, Color.White, 0f, Vector2.Zero,
                SpriteEffects.None, 0);
        }

        public override void DrawDebug(SpriteBatch spriteBatch) =>
            ShapeHelpers.DrawRectangle(spriteBatch, Bounds, Color.Red);

    }
}