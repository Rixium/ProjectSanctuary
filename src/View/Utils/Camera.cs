using Microsoft.Xna.Framework;

namespace ProjectSanctuary.View.Utils
{
    public class Camera
    {
        public float Zoom = 2f;
        private Vector2 _position;

        public Camera() => _position = Vector2.Zero;

        public Vector2 Position
        {
            set => _position = value;
        }

        public Matrix GetMatrix() =>
            Matrix.CreateTranslation(new Vector3(-_position.X, -_position.Y, 0)) *
            Matrix.CreateScale(Zoom, Zoom, 1) *
            Matrix.CreateTranslation(new Vector3(ViewManager.ViewPort.Center().X, ViewManager.ViewPort.Center().Y, 0));
    }
}