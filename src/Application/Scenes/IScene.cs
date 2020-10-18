using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Scenes
{
    public interface IScene
    {
        Color BackgroundColor { get; }
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
        void WindowResized();
    }
}