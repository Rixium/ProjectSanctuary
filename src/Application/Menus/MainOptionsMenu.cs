using System.Collections.Generic;
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
        private readonly ScrollBox _scrollBox;
        private readonly float _buttonScale;
        private readonly Texture2D _menuButtons;
        private Panel _panel;
        public IClickable BackButton { get; private set; }

        public MainOptionsMenu()
        {
            _titleYOffset = ViewManager.ViewPort.Height / 2.0f - 50;

            _scrollBox =
                new ScrollBox(
                    "Hello everyone, thanks for purchasing this amazing game.\nProject Sanctuary is a game about raising animals that have long been forgotten.",
                    new Rectangle(10, 10, 400, 400));

            _menuButtons = ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons");
            _buttonScale = 3f;

            SetupButtons();
        }

        private void SetupButtons()
        {
            Clickables.Clear();
            
            BackButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 166, 96, 22)),
                new Sprite(_menuButtons, new Rectangle(96, 166, 96, 22)),
                new Vector2(ViewManager.ViewPort.Bounds.Left + 10 + 96 * _buttonScale / 2f,
                    ViewManager.ViewPort.Bounds.Bottom - (22 / 2f * _buttonScale) - 10), _buttonScale);

            Clickables.Add(BackButton);

            var nineSlice = new NineSlice(_menuButtons, new Dictionary<Segment, Rectangle>
            {
                {Segment.TopLeft, new Rectangle(1, 189, 8, 9)},
                {Segment.Top, new Rectangle(10, 189, 1, 9)},
                {Segment.TopRight, new Rectangle(12, 189, 8, 9)},
                {Segment.Right, new Rectangle(12, 199, 8, 1)},
                {Segment.BottomRight, new Rectangle(12, 201, 8, 8)},
                {Segment.Bottom, new Rectangle(10, 201, 1, 8)},
                {Segment.BottomLeft, new Rectangle(1, 201, 8, 8)},
                {Segment.Left, new Rectangle(1, 199, 8, 1)},
                {Segment.Center, new Rectangle(10, 199, 1, 1)}
            });

            _panel = new Panel(nineSlice,
                new Rectangle(
                    (int) (ViewManager.ViewPort.Center().X - 250),
                    (int) (ViewManager.ViewPort.Center().Y - 250), 500,
                    500), _buttonScale);
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
                hoveringButton.Click();
                ContentChest.Instance.Get<SoundEffect>("Sounds/menuHover").Play();
            }

            _lastMouse = mouse;
            base.Update(delta);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            
            _scrollBox.Draw(spriteBatch);

            foreach (var clickable in Clickables)
            {
                clickable.Draw(spriteBatch);
            }
            
            _panel.Draw(spriteBatch);
            
            spriteBatch.End();
        }

        public override void WindowResized()
        {
            SetupButtons();
            base.WindowResized();
        }
    }
}