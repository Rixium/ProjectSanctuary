using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View.Menus
{
    public abstract class Menu : IMenu
    {
        public virtual void Update(float delta)
        {
            
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        
    }
}