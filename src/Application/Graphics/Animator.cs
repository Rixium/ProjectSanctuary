using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Graphics
{
    internal class Animator
    {
        private readonly IReadOnlyCollection<Animation> _animations;
        private Animation _currentAnimation;

        // ReSharper disable once ConvertToAutoProperty
        // ReSharper disable once MemberCanBePrivate.Global
        public Animation CurrentAnimation
        {
            get => _currentAnimation;
            set => _currentAnimation = value;
        }

        public Animator(IReadOnlyCollection<Animation> animations)
        {
            _animations = animations;
        }

        public void SetAnimation(string animationName) =>
            CurrentAnimation =
                _animations.FirstOrDefault(x => x.Name.Equals(animationName, StringComparison.OrdinalIgnoreCase));

        public void Update(in float delta) =>
            CurrentAnimation?.Update(delta);
    }
}