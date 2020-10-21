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
    public class CharacterCreationMenu : Menu
    {
        private readonly Texture2D _background;
        private readonly Texture2D _menuButtons;
        private readonly float _buttonScale;
        private Panel _panel;

        private TextBlock _characterCreationTitle;
        private MouseState _lastMouse;

        public TexturedButton BackButton { get; set; }
        public TextBox NameTextBox { get; set; }

        public CharacterCreationMenu()
        {
            _background = ContentChest.Instance.Get<Texture2D>("background");
            _menuButtons = ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons");
            _buttonScale = 3f;

            SetupButtons();
        }

        private void SetupButtons()
        {
            Clickables.Clear();

            var font = ContentChest.Instance.Get<SpriteFont>("Fonts/TitleFont");
            var interfaceFont = ContentChest.Instance.Get<SpriteFont>("Fonts/InterfaceFont");
            
            _characterCreationTitle =
                new TextBlock("Character Creation",
                    new Vector2(
                        ViewManager.ViewPort.TitleSafeArea.Center.X -
                        TextHelpers.TextWidth(font, "Character Creation").Half(),
                        ViewManager.ViewPort.TitleSafeArea.Top + 10),
                    font,
                    Color.White,
                    Color.Black
                );

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

            const int panelWidth = 400;

            _panel = new Panel(nineSlice,
                new Rectangle(
                    (int) (ViewManager.ViewPort.Center().X - panelWidth / 2f),
                    (int) (ViewManager.ViewPort.Center().Y - 250), panelWidth,
                    500), _buttonScale);
                
            BackButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 166, 96, 22)),
                new Sprite(_menuButtons, new Rectangle(96, 166, 96, 22)),
                new Vector2(_panel.BottomLeft().X + 96 * _buttonScale / 2f,
                    _panel.BottomLeft().Y + (22 / 2f * _buttonScale) + 10), _buttonScale);

            NameTextBox = new TextBox(ViewManager.ViewPort.Center(), interfaceFont, 200);
            Clickables.Add(BackButton);

        }


        public override void Update(float delta)
        {
            var mouse = Mouse.GetState();
            var (x, y) = new Vector2(mouse.X, mouse.Y);
            var mouseRectangle = new Rectangle((int) x, (int) y, 1, 1);

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
            spriteBatch.Draw(_background, new Rectangle(0, 0, ViewManager.ViewPort.Width, ViewManager.ViewPort.Height),
                Color.White * 0.2f);

            foreach (var clickable in Clickables)
            {
                clickable.Draw(spriteBatch);
            }

            _characterCreationTitle.Draw(spriteBatch);

            _panel.Draw(spriteBatch);
            
            NameTextBox.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void WindowResized()
        {
            SetupButtons();
            base.WindowResized();
        }
    }
}