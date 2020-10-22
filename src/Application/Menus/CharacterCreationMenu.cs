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
        private readonly Texture2D _menuButtons;
        private readonly float _buttonScale;
        private Panel _panel;

        private MouseState _lastMouse;

        public TexturedButton BackButton { get; set; }
        public TextBlock NameTextBoxTitle { get; set; }
        public TextBox NameTextBox { get; set; }
        public TextBlock PronounTextBoxTitle { get; set; }
        public DropDownBox PronounDropDown { get; set; }

        public CharacterCreationMenu()
        {
            _menuButtons = ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons");
            _buttonScale = 3f;

            SetupButtons();
        }

        private void SetupButtons()
        {
            Clickables.Clear();

            var interfaceFont = ContentChest.Instance.Get<SpriteFont>("Fonts/InterfaceFont");
            var inputBoxFont = ContentChest.Instance.Get<SpriteFont>("Fonts/InputBoxFont");

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

            const int panelWidth = 550;

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

            var nameSectionPosition = new Vector2(_panel.Center().X,
                _panel.Top() + 30);
            NameTextBoxTitle = new TextBlock("Name",
                nameSectionPosition - new Vector2(interfaceFont.MeasureString("Name").X / 2f, 0), interfaceFont,
                Color.White, Color.Black);
            NameTextBox =
                new TextBox(nameSectionPosition + new Vector2(-100, interfaceFont.MeasureString("Name").Y + 10),
                    inputBoxFont, 200);

            var pronounSectionPosition = new Vector2(_panel.Center().X, NameTextBox.Bounds.Bottom + 10);
            PronounTextBoxTitle = new TextBlock("Pronouns",
                pronounSectionPosition - new Vector2(interfaceFont.MeasureString("Pronouns").X / 2f, 0), interfaceFont,
                Color.White, Color.Black);
            PronounDropDown = new DropDownBox(inputBoxFont,
                pronounSectionPosition + new Vector2(-100, interfaceFont.MeasureString("Pronouns").Y + 10),
                new[] {"He/Him", "She/Her"}, 200);

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

            NameTextBox.Update(delta);
            PronounDropDown.Update(delta);

            _lastMouse = mouse;
            base.Update(delta);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var clickable in Clickables)
            {
                clickable.Draw(spriteBatch);
            }

            _panel.Draw(spriteBatch);

            NameTextBoxTitle.Draw(spriteBatch);
            NameTextBox.Draw(spriteBatch);

            PronounTextBoxTitle.Draw(spriteBatch);
            PronounDropDown.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void WindowResized()
        {
            SetupButtons();
            base.WindowResized();
        }
    }
}