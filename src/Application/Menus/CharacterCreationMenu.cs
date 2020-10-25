using System.Collections.Generic;
using System.Linq;
using Application.Content;
using Application.Graphics;
using Application.UI;
using Application.UI.Widgets;
using Application.Utils;
using Application.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Menus
{
    public class CharacterCreationMenu : Menu
    {
        private readonly Texture2D _menuButtons;
        private readonly float _buttonScale;
        private Panel _panel;
        private ColorPicker _colorPicker;

        public TexturedButton BackButton { get; private set; }
        private TexturedButton DoneButton { get; set; }
        private DropDownBox PronounDropDown { get; set; }
        private TextBox NameTextBox { get; set; }

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

            var panelWidth = (int) (96 * _buttonScale * 2 + 30);

            _panel = new Panel(nineSlice,
                new Rectangle(
                    (int) (ViewManager.ViewPort.Center().X - panelWidth / 2f),
                    (int) (ViewManager.ViewPort.Center().Y - (500 + 30 + 22 * _buttonScale) / 2f), panelWidth,
                    500), _buttonScale);

            BackButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 166, 96, 22)),
                new Sprite(_menuButtons, new Rectangle(96, 166, 96, 22)),
                new Vector2(_panel.BottomLeft().X + 96 * _buttonScale / 2f,
                    _panel.BottomLeft().Y + (22 / 2f * _buttonScale) + 10), _buttonScale);

            DoneButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 144, 96, 22)),
                new Sprite(_menuButtons, new Rectangle(96, 144, 96, 22)),
                new Vector2(_panel.BottomRight().X - 96 * _buttonScale / 2f,
                    _panel.BottomLeft().Y + (22 / 2f * _buttonScale) + 10), _buttonScale);

            var nameSectionPosition = new Vector2(_panel.Left() + 30,
                _panel.Top() + 30);

            var nameTextBoxTitle = new TextBlock("Name", nameSectionPosition, interfaceFont, Color.White, Color.Black);
            NameTextBox = new TextBox(nameSectionPosition + new Vector2(0, interfaceFont.MeasureString("Name").Y + 10),
                inputBoxFont, 200);

            var pronounSectionPosition = new Vector2(_panel.Left() + 30, NameTextBox.Bounds.Bottom + 10);
            var pronounTextBoxTitle =
                new TextBlock("Pronouns", pronounSectionPosition, interfaceFont, Color.White, Color.Black);
            PronounDropDown = new DropDownBox(inputBoxFont,
                pronounSectionPosition + new Vector2(0, interfaceFont.MeasureString("Pronouns").Y + 10),
                SanctuaryGame.OptionsManager.PronounOptions.Pronouns.Select(x =>
                    $"{x.Subjective}/{x.Objective}").ToArray(), 200);

            var hairColor = new TextBlock("Hair Color",
                new Vector2(PronounDropDown.Left(), PronounDropDown.BottomLeft().Y + 10), interfaceFont, Color.White,
                Color.Black);

            Clickables.Add(BackButton);
            Clickables.Add(DoneButton);

            _panel.AddChild(pronounTextBoxTitle);
            _panel.AddChild(PronounDropDown);
            _panel.AddChild(nameTextBoxTitle);
            _panel.AddChild(NameTextBox);
            _panel.AddChild(new Panel(nineSlice,
                new Rectangle(PronounDropDown.Bounds.Right + 30, _panel.Top() + 30,
                    _panel.Right() - PronounDropDown.Bounds.Right - 60,
                    _panel.Right() - PronounDropDown.Bounds.Right - 60),
                3f));
            _panel.AddChild(BackButton);
            _panel.AddChild(DoneButton);

            _panel.AddChild(hairColor);
            _colorPicker = _panel.AddChild(
                new ColorPicker(new Vector2(hairColor.BottomLeft().X, hairColor.BottomLeft().Y + 10),
                    PronounDropDown.Bounds.Width, 25, _buttonScale));
        }

        public override void Update(float delta)
        {
            IClickable hoveringButton = null;

            foreach (var button in Clickables)
            {
                button.Hovering = false;

                if (!button.Intersects(SanctuaryGame.MouseManager.MouseBounds))
                {
                    continue;
                }

                hoveringButton = button;
                button.Hovering = true;
            }

            if (hoveringButton != null && SanctuaryGame.MouseManager.LeftClicked)
            {
                hoveringButton.Click();
                ContentChest.Instance.Get<SoundEffect>("Sounds/menuHover").Play();
            }

            NameTextBox.Update();
            PronounDropDown.Update();

            base.Update(delta);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
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