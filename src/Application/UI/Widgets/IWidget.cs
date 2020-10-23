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
    }
}