using System;
using System.Linq;
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
    public class TitleMenu : Menu
    {
        private readonly Texture2D _background;
        private readonly Texture2D _menuButtons;
        private readonly Rectangle _titleImageSource;
        private readonly float _buttonScale;

        private float _titleYOffset;
        private MouseState _lastMouse;
        private float _lastDrag;
        public Image SignTopImage { get; private set; }

        public IClickable OptionsMenuButton { get; private set; }
        public IClickable NewGameButton { get; private set; }
        public IClickable LoadGameButton { get; private set; }
        public IClickable ExitGameButton { get; private set; }

        public ScrollBox ScrollBox { get; private set; }

        public TitleMenu()
        {
            _background = ContentChest.Instance.Get<Texture2D>("background");
            _titleImageSource = new Rectangle(0, 241, 258, 67);
            _titleYOffset = -(ViewManager.ViewPort.Height / 2.0f - 50);
            _buttonScale = 3f;

            _menuButtons = ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons");

            SignTopImage = new Image(
                new Sprite(_menuButtons, new Rectangle(0, 0, 192, 44)),
                new Vector2(ViewManager.ViewPort.Center().X,
                    ViewManager.ViewPort.Center().Y - ViewManager.ViewPort.Height / 4f), _buttonScale);

            var newButtonPosition = new Vector2(SignTopImage.Bounds.Left + 96 / 2f * _buttonScale,
                SignTopImage.Bounds.Bottom + 32 / 2f * _buttonScale);
            var newsPanelPosition = new Vector2(SignTopImage.Bounds.Right - 96 / 2f * _buttonScale,
                SignTopImage.Bounds.Bottom + 96 / 2f * _buttonScale);

            NewsPanelImage = new Image(
                new Sprite(_menuButtons, new Rectangle(192, 45, 96, 96)),
                newsPanelPosition, _buttonScale);


            ScrollBox = new ScrollBox(
                "{line} Welcome to Project Sanctuary! {line} Build your Sanctuary, Save Animals and Change the World! Your village needs you. \n The times are changing and they need your guidance. \n The animals will thank you. {line} Crafted with love by YetiFace. {line} ",
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

            ContentChest.Instance.Preload<SoundEffect>("Sounds/menuHover");
        }

        public Image NewsPanelImage { get; set; }

        public override void Update(float delta)
        {
            var mouse = Mouse.GetState();
            var mousePosition = new Vector2(mouse.X, mouse.Y);

            var mouseRectangle = new Rectangle((int) mousePosition.X, (int) mousePosition.Y, 1, 1);

            if (_titleYOffset < 0)
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


            if (ScrollBox.Dragging)
            {
                if (MathHelper.Distance(mouse.Y, _lastDrag) > 10)
                {
                    if (mouse.Y < _lastDrag)
                    {
                        ScrollBox.ScrollLine(-1);
                        _lastDrag = mouse.Y;
                    }
                    else
                    {
                        ScrollBox.ScrollLine(1);
                        _lastDrag = mouse.Y;
                    }
                }
            }
            else if (mouse.LeftButton == ButtonState.Pressed &&
                     _lastMouse.LeftButton == ButtonState.Released)
            {
                if (hoveringButton != null)
                {
                    hoveringButton.Click();
                    ContentChest.Instance.Get<SoundEffect>("Sounds/menuHover").Play();
                }
                else
                {
                    if (mouseRectangle.Intersects(ScrollBox.TopNibBounds()))
                    {
                        ScrollBox.ScrollLine(-1);
                    }
                    else if (mouseRectangle.Intersects(ScrollBox.BottomNibBounds()))
                    {
                        ScrollBox.ScrollLine(1);
                    }
                    else if (mouseRectangle.Intersects(ScrollBox.ScrollBarBounds()))
                    {
                        ScrollBox.Dragging = true;
                        Console.WriteLine("BEGIN DRAG");
                        _lastDrag = mouse.Y;
                    }
                }
            } else if (mouseRectangle.Intersects(ScrollBox.Bounds))
            {
                if (mouse.ScrollWheelValue < _lastMouse.ScrollWheelValue)
                {
                    ScrollBox.ScrollLine(1);
                } else if (mouse.ScrollWheelValue > _lastMouse.ScrollWheelValue)
                {
                    ScrollBox.ScrollLine(-1);
                }
            }

            if (mouse.LeftButton == ButtonState.Released)
            {
                ScrollBox.Dragging = false;
            }

            _lastMouse = mouse;
            base.Update(delta);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(_background, new Rectangle(0, 0, ViewManager.ViewPort.Width, ViewManager.ViewPort.Height),
                Color.White * 0.2f);
            var title = "Project Sanctuary";
            var font = ContentChest.Instance.Get<SpriteFont>("Fonts/TitleFont");

            spriteBatch.DrawString(
                ContentChest.Instance.Get<SpriteFont>("Fonts/TitleFont"),
                title,
                new Vector2(ViewManager.ViewPort.Center().X - font.MeasureString(title).X / 2,
                    SignTopImage.Bounds.Top + _titleYOffset -
                    font.MeasureString(title).Y / 2),
                Color.Black);

            spriteBatch.Draw(_menuButtons,
                ViewManager.ViewPort.Center() - new Vector2(0, ViewManager.ViewPort.Center().Y / 2.0f + _titleYOffset),
                _titleImageSource, Color.White, 0f,
                new Vector2(_titleImageSource.Width, _titleImageSource.Height) / 2.0f, 3f, SpriteEffects.None, 0.2f);

            foreach (var clickable in Clickables.Reverse())
            {
                clickable.Draw(spriteBatch);
            }

            SignTopImage.Draw(spriteBatch);
            NewsPanelImage.Draw(spriteBatch);

            ScrollBox.Draw(spriteBatch);

            var size = ContentChest.Instance.Get<SpriteFont>("Fonts/InterfaceFont").MeasureString("by YetiFace");

            spriteBatch.DrawString(ContentChest.Instance.Get<SpriteFont>("Fonts/InterfaceFont"), "by YetiFace",
                new Vector2(ViewManager.ViewPort.Center().X - size.X / 2f,
                    ViewManager.ViewPort.Bounds.Bottom - 10 - size.Y),
                Color.Black);
            spriteBatch.End();
        }

        public bool IsDebug { get; set; }
    }
}