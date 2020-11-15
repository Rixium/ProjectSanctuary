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
             _hairs = _hairContentLoader.GetContent("assets/characters/player_hair.json");
            var num = new Random((int) DateTime.Now.Ticks).Next(0, 2) + 1;

            try
            {
                await _contentChest.Preload<Song>($"Music/MenuSong{num}");
                MediaPlayer.Play(_contentChest.Get<Song>($"Music/MenuSong{num}"));
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

            // RequestNextScene?.Invoke(SceneType.Menu);
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

            var x = 0;
            
            foreach (var hair in _hairs)
            {
                spriteBatch.Draw(_contentChest.Get<Texture2D>("characters/player_hair"), new Vector2(200, 200) + new Vector2(x, 0), hair.Source, Color.Red,0f, Vector2.Zero, 3f, SpriteEffects.None, 1f);
                x += 32 * 3;
            }

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