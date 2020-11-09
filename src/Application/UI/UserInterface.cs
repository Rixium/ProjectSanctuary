using Application.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.UI
{
    public class UserInterface : IUserInterface
    {
        public IWidget Root { get; private set; }

        private Rectangle _mouseRectangle;
        private MouseState _mouseState;

        private MouseState MouseState
        {
            set
            {
                if (_mouseState.X != value.X || _mouseState.Y != value.Y)
                {
                    _mouseRectangle.X = value.X;
                    _mouseRectangle.Y = value.Y;
                    Root?.MouseMove(_mouseRectangle);
                }

                if (value.LeftButton == ButtonState.Released && _mouseState.LeftButton == ButtonState.Pressed)
                {
                    Root?.MouseClick(_mouseRectangle);
                } else if (value.LeftButton == ButtonState.Pressed && _mouseState.LeftButton == ButtonState.Pressed)
                {
                    Root?.MouseHeld(_mouseRectangle);
                }

                _mouseState = value;
            }
        }

        public UserInterface()
        {
            _mouseRectangle = new Rectangle(0, 0, 1, 1);
            MouseState = Mouse.GetState();
        }

        public void Update(float delta)
        {
            UpdateMouse();
        }

        private void UpdateMouse() => MouseState = Mouse.GetState();

        public void Draw(SpriteBatch spriteBatch) => Root?.Draw(spriteBatch);

        public T AddWidget<T>(T widget) where T : IWidget
        {
            Root = widget;
            return widget;
        }
    }
}