using Microsoft.Xna.Framework.Graphics;

namespace Application.Transitions
{
    public interface ITransitionManager
    {
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
    }
}