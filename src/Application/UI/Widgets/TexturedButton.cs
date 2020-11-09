using System;
using Application.Graphics;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    public class TexturedButton : Widget, IClickable
    {
        private readonly Sprite _texture;
        private readonly Sprite _hoverTexture;

        public TexturedButton(Sprite texture, Sprite hoverTexture, Vector2 position, float scale)
        {
            _texture = texture;
            _hoverTexture = hoverTexture;

            Bounds =
                new Rectangle(
                    (int) (position.X - _texture.Origin.X * scale),
                    (int) (position.Y - _texture.Origin.Y * scale),
                    (int) (_texture.Source.Width * scale),
                    (int) (_texture.Source.Height * scale)
                );
        }

        public int Height => _texture.Source.Height;
        public bool Hovering { get; set; }

        public Action OnClick { get; set; }
        public int Width => _texture.Source.Width;

        public void Update(float delta)
        {
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            if (Hovering)
            {
                spriteBatch.Draw(_hoverTexture.Texture, Bounds, _hoverTexture.Source, Color.White, 0f,
                    Vector2.Zero,
                    SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(_texture.Texture, Bounds, _texture.Source, Color.White, 0f, Vector2.Zero,
                    SpriteEffects.None, 0);
            }
        }

        public override void DrawDebug(SpriteBatch spriteBatch) =>
            ShapeHelpers.DrawRectangle(spriteBatch, Bounds, Color.Red);

        public void Click()
        {
            OnClick?.Invoke();
            Hovering = false;
        }

        public bool Intersects(Rectangle rectangle) =>
            rectangle.Intersects(Bounds);

        public override bool MouseMove(Rectangle mouseBounds)
        {
            if (Intersects(mouseBounds))
            {
                Hovering = true;
                return true;
            }

            Hovering = false;
            
            return base.MouseMove(mouseBounds);
        }

        public override bool MouseClick(Rectangle mouseRectangle)
        {
            if (Intersects(mouseRectangle))
            {
                Click();
                return true;
            }

            return base.MouseClick(mouseRectangle);
        }
    }
}