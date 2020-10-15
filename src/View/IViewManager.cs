using System;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View
{
    public interface IViewManager
    {
        Action OnExitRequest { get; set; }
        void Initialize();
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
    }
}