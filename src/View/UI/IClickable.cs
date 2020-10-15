using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View.UI
{
    public interface IClickable
    {

        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
        
    }
}