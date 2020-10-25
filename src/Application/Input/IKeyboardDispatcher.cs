using System;
using Microsoft.Xna.Framework.Input;

namespace Application.Input
{
    public interface IKeyboardDispatcher
    {
        void SubscribeToKeyPress(Keys key, Action callback);
        void Update(float delta);
        void SubscribeToAnyKeyPress(Action<Keys> callback);
    }
}