using System;
using Application.Content;
using Application.Content.Aseprite;
using Application.Content.ContentLoader;
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
        private const string Title = "Project Sanctuary";

        private readonly IContentChest _contentChest;
        private readonly IViewPortManager _viewPortManager;
        private readonly IUserInterface _userInterface;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;

        private float _buttonScale;
        public IClickable NewGameButton { get; private set; }
        public IClickable LoadGameButton { get; private set; }
        private IClickable ExitGameButton { get; set; }
        private ScrollBox ScrollBox { get; set; }
        private Image SignTopImage { get; set; }
        private Image NewsPanelImage { get; set; }
        private TextBlock TitleTextBlock { get; set; }

        public TitleMenu(IContentChest contentChest, IViewPortManager viewPortManager, IUserInterface userInterface,
            IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _contentChest = contentChest;
            _viewPortManager = viewPortManager;
            _userInterface = userInterface;
            _spriteMapLoader = spriteMapLoader;
        }

        public override void Initialize()
        {
            _contentChest.Preload<SoundEffect>("Sounds/menuHover");
            SetupUserInterface();
        }

        private void SetupUserInterface()
        {
            var mainMenuSpriteMap = _spriteMapLoader.GetContent("assets/UI/title_menu_buttons.json");
            _buttonScale = 3f;
            var font = _contentChest.Get<SpriteFont>("Fonts/TitleFont");

            // Main Menu Heading
            var signTopSprite = mainMenuSpriteMap.CreateSpriteFromRegion("Title_Button_Heading");
            SignTopImage = _userInterface.AddWidget(new Image(signTopSprite,
                new Vector2(_viewPortManager.ViewPort.Center().X - signTopSprite.Source.Width * _buttonScale / 2f,
                    _viewPortManager.ViewPort.Center().Y - _viewPortManager.ViewPort.Height / 6f), _buttonScale));

            // Title Text
            TitleTextBlock = new TextBlock(Title,
                new Vector2(_viewPortManager.ViewPort.Center().X - font.MeasureString(Title).X / 2,
                    SignTopImage.Bounds.Top -
                    font.MeasureString(Title).Y), font, Color.Black, null);

            // News Section
            var newsPanelSprite = mainMenuSpriteMap.CreateSpriteFromRegion("Main_Menu_Empty");
            var newsPanelPosition = new Vector2(SignTopImage.Bounds.Right - newsPanelSprite.Source.Width * _buttonScale,
                SignTopImage.Bounds.Bottom);
            NewsPanelImage = SignTopImage.AddChild(new Image(newsPanelSprite,
                newsPanelPosition, _buttonScale));
            ScrollBox = new ScrollBox(_contentChest,
                "{line} Welcome to Project Sanctuary! {line} Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi congue finibus maximus. Maecenas rhoncus malesuada eros vitae tincidunt. Nam suscipit, justo ac gravida rhoncus, ante neque auctor urna, a egestas dui odio eget ante. Aenean nec eros nisi. Nam bibendum viverra tincidunt. Phasellus elementum urna nibh, ac egestas nibh pellentesque vitae. Nulla in mollis nisl. Vivamus nec mauris rutrum magna sollicitudin venenatis et a enim. Phasellus quis mi ex. {line} ",
                NewsPanelImage.Bounds.Add(5 * _buttonScale, 14 * _buttonScale, -11 * _buttonScale, -20 * _buttonScale));

            // New Game Button
            var newButtonPosition = new Vector2(SignTopImage.Bounds.Left,
                SignTopImage.Bounds.Bottom);
            NewGameButton = new TexturedButton(
                mainMenuSpriteMap.CreateSpriteFromRegion("New_Off"),
                mainMenuSpriteMap.CreateSpriteFromRegion("New_On"),
                newButtonPosition, _buttonScale);
            NewGameButton.OnClick += () => { };

            // Load Game Button
            LoadGameButton = new TexturedButton(
                mainMenuSpriteMap.CreateSpriteFromRegion("Load_Off"),
                mainMenuSpriteMap.CreateSpriteFromRegion("Load_On"),
                newButtonPosition + new Vector2(0, NewGameButton.Height * _buttonScale), _buttonScale);
            LoadGameButton.OnClick += () => { };

            // Exit Game Button
            ExitGameButton = new TexturedButton(
                mainMenuSpriteMap.CreateSpriteFromRegion("Exit_Off"),
                mainMenuSpriteMap.CreateSpriteFromRegion("Exit_On"),
                newButtonPosition + new Vector2(0,
                    NewGameButton.Height * _buttonScale + LoadGameButton.Height * _buttonScale),
                _buttonScale);
            ExitGameButton.OnClick += () => Console.WriteLine("Closing Game...");

            var settingsSprite = mainMenuSpriteMap.CreateSpriteFromRegion("Settings_On");
            var settingsButton = new TexturedButton(
                mainMenuSpriteMap.CreateSpriteFromRegion("Settings_Off"), settingsSprite,
                new Vector2(_viewPortManager.ViewPort.Width - settingsSprite.Source.Width * _buttonScale - 10,
                    _viewPortManager.ViewPort.Height - settingsSprite.Source.Height * _buttonScale - 10), _buttonScale);
            
            // Add all elements to the parent.
            SignTopImage.AddChild(TitleTextBlock);
            SignTopImage.AddChild(ScrollBox);
            SignTopImage.AddChild(NewGameButton as IWidget);
            SignTopImage.AddChild(LoadGameButton as IWidget);
            SignTopImage.AddChild(ExitGameButton as IWidget);
            SignTopImage.AddChild(settingsButton);

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