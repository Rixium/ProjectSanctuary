using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View.Scenes
{
    public interface IScene
    {
        Color BackgroundColor { get; }
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
    }
}