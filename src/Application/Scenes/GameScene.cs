using System;
using Application.Content;
using Application.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Scenes
{
    internal class GameScene : IScene
    {
        private readonly IContentChest _contentChest;
        private Animator _testAnimator;

        public SceneType SceneType => SceneType.Game;
        public Action<SceneType> RequestNextScene { get; set; }
        public Color BackgroundColor => Color.Green;
        
        public GameScene(IContentChest contentChest)
        {
            _contentChest = contentChest;
        }

        public void Update(float delta)
        {
            _testAnimator.Update(delta);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            var sprite = _contentChest.Get<Texture2D>("characters/player_body");
            var anim = _testAnimator.CurrentAnimation;
            var frame = anim.CurrentFrame;
            
            spriteBatch.Draw(sprite, new Vector2(100, 100), frame.Source, Color.White);
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

        public void Initialize()
        {
            _testAnimator = new Animator(new[]
            {
                new Animation("Walk", new[]
                {
                    new AnimationFrame(0, 0, 16, 32, 0.3f),
                    new AnimationFrame(16, 0, 16, 32, 0.3f),
                    new AnimationFrame(32, 0, 16, 32, 0.3f),
                }, true)
            });

            _testAnimator.SetAnimation("Walk");
        }
    }
}