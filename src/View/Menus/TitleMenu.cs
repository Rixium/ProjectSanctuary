using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectSanctuary.View.Content;
using ProjectSanctuary.View.Utils;

namespace ProjectSanctuary.View.Menus
{
    public class TitleMenu : Menu
    {
        
        private const int SelectedXOffset = 48;
        
        private KeyboardState _lastKeyboardState;
        private Texture2D _menuButtons;
        private Rectangle _titleImageSource;
        private float titleYOffset;
        private Rectangle _signTopSource;
        private Rectangle _newButtonSource;
        private Rectangle _loadButtonSource;
        private Rectangle _exitButtonSource;
        private int selectedButton;

        public TitleMenu()
        {
            _menuButtons = ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons");
            _titleImageSource = new Rectangle(0, 241, 258, 67);
            titleYOffset = ViewManager.ViewPort.Height / 2.0f - 50;

            _signTopSource = new Rectangle(0, 0, 48, 21);
            _newButtonSource = new Rectangle(0, 21, 48, 14);
            _loadButtonSource = new Rectangle(0, 35, 48, 17);
            _exitButtonSource = new Rectangle(0, 52, 48, 17);
        }
        
        public override void Update(float delta)
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            var mouseRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            if (titleYOffset > 0)
            {
                titleYOffset = MathHelper.Lerp(titleYOffset, 0, delta);
            }

            if (keyboardState.IsKeyDown(Keys.S) && _lastKeyboardState.IsKeyUp(Keys.S)) 
            {
                selectedButton++;

                if (selectedButton > 2)
                {
                    selectedButton = 0;
                }
            } else if (keyboardState.IsKeyDown(Keys.W) && _lastKeyboardState.IsKeyUp(Keys.W))
            {
                selectedButton--;
                
                if (selectedButton < 0)
                {
                    selectedButton = 2;
                }
            }
            
            _lastKeyboardState = keyboardState;
            
            base.Update(delta);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_menuButtons,
                new Vector2(ViewManager.ViewPort.Width,
                    ViewManager.ViewPort.Height - ViewManager.ViewPort.Height / 2.0f) / 2.0f +
                new Vector2(0, titleYOffset),
                _titleImageSource, Color.White, 0f,
                new Vector2(_titleImageSource.Width, _titleImageSource.Height) / 2.0f, 2f, SpriteEffects.None, 0.2f);

            const float zoom = 3f;

            spriteBatch.Draw(_menuButtons, new Vector2(100, 100), _signTopSource, Color.White, 0, Vector2.Zero, zoom,
                SpriteEffects.None, 0.2f);

            if (selectedButton == 0)
            {
                spriteBatch.Draw(_menuButtons, new Vector2(100, 100 + _signTopSource.Height * zoom),
                    _newButtonSource.Add(SelectedXOffset, 0),
                    Color.White, 0, Vector2.Zero, zoom, SpriteEffects.None, 0.3f);
            }
            else
            {
                spriteBatch.Draw(_menuButtons, new Vector2(100, 100 + _signTopSource.Height * zoom), _newButtonSource,
                    Color.White, 0, Vector2.Zero, zoom, SpriteEffects.None, 0.3f);
            }

            if (selectedButton == 1)
            {
                spriteBatch.Draw(_menuButtons,
                    new Vector2(100, 100 + _signTopSource.Height * zoom + _newButtonSource.Height * zoom),
                    _loadButtonSource.Add(SelectedXOffset, 0), Color.White, 0, Vector2.Zero / 2f, zoom,
                    SpriteEffects.None, 0.2f);
            }
            else
            {
                spriteBatch.Draw(_menuButtons,
                    new Vector2(100, 100 + _signTopSource.Height * zoom + _newButtonSource.Height * zoom),
                    _loadButtonSource, Color.White, 0, Vector2.Zero / 2f, zoom, SpriteEffects.None, 0.2f);
            }

            if (selectedButton == 2)
            {
                spriteBatch.Draw(_menuButtons,
                    new Vector2(100,
                        100 + _signTopSource.Height * zoom +
                        (_newButtonSource.Height + _loadButtonSource.Height) * zoom),
                    _exitButtonSource.Add(SelectedXOffset, 0), Color.White, 0,
                    Vector2.Zero, zoom, SpriteEffects.None, 0.2f);
            }
            else
            {
                spriteBatch.Draw(_menuButtons,
                    new Vector2(100,
                        100 + _signTopSource.Height * zoom +
                        (_newButtonSource.Height + _loadButtonSource.Height) * zoom), _exitButtonSource, Color.White, 0,
                    Vector2.Zero, zoom, SpriteEffects.None, 0.2f);
            }


            spriteBatch.End();
        }
    }
}