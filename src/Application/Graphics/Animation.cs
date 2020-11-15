using System;
using System.Collections.Generic;

namespace Application.Graphics
{
    internal class Animation
    {
        private readonly IList<AnimationFrame> _frames;
        private readonly bool _repeating;

        private int _currentFrameIndex;
        private float _timer;

        public string Name { get; private set; }
        public AnimationFrame CurrentFrame => _frames[_currentFrameIndex];

        public Animation(string name, IList<AnimationFrame> frames, bool repeating = false)
        {
            Name = name;
            _frames = frames;
            _repeating = repeating;
        }

        public void Update(float delta)
        {
            _timer += delta;

            if (_timer > CurrentFrame.FrameTime)
            {
                NextFrame();
            }
        }

        private void NextFrame()
        {
            _timer = 0;
            _currentFrameIndex++;

            if (_repeating is false)
            {
                _currentFrameIndex = Math.Clamp(_currentFrameIndex, 0, _frames.Count - 1);
                return;
            }

            if (_currentFrameIndex >= _frames.Count)
            {
                _currentFrameIndex = 0;
            }
        }
    }
}