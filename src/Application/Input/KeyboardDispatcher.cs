using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Application.Input
{
    public class KeyboardDispatcher
    {
        private readonly Dictionary<Keys, Action> _onKeyPressSubscribers = new Dictionary<Keys, Action>();
        private Action<Keys> _anyKeyPressSubscribers;

        private KeyboardState _lastKeyState;

        public void SubscribeToKeyPress(Keys key, Action callback)
        {
            if (_onKeyPressSubscribers.ContainsKey(key))
            {
                _onKeyPressSubscribers[key] += callback;
            }
            else
            {
                _onKeyPressSubscribers.Add(key, callback);
            }
        }

        public void Update(float delta)
        {
            var keyState = Keyboard.GetState();

            var pressedKeys = keyState.GetPressedKeys().Except(_lastKeyState.GetPressedKeys()).ToArray();
            
            foreach (var key in pressedKeys)
            {
                if (_onKeyPressSubscribers.Keys.Contains(key))
                {
                    _onKeyPressSubscribers[key]?.Invoke();
                }

                _anyKeyPressSubscribers?.Invoke(key);
            }

            _lastKeyState = keyState;
        }

        public void SubscribeToAnyKeyPress(Action<Keys> callback) => _anyKeyPressSubscribers += callback;
    }
}