using Microsoft.Xna.Framework;

namespace Application.Input
{
    public interface IMouseManager
    {
        Rectangle MouseBounds { get; }
        bool LeftClicked { get; }
        bool LeftReleased { get; }
        bool RightClicked { get; }
        bool RightReleased { get; }
        bool MiddleClicked { get; }
        bool MiddleReleased { get; }
        bool LeftHeld { get; }
        bool ScrolledDown { get; }
        bool ScrolledUp { get; }
        void Update(float delta);
    }
}