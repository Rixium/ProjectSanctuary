using System;
using System.Collections.Generic;
using System.Linq;
using Application.Configuration;
using Application.Content;
using Application.Content.ContentLoader;
using Application.Content.ContentTypes;
using Application.Graphics;
using Application.Input;
using Application.Player;
using Application.UI;
using Application.UI.Widgets;
using Application.Utils;
using Application.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Menus
{
    public class CharacterCreationMenu : Menu
    {
        private readonly IContentChest _contentChest;
        private readonly IPlayerMaker _playerMaker;
        private readonly IViewPortManager _viewPortPortManager;
        private readonly IKeyboardDispatcher _keyboardDispatcher;
        private readonly IUserInterface _userInterface;
        private readonly IOptionsManager _optionsManager;

        private readonly IContentLoader<Hair> _hairContentLoader;
        private readonly IContentLoader<Head> _headContentLoader;
        private readonly IContentLoader<Eyes> _eyeContentLoader;

        private Texture2D _menuButtons;
        private float _buttonScale;
        private Panel _panel;

        private ColorPicker _hairColorPicker;
        private Rectangle _hairSource;
        private Color _hairColor;

        private ColorPicker _bodyColorPicker;
        private Color _bodyColor;

        private Texture2D _playerEyes;
        private Texture2D _playerHair;
        private Texture2D _playerBody;
        private Vector2 _playerPosition;

        private Hair[] _hair;
        private Head[] _heads;
        private Eyes[] _eyes;

        public TexturedButton BackButton { get; private set; }
        public TexturedButton DoneButton { get; set; }
        private DropDownBox PronounDropDown { get; set; }
        private DropDownBox PlayerHairDropDown { get; set; }
        private TextBox NameTextBox { get; set; }

        public CharacterCreationMenu(IContentChest contentChest, IPlayerMaker playerMaker,
            IViewPortManager viewPortManager, IKeyboardDispatcher keyboardDispatcher, IUserInterface userInterface,
            IOptionsManager optionsManager, IContentLoader<Hair> hairContentLoader,
            IContentLoader<Head> headContentLoader,
            IContentLoader<Eyes> eyeContentLoader)
        {
            _contentChest = contentChest;
            _playerMaker = playerMaker;
            _viewPortPortManager = viewPortManager;
            _keyboardDispatcher = keyboardDispatcher;
            _userInterface = userInterface;
            _optionsManager = optionsManager;
            _hairContentLoader = hairContentLoader;
            _headContentLoader = headContentLoader;
            _eyeContentLoader = eyeContentLoader;
        }


        public override void Initialize()
        {
            _menuButtons = _contentChest.Get<Texture2D>("UI/title_menu_buttons");
            _buttonScale = 3f;
            _hair = _hairContentLoader.GetContent("assets/characters/player_hair.json").ToArray();
            _heads = _headContentLoader.GetContent("assets/characters/player_heads.json").ToArray();
            _eyes = _eyeContentLoader.GetContent("assets/characters/player_eyes.json").ToArray();

            SetupUserInterface();
        }

        private void SetupUserInterface()
        {
            var interfaceFont = _contentChest.Get<SpriteFont>("Fonts/InterfaceFont");
            var inputBoxFont = _contentChest.Get<SpriteFont>("Fonts/InputBoxFont");

            var portraitTexture = _contentChest.Get<Texture2D>("portrait_background");
            var portraitImage = new Sprite(portraitTexture);

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

            var panelWidth = (int) (30 + 200 + 30 + portraitImage.Texture.Width * _buttonScale + 30);
            _panel = new Panel(nineSlice,
                new Rectangle(
                    (int) (_viewPortPortManager.ViewPort.Center().X - panelWidth / 2f),
                    (int) (_viewPortPortManager.ViewPort.Center().Y - (500 + 30 + 22 * _buttonScale) / 2f), panelWidth,
                    500), _buttonScale);

            BackButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 162, 78, 22)),
                new Sprite(_menuButtons, new Rectangle(78, 162, 78, 22)),
                new Vector2(_panel.BottomLeft().X + 78 * _buttonScale / 2f,
                    _panel.BottomLeft().Y + (22 / 2f * _buttonScale) + 10), _buttonScale);
            DoneButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 140, 78, 22)),
                new Sprite(_menuButtons, new Rectangle(78, 140, 78, 22)),
                new Vector2(_panel.BottomRight().X - 78 * _buttonScale / 2f,
                    _panel.BottomLeft().Y + (22 / 2f * _buttonScale) + 10), _buttonScale);

            var nameSectionPosition = new Vector2(_panel.Left() + 30,
                _panel.Top() + 30);
            var nameTextBoxTitle = new TextBlock("Name", nameSectionPosition, interfaceFont, Color.White, Color.Black);
            NameTextBox = new TextBox(_contentChest, _keyboardDispatcher,
                nameSectionPosition + new Vector2(0, interfaceFont.MeasureString("Name").Y + 10),
                inputBoxFont, 200)
            {
                Value = _playerMaker.Name
            };
            NameTextBox.Changed += OnPlayerNameSet;

            var pronounSectionPosition = new Vector2(_panel.Left() + 30, NameTextBox.Bounds.Bottom + 10);
            var pronounTextBoxTitle =
                new TextBlock("Pronouns", pronounSectionPosition, interfaceFont, Color.White, Color.Black);
            PronounDropDown = new DropDownBox(_contentChest, inputBoxFont,
                pronounSectionPosition + new Vector2(0, interfaceFont.MeasureString("Pronouns").Y + 10),
                _optionsManager.PronounOptions.Pronouns.Select(x =>
                    $"{x.Subjective}/{x.Objective}").ToArray(), 200);
            PronounDropDown.Hover += OnPronounSelect;
            PronounDropDown.SelectedIndex = _playerMaker.Pronouns;

            var characterPanel = new Image(portraitImage,
                new Vector2(
                    PronounDropDown.Bounds.Right + 30 +
                    (_panel.Right() - 30 - (PronounDropDown.Bounds.Right + 30)) / 2f -
                    portraitImage.Center.X * _buttonScale,
                    nameTextBoxTitle.Top()), _buttonScale);

            var hairText = new TextBlock("Hair Style",
                new Vector2(PronounDropDown.Left(), PronounDropDown.BottomLeft().Y + 10), interfaceFont, Color.White,
                Color.Black);
            PlayerHairDropDown = new DropDownBox(_contentChest, inputBoxFont,
                new Vector2(hairText.Left(), hairText.BottomLeft().Y + 10),
                _hair.Select(x => x.Name).ToArray(), 200);
            PlayerHairDropDown.Hover += OnHairSelect;
            PlayerHairDropDown.SelectedIndex = _playerMaker.Hair;
            var hairColor = new TextBlock("Hair Color",
                new Vector2(PlayerHairDropDown.Left(), PlayerHairDropDown.BottomLeft().Y + 40), interfaceFont,
                Color.White,
                Color.Black);

            _panel.AddChild(pronounTextBoxTitle);
            _panel.AddChild(nameTextBoxTitle);
            _panel.AddChild(NameTextBox);
            _panel.AddChild(characterPanel);
            _panel.AddChild(BackButton);
            _panel.AddChild(DoneButton);
            _panel.AddChild(hairText);
            _panel.AddChild(hairColor);

            _hairColorPicker = _panel.AddChild(
                new ColorPicker(_contentChest, new Vector2(hairColor.BottomLeft().X, hairColor.BottomLeft().Y + 10),
                    PronounDropDown.Bounds.Width, 25, _buttonScale));
            _hairColorPicker.SetValues(9.5f, 70.5f, 38.5f);
            _hairColorPicker.Changed += OnHairColorChanged;

            var bodyColor = new TextBlock("Body Color",
                new Vector2(_hairColorPicker.Right() + 30, hairColor.Top()), interfaceFont, Color.White,
                Color.Black);
            _bodyColorPicker = _panel.AddChild(
                new ColorPicker(_contentChest, new Vector2(bodyColor.BottomLeft().X, bodyColor.BottomLeft().Y + 10),
                    PronounDropDown.Bounds.Width, 25, _buttonScale));
            _bodyColorPicker.SetValues(5f, 21.5f, 100f);
            _bodyColorPicker.Changed += OnBodyColorChanged;

            _panel.AddChild(bodyColor);
            _panel.AddChild(PlayerHairDropDown);
            _panel.AddChild(PronounDropDown);

            _playerEyes = _contentChest.Get<Texture2D>("Characters/player_eyes");
            _playerHair = _contentChest.Get<Texture2D>("Characters/player_hair");
            _playerBody = _contentChest.Get<Texture2D>("Characters/player_body");
            _playerPosition = characterPanel.Center() - new Vector2(16 * _buttonScale / 2f,
                32 * _buttonScale / 2f - 30f);

            _userInterface.AddWidget(_panel);

            _hairColor = _hairColorPicker.GetColor();
            _bodyColor = _bodyColorPicker.GetColor();

            DoneButton.OnClick += () =>
            {
                Console.WriteLine("Saving Settings");
                _playerMaker.SetHair(PlayerHairDropDown.SelectedIndex);
                _playerMaker.SetName(NameTextBox.GetValue());
                _playerMaker.SetPronouns(PronounDropDown.SelectedIndex);
                _playerMaker.SetBodyColor(_bodyColorPicker.GetColor());
                _playerMaker.SetHairColor(_hairColorPicker.GetColor());
            };
        }

        private void OnPronounSelect(int index) => _playerMaker.SetPronouns(index);

        private void OnPlayerNameSet(string name) => _playerMaker.SetName(name);

        private void OnBodyColorChanged()
        {
            _bodyColor = _bodyColorPicker.GetColor();
            _playerMaker.SetBodyColor(_bodyColor);
        }

        private void OnHairColorChanged()
        {
            _hairColor = _hairColorPicker.GetColor();
            _playerMaker.SetHairColor(_hairColor);
        }

        private void OnHairSelect(int index)
        {
            _hairSource = _hair[index].Source;
            _hairSource.Width = 32;
            _hairSource.Height = 32;
            _playerMaker.SetHair(index);
        }

        public override void Update(float delta)
        {
            _userInterface.Update(delta);
            base.Update(delta);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _userInterface.Draw(spriteBatch);
            DrawCharacter(spriteBatch);
            spriteBatch.End();
        }

        private void DrawCharacter(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerBody, _playerPosition, new Rectangle(0, 0, 16, 32), _bodyColor, 0f, Vector2.Zero,
                _buttonScale,
                SpriteEffects.None, 0f);
            spriteBatch.Draw(_contentChest.Get<Texture2D>("characters/player_heads"), _playerPosition,
                new Rectangle(0, 0, 16, 32), _bodyColor, 0f, Vector2.Zero,
                _buttonScale,
                SpriteEffects.None, 0f);
            spriteBatch.Draw(_contentChest.Get<Texture2D>("characters/player_eyes"), _playerPosition,
                new Rectangle(0, 0, 16, 32), Color.White, 0f, Vector2.Zero,
                _buttonScale,
                SpriteEffects.None, 0f);
            spriteBatch.Draw(_playerHair, _playerPosition - new Vector2(32 / 2f + 8, 32 / 2f + 8), _hairSource,
                _hairColor, 0f,
                Vector2.Zero,
                _buttonScale, SpriteEffects.None, 0f);
        }

        public override void WindowResized()
        {
            SetupUserInterface();
            base.WindowResized();
        }

        public override void Finish()
        {
            PlayerHairDropDown.Hover -= OnHairSelect;
            base.Finish();
        }
    }
}