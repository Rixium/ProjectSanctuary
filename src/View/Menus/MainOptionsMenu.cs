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
    internal class MainOptionsMenu : Menu
    {
        private Rectangle _titleImageSource;
        private float _titleYOffset;
        private Texture2D _menuButtons;
        private Sprite _signTopSprite;
        private Vector2 _signTopPosition;
        private TexturedButton _newButton;
        private MouseState _lastMouse;
        private SpriteFont _interfaceFont;

        public MainOptionsMenu()
        {
            _titleImageSource = new Rectangle(0, 241, 258, 67);
            _titleYOffset = ViewManager.ViewPort.Height / 2.0f - 50;

            _menuButtons = ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons");

            _signTopSprite = new Sprite(_menuButtons, new Rectangle(0, 0, 96, 44));
            _signTopPosition = new Vector2(ViewManager.ViewPort.Center().X,
                ViewManager.ViewPort.Center().Y);

            _interfaceFont = ContentChest.Instance.Get<SpriteFont>("Fonts/InterfaceFont");
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
                hoveringButton?.Click();
                ContentChest.Instance.Get<SoundEffect>("Sounds/menuHover").Play();
            }

            _lastMouse = mouse;
            base.Update(delta);
        }

        public IClickable BackButton { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _newButton.Draw(spriteBatch);

            var i = 0;
            foreach (var control in ViewManager.Instance.GetControls())
            {
                spriteBatch.DrawString(_interfaceFont, control,
                    new Vector2(_signTopPosition.X, _signTopPosition.Y + ++i * 15), Color.Black);
            }

            spriteBatch.End();
        }
    }
}