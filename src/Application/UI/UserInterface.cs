using Application.Input;
using Application.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.UI
{
    public enum MouseScrollDirection
    {
        Up,
        Down
    }

    public class UserInterface : IUserInterface
    {
        private readonly MonoGameMouseManager _mouseManager;
        private Rectangle _mouseRectangle;
        private MouseState _mouseState;

        public IWidget Root { get; private set; }

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

                if (_mouseManager.LeftClicked)
                {
                    Root?.MouseClick(_mouseRectangle);
                }

                if (_mouseManager.LeftHeld)
                {
                    Root?.MouseHeld(_mouseRectangle);
                }

                if (_mouseManager.LeftReleased)
                {
                    Root?.MouseReleased(_mouseRectangle);
                }
                
                if (_mouseManager.Dragging)
                {
                    Root?.MouseDragged(_mouseRectangle, _mouseManager.Drag.X, _mouseManager.Drag.Y);
                }

                if (_mouseManager.ScrolledUp)
                {
                    Root?.MouseScrolled(_mouseRectangle, MouseScrollDirection.Up);
                }
                
                if (_mouseManager.ScrolledDown)
                {
                    Root?.MouseScrolled(_mouseRectangle, MouseScrollDirection.Down);
                }

                _mouseState = value;
            }
        }

        public UserInterface()
        {
            _mouseManager = new MonoGameMouseManager();
            _mouseRectangle = new Rectangle(0, 0, 1, 1);
            MouseState = Mouse.GetState();
        }

        public void Update(float delta)
        {
            UpdateMouse(delta);
        }

        private void UpdateMouse(float delta)
        {
            _mouseManager.Update(delta);
            MouseState = Mouse.GetState();
        }

        public void Draw(SpriteBatch spriteBatch) => Root?.Draw(spriteBatch);

        public T AddWidget<T>(T widget) where T : IWidget
        {
            Root = widget;
            return widget;
        }
    }
}