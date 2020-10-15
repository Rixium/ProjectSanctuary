using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View.UI
{
    public interface IClickable
    {
        Action OnClick { get; }
        int Width { get; }
        int Height { get; }
        bool Hovering { get; set; }
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
        bool Intersects(Rectangle rectangle);
        void DrawDebug(SpriteBatch spriteBatch);
        void Click();
    }
}