using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View.Content;

namespace ProjectSanctuary.View.Scenes
{
    public class SplashScene : IScene
    {
        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(
                ContentChest.Instance.Get<Texture2D>("splash"),
                new Rectangle(0, 0, ViewManager.ViewPort.Width, ViewManager.ViewPort.Height),
                Color.White);

            spriteBatch.End();
        }
    }
}