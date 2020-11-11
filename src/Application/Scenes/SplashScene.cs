using System;
using Application.Content;
using Application.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Application.Scenes
{
    public class SplashScene : IScene
    {
        private readonly IContentChest _contentChest;
        private readonly ISceneManager _sceneManager;
        private readonly MenuScene _menuScene;
        public Color BackgroundColor => Color.Black;

        private const float TimeToShow = 3f;
        private float _timeShown;

        public SplashScene(IContentChest contentChest, ISceneManager sceneManager, MenuScene menuScene)
        {
            _contentChest = contentChest;
            _sceneManager = sceneManager;
            _menuScene = menuScene;
        }


        public void Initialize()
        {
            
        }
        
        public void Update(float delta)
        {
            if (_sceneManager.NextScene != null)
            {
                return;
            }

            _timeShown += delta;

            if (_timeShown < TimeToShow)
            {
                return;
            }

            var num = new Random((int) DateTime.Now.Ticks).Next(0, 2) + 1;
            MediaPlayer.Play(_contentChest.Get<Song>($"Music/MenuSong{num}"));

            _sceneManager.AddScene(_menuScene);
            _sceneManager.SetNextScene<MenuScene>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            var texture = _contentChest.Get<Texture2D>("splash");

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

        public void Finish()
        {
        }

        public void Start()
        {
        }
    }
}