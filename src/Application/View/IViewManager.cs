﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.View
{
    public interface IViewManager
    {
        Viewport ViewPort { get; set; }
        GraphicsDeviceManager Graphics { get; set; }
        Action OnExitRequest { get; set; }
        Func<IList<string>> RequestControls { get; set; }
        void Initialize();
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
        void RequestExit();
        IEnumerable<string> GetControls();
        void WindowResized();
    }
}