using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Application.Input
{
    public class MonoGameMouseManager : IMouseManager
    {
        public Rectangle MouseBounds { get; private set; }
        public bool LeftClicked { get; private set; }
        public bool LeftReleased { get; private set; }
        public bool RightClicked { get; private set; }
        public bool RightReleased { get; private set; }
        public bool MiddleClicked { get; private set; }
        public bool MiddleReleased { get; private set; }
        public bool LeftHeld { get; private set; }
        public bool ScrolledDown { get; private set; }
        public bool ScrolledUp { get; private set; }

        private MouseState _lastMouseState;

        public void Update(float delta)
        {
            var mouseState = Mouse.GetState();

            MouseBounds = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            LeftClicked = mouseState.LeftButton == ButtonState.Pressed &&
                          _lastMouseState.LeftButton == ButtonState.Released;
            LeftReleased = mouseState.LeftButton == ButtonState.Released &&
                           _lastMouseState.LeftButton == ButtonState.Pressed;
            RightClicked = mouseState.RightButton == ButtonState.Pressed &&
                           _lastMouseState.RightButton == ButtonState.Released;
            RightReleased = mouseState.RightButton == ButtonState.Released &&
                            _lastMouseState.RightButton == ButtonState.Pressed;
            MiddleClicked = mouseState.MiddleButton == ButtonState.Pressed &&
                            _lastMouseState.MiddleButton == ButtonState.Released;
            MiddleReleased = mouseState.MiddleButton == ButtonState.Released &&
                             _lastMouseState.MiddleButton == ButtonState.Pressed;
            LeftHeld = mouseState.LeftButton == ButtonState.Pressed &&
                       _lastMouseState.LeftButton == ButtonState.Pressed;

            ScrolledUp = mouseState.ScrollWheelValue < _lastMouseState.ScrollWheelValue;
            ScrolledDown = mouseState.ScrollWheelValue > _lastMouseState.ScrollWheelValue;

            _lastMouseState = mouseState;
        }
    }
}