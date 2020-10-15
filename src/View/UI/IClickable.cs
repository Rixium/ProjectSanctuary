using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View.UI
{
    public interface IClickable
    {
        int Width { get; }
        int Height { get; }
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
    }
}