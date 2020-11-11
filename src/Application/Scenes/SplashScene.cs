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
        private readonly IViewPortManager _viewPortManager;
        private readonly IContentChest _contentChest;
        private readonly MenuScene _menuScene;
        public Action<IScene> RequestNextScene { get; set; }
        public Color BackgroundColor => Color.Black;

        private const float TimeToShow = 3f;
        private float _timeShown;

        public SplashScene(
            IViewPortManager viewPortManager,
            IContentChest contentChest,
            MenuScene menuScene)
        {
            _viewPortManager = viewPortManager;
            _contentChest = contentChest;
            _menuScene = menuScene;
        }


        public void Initialize()
        {
        }

        public void Update(float delta)
        {
            _timeShown += delta;

            if (_timeShown < TimeToShow)
            {
                return;
            }

            var num = new Random((int) DateTime.Now.Ticks).Next(0, 2) + 1;
            MediaPlayer.Play(_contentChest.Get<Song>($"Music/MenuSong{num}"));

            RequestNextScene?.Invoke(_menuScene);
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