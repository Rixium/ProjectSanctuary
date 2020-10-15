using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectSanctuary.View.Content;
using ProjectSanctuary.View.Graphics;
using ProjectSanctuary.View.UI;
using ProjectSanctuary.View.Utils;

namespace ProjectSanctuary.View.Menus
{
    public class TitleMenu : Menu
    {
        private readonly Sprite _signTopSprite;
        private readonly Vector2 _signTopPosition;
        private readonly Camera _camera;
        private readonly Texture2D _background;
        private readonly Texture2D _menuButtons;
        private readonly Rectangle _titleImageSource;

        private float _titleYOffset;
        private MouseState _lastMouse;

        public TitleMenu()
        {
            _camera = new Camera(ViewManager.ViewPort.Center());
            _camera.AdjustZoom(3);
            
            _background = ContentChest.Instance.Get<Texture2D>("background");
            _titleImageSource = new Rectangle(0, 241, 258, 67);
            _titleYOffset = ViewManager.ViewPort.Height / 2.0f - 50;

            _menuButtons = ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons");

            _signTopSprite = new Sprite(_menuButtons, new Rectangle(0, 0, 48, 22));
            _signTopPosition = new Vector2(_camera.ViewportWorldBoundary().Center.X,
                _camera.ViewportWorldBoundary().Center.Y);

            var newButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 22, 48, 14)),
                new Sprite(_menuButtons, new Rectangle(48, 22, 48, 14)),
                _signTopPosition + new Vector2(0, 18));

            newButton.OnClick += () => { };

            var loadButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 36, 48, 16)),
                new Sprite(_menuButtons, new Rectangle(48, 36, 48, 16)),
                _signTopPosition + new Vector2(0, 19 + newButton.Height));

            loadButton.OnClick += () => { };

            var exitButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 52, 48, 16)),
                new Sprite(_menuButtons, new Rectangle(48, 52, 48, 16)),
                _signTopPosition + new Vector2(0, 19 + newButton.Height + loadButton.Height));

            exitButton.OnClick += () => { ViewManager.Instance.RequestExit(); };

            Clickables.Add(newButton);
            Clickables.Add(loadButton);
            Clickables.Add(exitButton);

            ContentChest.Instance.Preload<SoundEffect>("Sounds/menuHover");
        }

        public override void Update(float delta)
        {
            var mouse = Mouse.GetState();
            var mousePosition = new Vector2(mouse.X, mouse.Y);
            mousePosition = Vector2.Transform(mousePosition, Matrix.Invert(_camera.TranslationMatrix));

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

            if (mouse.LeftButton == ButtonState.Pressed && _lastMouse.LeftButton == ButtonState.Released)
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

            spriteBatch.Draw(_background, new Rectangle(0, 0, ViewManager.ViewPort.Width, ViewManager.ViewPort.Height),
                Color.White * 0.2f);
            spriteBatch.Draw(_menuButtons,
                ViewManager.ViewPort.Center() - new Vector2(0, ViewManager.ViewPort.Center().Y / 2.0f + _titleYOffset),
                _titleImageSource, Color.White, 0f,
                new Vector2(_titleImageSource.Width, _titleImageSource.Height) / 2.0f, 3f, SpriteEffects.None, 0.2f);
            spriteBatch.End();

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.TranslationMatrix);

            foreach (var clickable in Clickables.Reverse())
            {
                clickable.Draw(spriteBatch);

                if (IsDebug)
                {
                    clickable.DrawDebug(spriteBatch);
                }
            }

            spriteBatch.Draw(_signTopSprite.Texture, _signTopPosition, _signTopSprite.Source, Color.White, 0f,
                _signTopSprite.Origin, 1f,
                SpriteEffects.None, 0);

            spriteBatch.End();
        }

        public bool IsDebug { get; set; }
    }
}