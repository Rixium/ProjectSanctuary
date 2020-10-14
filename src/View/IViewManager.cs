using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View
{
    public interface IViewManager
    {
        void Initialize();
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}