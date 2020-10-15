using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View.Transitions
{
    public interface ITransitionManager
    {
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
    }
}