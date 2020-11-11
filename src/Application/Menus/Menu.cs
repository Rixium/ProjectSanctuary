using Microsoft.Xna.Framework.Graphics;

namespace Application.Menus
{
    public abstract class Menu : IMenu
    {

        public abstract void Initialize();
        public virtual void Update(float delta)
        {
        
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public virtual void WindowResized()
        {
            
        }

        public virtual void Finish()
        {
            
        }
    }
}