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
        private readonly Texture2D _menuButtons;
        private readonly IUserInterface _userInterface;

        private float _buttonScale;
        public IClickable NewGameButton { get; private set; }
        public IClickable LoadGameButton { get; private set; }
        private IClickable ExitGameButton { get; set; }
        private ScrollBox ScrollBox { get; set; }
        private Image SignTopImage { get; set; }
        private Image NewsPanelImage { get; set; }
        private TextBlock TitleTextBlock { get; set; }

        public TitleMenu()
        {
            _userInterface = new UserInterface();

            _menuButtons = ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons");
            ContentChest.Instance.Preload<SoundEffect>("Sounds/menuHover");

            SetupButtons();
        }
        
        private void SetupButtons()
        {
            Clickables.Clear();

            _buttonScale = 3f;

            const string title = "Project Sanctuary";
            var font = ContentChest.Instance.Get<SpriteFont>("Fonts/TitleFont");

            SignTopImage = _userInterface.AddWidget(new Image(
                new Sprite(_menuButtons, new Rectangle(0, 0, 192, 44)),
                new Vector2(ViewManager.ViewPort.Center().X,
                    ViewManager.ViewPort.Center().Y - ViewManager.ViewPort.Height / 6f), _buttonScale));

            TitleTextBlock = new TextBlock( title, new Vector2(ViewManager.ViewPort.Center().X - font.MeasureString(title).X / 2,
                SignTopImage.Bounds.Top -
                font.MeasureString(title).Y), font, Color.Black, null);

            var newButtonPosition = new Vector2(SignTopImage.Bounds.Left + 96 / 2f * _buttonScale,
                SignTopImage.Bounds.Bottom + 32 / 2f * _buttonScale);
            var newsPanelPosition = new Vector2(SignTopImage.Bounds.Right - 96 / 2f * _buttonScale,
                SignTopImage.Bounds.Bottom + 96 / 2f * _buttonScale);

            NewsPanelImage = SignTopImage.AddChild(new Image(
                new Sprite(_menuButtons, new Rectangle(192, 45, 96, 96)),
                newsPanelPosition, _buttonScale));

            ScrollBox = new ScrollBox(
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

            ExitGameButton.OnClick += () => { ViewManager.Instance.RequestExit(); };

            Clickables.Add(NewGameButton);
            Clickables.Add(LoadGameButton);
            Clickables.Add(ExitGameButton);

            SignTopImage.AddChild(TitleTextBlock);
            SignTopImage.AddChild(ScrollBox);
            SignTopImage.AddChild(NewGameButton as IWidget);
            SignTopImage.AddChild(LoadGameButton as IWidget);
            SignTopImage.AddChild(ExitGameButton as IWidget);
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

            SanctuaryGame.Cursor.ActiveState = hoveringButton != null ? CursorState.Hand : CursorState.Cursor;

            if (ScrollBox.Dragging)
            {
                if (SanctuaryGame.MouseManager.Dragged(6))
                {
                    if (SanctuaryGame.MouseManager.Drag < 0)
                    {
                        ScrollBox.ScrollLine(-1);
                    }
                    else if (SanctuaryGame.MouseManager.Drag > 0)
                    {
                        ScrollBox.ScrollLine(1);
                    }
                }
            }

            else if (SanctuaryGame.MouseManager.LeftClicked)
            {
                if (hoveringButton != null)
                {
                    hoveringButton.Click();
                    ContentChest.Instance.Get<SoundEffect>("Sounds/menuHover").Play();
                }
                else
                {
                    if (SanctuaryGame.MouseManager.MouseBounds.Intersects(ScrollBox.TopNibBounds()))
                    {
                        ScrollBox.ScrollLine(-1);
                    }
                    else if (SanctuaryGame.MouseManager.MouseBounds.Intersects(ScrollBox.BottomNibBounds()))
                    {
                        ScrollBox.ScrollLine(1);
                    }
                    else if (SanctuaryGame.MouseManager.MouseBounds.Intersects(ScrollBox.ScrollBarBounds()))
                    {
                        ScrollBox.Dragging = true;
                    }
                }
            }
            else if (SanctuaryGame.MouseManager.MouseBounds.Intersects(ScrollBox.Bounds))
            {
                if (SanctuaryGame.MouseManager.ScrolledUp)
                {
                    ScrollBox.ScrollLine(1);
                }
                else if (SanctuaryGame.MouseManager.ScrolledDown)
                {
                    ScrollBox.ScrollLine(-1);
                }
            }

            if (SanctuaryGame.MouseManager.LeftReleased)
            {
                ScrollBox.Dragging = false;
            }

            base.Update(delta);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _userInterface.Draw(spriteBatch);

            var size = ContentChest.Instance.Get<SpriteFont>("Fonts/InterfaceFont").MeasureString("by YetiFace");

            spriteBatch.DrawString(ContentChest.Instance.Get<SpriteFont>("Fonts/InterfaceFont"), "by YetiFace",
                new Vector2(ViewManager.ViewPort.Center().X - size.X / 2f,
                    ViewManager.ViewPort.Bounds.Bottom - 10 - size.Y),
                Color.Black);
            spriteBatch.End();
        }

        public override void WindowResized()
        {
            SetupButtons();
        }
    }
}