using Microsoft.Xna.Framework;

namespace Application.Graphics
{
    internal class AnimationFrame
    {
        private readonly int _x;
        private readonly int _y;
        private readonly int _width;
        private readonly int _height;
        public float FrameTime { get; }
        public Rectangle Source => new Rectangle(_x, _y, _width, _height);

        public AnimationFrame(int x, int y, int width, int height, float frameTime)
        {
            FrameTime = frameTime;

            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }
    }
}