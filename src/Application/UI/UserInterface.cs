using Application.UI.Widgets;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI
{
    public class UserInterface : IUserInterface
    {
        public IWidget Root { get; private set; }

        public UserInterface()
        {
        }

        public void Update(float delta)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Root?.Draw(spriteBatch);
        }

        public T AddWidget<T>(T widget) where T : IWidget
        {
            Root = widget;
            return widget;
        }
    }
}