using Microsoft.Xna.Framework.Graphics;

namespace Application.Menus
{
    public interface IMenu
    {
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
        void WindowResized();
        void Finish();
    }
}