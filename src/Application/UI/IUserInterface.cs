using Application.UI.Widgets;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI
{
    public interface IUserInterface
    {
        IWidget Root { get; }
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
        T AddWidget<T>(T widget) where T : IWidget;
    }
}