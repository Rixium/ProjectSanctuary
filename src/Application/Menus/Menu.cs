using System.Collections.Generic;
using Application.UI;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Menus
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