using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Scenes
{
    internal class GameScene : IScene
    {
        public SceneType SceneType => SceneType.Game;
        public Action<SceneType> RequestNextScene { get; set; }
        public Color BackgroundColor => Color.Green;

        public void Update(float delta)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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

        public void Initialize()
        {
        }
    }
}