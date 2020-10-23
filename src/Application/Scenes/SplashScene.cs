using Application.Content;
using Application.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Scenes
{
    public class SplashScene : IScene
    {
        public Color BackgroundColor => Color.Black;
        
        private const float TimeToShow = 3f;
        private float _timeShown;

        public void Update(float delta)
        {
            if (SceneManager.Instance.NextScene != null)
            {
                return;
            }

            _timeShown += delta;

            if (_timeShown < TimeToShow)
            {
                return;
            }
            
            // var num = new Random((int) DateTime.Now.Ticks).Next(0, 2) + 1;
            // MediaPlayer.Play(ContentChest.Instance.Get<Song>($"Music/MenuSong{num}"));
            
            SceneManager.Instance.AddScene(new MenuScene());
            SceneManager.Instance.SetNextScene<MenuScene>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            var texture = ContentChest.Instance.Get<Texture2D>("splash");
            
            spriteBatch.Draw(texture,
                new Vector2(ViewManager.ViewPort.Width, ViewManager.ViewPort.Height) / 2,
                new Rectangle(0, 0, ViewManager.ViewPort.Width, ViewManager.ViewPort.Height),
                Color.White,
                0f,
                new Vector2(texture.Width / 2.0f, texture.Height / 2.0f),
                1,
                SpriteEffects.None,
                0.2f);

            spriteBatch.End();
        }

        public void WindowResized()
        {
            
        }
    }
}