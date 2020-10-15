using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View.UI;

namespace ProjectSanctuary.View.Menus
{
    public abstract class Menu : IMenu
    {
        
        protected readonly IList<IClickable> Clickables = new List<IClickable>();
        
        public virtual void Update(float delta)
        {
            foreach (var clickable in Clickables)
            {
                clickable.Update(delta);
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        
    }
}