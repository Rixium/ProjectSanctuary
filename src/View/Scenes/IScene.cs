using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View.Scenes
{
    public interface IScene
    {
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}