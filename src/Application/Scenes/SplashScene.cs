using System;
using System.IO;
using System.Linq;
using Application.Content;
using Application.Content.Aseprite;
using Application.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;

namespace Application.Scenes
{
    public class SplashScene : IScene
    {
        public SceneType SceneType => SceneType.Splash;
        
        private readonly IViewPortManager _viewPortManager;
        private readonly IContentChest _contentChest;
        public Action<SceneType> RequestNextScene { get; set; }
        public Color BackgroundColor => Color.Black;

        private const float TimeToShow = 3f;
        private float _timeShown;
        private Song _song;
        private Vector2 _playerHeadPosition;
        private Rectangle _hairSource;
        private Vector2 _hairPosition;

        public SplashScene(
            IViewPortManager viewPortManager,
            IContentChest contentChest)
        {
            _viewPortManager = viewPortManager;
            _contentChest = contentChest;
        }


        public void Initialize()
        {
            _playerHeadPosition = new Vector2(200, 200);
            var data = File.ReadAllText("assets/characters/player_hair.json");
            var aseprite = JsonConvert.DeserializeObject<AsepriteData>(data);

            _hairSource = new Rectangle(0, 0, 32, 32);
            _hairPosition = _playerHeadPosition - new Vector2(16 - 16 / 2f, 16 - 16 / 2f);
        }

        public void Update(float delta)
        {
            _timeShown += delta;

            if (_timeShown < TimeToShow)
            {
                return;
            }

            if (_song == null)
            {
                return;
            }

            MediaPlayer.Play(_song);

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

            spriteBatch.Draw(_contentChest.Get<Texture2D>("characters/player_heads"), _playerHeadPosition,
                new Rectangle(0, 0, 16, 32), Color.Yellow);
            spriteBatch.Draw(_contentChest.Get<Texture2D>("characters/player_hair"), _hairPosition, _hairSource,
                Color.Red);

            spriteBatch.End();
        }

        public void WindowResized()
        {
        }

        public void Finish()
        {
        }

        public async void Start()
        {
            var num = new Random((int) DateTime.Now.Ticks).Next(0, 2) + 1;
            await _contentChest.Preload<Song>($"Music/MenuSong{num}");
            _song = _contentChest.Get<Song>($"Music/MenuSong{num}");
        }
    }
}