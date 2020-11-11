using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Scenes
{
    public interface IScene
    {
        Action<IScene> RequestNextScene { get; set; }
        Color BackgroundColor { get; }
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
        void WindowResized();
        void Finish();
        void Start();
        void Initialize();
    }
}