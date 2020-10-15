using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View.Menus
{
    public interface IMenu
    {
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
    }
}