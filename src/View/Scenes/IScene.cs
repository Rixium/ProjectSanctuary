using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View.Scenes
{
    public interface IScene
    {
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
    }
}