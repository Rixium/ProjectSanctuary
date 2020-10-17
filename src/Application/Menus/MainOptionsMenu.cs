using Application.Content;
using Application.Graphics;
using Application.UI;
using Application.Utils;
using Application.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.Menus
{
    internal class MainOptionsMenu : Menu
    {
        private float _titleYOffset;
        private MouseState _lastMouse;
        private ScrollBox _scrollBox;

        public MainOptionsMenu()
        {
            _titleYOffset = ViewManager.ViewPort.Height / 2.0f - 50;

            _scrollBox =
                new ScrollBox(
                    "Hello everyone, thanks for purchasing this amazing game.\nProject Sanctuary is a game about raising animals that have long been forgotten.",
                    new Rectangle(10, 10, 400, 400));
        }

        public override void Update(float delta)
        {
            var mouse = Mouse.GetState();
            var mousePosition = new Vector2(mouse.X, mouse.Y);

            var mouseRectangle = new Rectangle((int) mousePosition.X, (int) mousePosition.Y, 1, 1);

            if (_titleYOffset > 0)
            {
                _titleYOffset = MathHelper.Lerp(_titleYOffset, 0, delta);
            }

            IClickable hoveringButton = null;

            foreach (var button in Clickables)
            {
                button.Hovering = false;

                if (!button.Intersects(mouseRectangle))
                {
                    continue;
                }

                hoveringButton = button;
                button.Hovering = true;
            }

            if (hoveringButton != null && mouse.LeftButton == ButtonState.Pressed &&
                _lastMouse.LeftButton == ButtonState.Released)
            {
                hoveringButton?.Click();
                ContentChest.Instance.Get<SoundEffect>("Sounds/menuHover").Play();
            }

            _lastMouse = mouse;
            base.Update(delta);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _scrollBox.Draw(spriteBatch);
            _scrollBox.DrawDebug(spriteBatch);


            spriteBatch.End();
        }
    }
}