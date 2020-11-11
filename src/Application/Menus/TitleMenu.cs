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
    public class TitleMenu : Menu
    {
        private readonly IContentChest _contentChest;
        private readonly IViewPortManager _viewPortManager;
        private Texture2D _menuButtons;
        private IUserInterface _userInterface;

        private float _buttonScale;
        public IClickable NewGameButton { get; private set; }
        public IClickable LoadGameButton { get; private set; }
        private IClickable ExitGameButton { get; set; }
        private ScrollBox ScrollBox { get; set; }
        private Image SignTopImage { get; set; }
        private Image NewsPanelImage { get; set; }
        private TextBlock TitleTextBlock { get; set; }

        public TitleMenu(IContentChest contentChest, IViewPortManager viewPortManager)
        {
            _contentChest = contentChest;
            _viewPortManager = viewPortManager;
            _userInterface = new UserInterface();
        }

        public override void Initialize()
        {
            _menuButtons = _contentChest.Get<Texture2D>("UI/title_menu_buttons");
            _contentChest.Preload<SoundEffect>("Sounds/menuHover");

            SetupUserInterface();
        }

        private void SetupUserInterface()
        {
            _userInterface = new UserInterface();
            _buttonScale = 3f;

            const string title = "Project Sanctuary";
            var font = _contentChest.Get<SpriteFont>("Fonts/TitleFont");

            SignTopImage = _userInterface.AddWidget(new Image(
                new Sprite(_menuButtons, new Rectangle(0, 0, 192, 44)),
                new Vector2(_viewPortManager.ViewPort.Center().X,
                    _viewPortManager.ViewPort.Center().Y - _viewPortManager.ViewPort.Height / 6f), _buttonScale));

            TitleTextBlock = new TextBlock(title,
                new Vector2(_viewPortManager.ViewPort.Center().X - font.MeasureString(title).X / 2,
                    SignTopImage.Bounds.Top -
                    font.MeasureString(title).Y), font, Color.Black, null);

            var newButtonPosition = new Vector2(SignTopImage.Bounds.Left + 96 / 2f * _buttonScale,
                SignTopImage.Bounds.Bottom + 32 / 2f * _buttonScale);
            var newsPanelPosition = new Vector2(SignTopImage.Bounds.Right - 96 / 2f * _buttonScale,
                SignTopImage.Bounds.Bottom + 96 / 2f * _buttonScale);

            NewsPanelImage = SignTopImage.AddChild(new Image(
                new Sprite(_menuButtons, new Rectangle(192, 45, 96, 96)),
                newsPanelPosition, _buttonScale));

            ScrollBox = new ScrollBox(_contentChest,
                "{line} Welcome to Project Sanctuary! {line} Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi congue finibus maximus. Maecenas rhoncus malesuada eros vitae tincidunt. Nam suscipit, justo ac gravida rhoncus, ante neque auctor urna, a egestas dui odio eget ante. Aenean nec eros nisi. Nam bibendum viverra tincidunt. Phasellus elementum urna nibh, ac egestas nibh pellentesque vitae. Nulla in mollis nisl. Vivamus nec mauris rutrum magna sollicitudin venenatis et a enim. Phasellus quis mi ex. {line} ",
                NewsPanelImage.Bounds.Add(5 * _buttonScale, 14 * _buttonScale, -11 * _buttonScale, -20 * _buttonScale));

            NewGameButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 44, 96, 32)),
                new Sprite(_menuButtons, new Rectangle(96, 44, 96, 32)),
                newButtonPosition, _buttonScale);

            NewGameButton.OnClick += () => { };

            LoadGameButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 44 + 32, 96, 32)),
                new Sprite(_menuButtons, new Rectangle(96, 44 + 32, 96, 32)),
                newButtonPosition + new Vector2(0, NewGameButton.Height * _buttonScale), _buttonScale);

            LoadGameButton.OnClick += () => { };

            ExitGameButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 44 + 32 + 32, 96, 32)),
                new Sprite(_menuButtons, new Rectangle(96, 44 + 32 + 32, 96, 32)),
                newButtonPosition + new Vector2(0,
                    NewGameButton.Height * _buttonScale + LoadGameButton.Height * _buttonScale),
                _buttonScale);

            SignTopImage.AddChild(TitleTextBlock);
            SignTopImage.AddChild(ScrollBox);
            SignTopImage.AddChild(NewGameButton as IWidget);
            SignTopImage.AddChild(LoadGameButton as IWidget);
            SignTopImage.AddChild(ExitGameButton as IWidget);

            _userInterface.AddWidget(SignTopImage);
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

            var size = _contentChest.Get<SpriteFont>("Fonts/InterfaceFont").MeasureString("by YetiFace");

            spriteBatch.DrawString(_contentChest.Get<SpriteFont>("Fonts/InterfaceFont"), "by YetiFace",
                new Vector2(_viewPortManager.ViewPort.Center().X - size.X / 2f,
                    _viewPortManager.ViewPort.Bounds.Bottom - 10 - size.Y),
                Color.Black);
            spriteBatch.End();
        }

        public override void WindowResized()
        {
            SetupUserInterface();
        }

        public override void Finish()
        {
            
        }
    }
}