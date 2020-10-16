using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectSanctuary.View
{
    public interface IViewManager
    {
        Action OnExitRequest { get; set; }
        Func<IList<string>> RequestControls { get; set; }
        void Initialize();
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
    }
}