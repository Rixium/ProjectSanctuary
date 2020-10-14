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
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            var texture = ContentChest.Instance.Get<Texture2D>("splash");
            var zoom = ViewManager.ViewPort.Height < 800 ? 2 : 3;
            
            spriteBatch.Draw(texture,
                new Vector2(ViewManager.ViewPort.Width, ViewManager.ViewPort.Height) / 2,
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                0f,
                new Vector2(texture.Width / 2.0f, texture.Height / 2.0f),
                zoom,
                SpriteEffects.None,
                0.2f);

            spriteBatch.End();
        }
    }
}