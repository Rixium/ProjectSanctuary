using System;
using System.Collections.Generic;
using Application.Content;
using Application.Content.ContentTypes;
using Application.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Application.Scenes
{
    public class SplashScene : IScene
    {
        public SceneType SceneType => SceneType.Splash;
        
        private readonly IViewPortManager _viewPortManager;
        private readonly IContentChest _contentChest;
        private readonly IContentLoader<Hair> _hairContentLoader;
        public Action<SceneType> RequestNextScene { get; set; }
        public Color BackgroundColor => Color.Black;

        private const float TimeToShow = 3f;
        private float _timeShown;
        private IReadOnlyCollection<Hair> _hairs;
        private int _songNum;

        public SplashScene(
            IViewPortManager viewPortManager,
            IContentChest contentChest,
            IContentLoader<Hair> hairContentLoader)
        {
            _viewPortManager = viewPortManager;
            _contentChest = contentChest;
            _hairContentLoader = hairContentLoader;
        }


        public async void Initialize()
        {
            _songNum = new Random((int) DateTime.Now.Ticks).Next(0, 2) + 1;

            try
            {
                await _contentChest.Preload<Song>($"Music/MenuSong{_songNum}");
            }
            catch (Exception x)
            {
                // ignored
            }
        }

        public void Update(float delta)
        {
            _timeShown += delta;

            if (_timeShown < TimeToShow)
            {
                return;
            }
            
            MediaPlayer.Play(_contentChest.Get<Song>($"Music/MenuSong{_songNum}"));
            RequestNextScene?.Invoke(SceneType.Menu);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            var texture = _contentChest.Get<Texture2D>("splash");

            spriteBatch.Draw(texture,
                new Vector2(_viewPortManager.ViewPort.Width, _viewPortManager.ViewPort.Height) / 2,
                new Rectangle(0, 0, _viewPortManager.ViewPort.Width, _viewPortManager.ViewPort.Height),
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