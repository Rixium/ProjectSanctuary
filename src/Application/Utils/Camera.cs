using Application.View;
using Microsoft.Xna.Framework;

namespace Application.Utils
{
    public class Camera
    {
        public Camera(Vector2 cameraPosition)
        {
            Position = cameraPosition;
            Zoom = 1.0f;
        }

        public Vector2 Position { get; set; }
        private float Zoom { get; set; }
        private float Rotation { get; set; }
        private static int ViewportWidth => ViewManager.ViewPort.Width;
        private static int ViewportHeight => ViewManager.ViewPort.Height;
        private static Vector2 ViewportCenter => new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);
        
        public Matrix TranslationMatrix =>
            Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
            Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));

        public void AdjustZoom(float amount)
        {
            Zoom += amount;
            if (Zoom < 0.1f)
            {
                Zoom = 0.1f;
            }
        }

        public Rectangle ViewportWorldBoundary()
        {
            var viewPortCorner = ScreenToWorld(new Vector2(0, 0));
            var viewPortBottomCorner = ScreenToWorld(new Vector2(ViewportWidth, ViewportHeight));

            return new Rectangle((int) viewPortCorner.X, (int) viewPortCorner.Y,
                (int) (viewPortBottomCorner.X - viewPortCorner.X),
                (int) (viewPortBottomCorner.Y - viewPortCorner.Y));
        }

        public void CenterOn(Vector2 position)
        {
            Position = position;
        }

        public void CenterOn(Point location)
        {
            Position = CenteredPosition(location);
        }

        private static Vector2 CenteredPosition(Point location, bool clampToMap = false)
        {
            var (x, y) = new Vector2(location.X, location.Y);
            var cameraCenteredOnTilePosition = new Vector2(x,
                y);
            if (clampToMap)
            {
                var cameraMax = new Vector2(ViewportWidth,
                    ViewportHeight);

                return Vector2.Clamp(cameraCenteredOnTilePosition, Vector2.Zero, cameraMax);
            }

            return cameraCenteredOnTilePosition;
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, TranslationMatrix);
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(TranslationMatrix));
        }
    }
}