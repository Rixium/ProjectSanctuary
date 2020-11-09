using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    public interface IWidget
    {
        int Id { get; set; }
        bool Visible { get; set; }
        T AddChild<T>(T widget) where T : IWidget;
        void Draw(SpriteBatch spriteBatch);
        void DrawDebug(SpriteBatch spriteBatch);
        bool MouseMove(Rectangle mouseBounds);
        bool MouseClick(Rectangle mouseRectangle);
        bool MouseHeld(Rectangle mouseRectangle);
        bool MouseDragged(Rectangle mouseRectangle, float dragX, float dragY);
        bool MouseScrolled(Rectangle mouseBounds, MouseScrollDirection direction);
        bool MouseReleased(Rectangle mouseRectangle);
    }
}