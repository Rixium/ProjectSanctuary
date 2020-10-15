﻿using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View
{
    public interface IViewManager
    {
        void Initialize();
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
    }
}