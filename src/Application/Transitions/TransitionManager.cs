using Application.Content;
using Application.Scenes;
using Application.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Transitions
{
    public class TransitionManager : ITransitionManager
    {
        private ISceneManager _sceneManager;
        private readonly IContentChest _contentChest;
        private readonly IViewManager _viewManager;

        private bool _fadingToBlack;
        private bool _fadingToTransparent;

        private const float FadeSpeed = 1f;
        private float _currentFade;
        private Texture2D _pixel;

        public TransitionManager(ISceneManager sceneManager, IContentChest contentChest, IViewManager viewManager)
        {
            _sceneManager = sceneManager;
            _contentChest = contentChest;
            _viewManager = viewManager;
        }

        public void Initialize()
        {
            _pixel = _contentChest.Get<Texture2D>("Utils/pixel");
        }

        public void Update(float delta)
        {
            if (_sceneManager.NextScene != null)
            {
                BeginTransition();
            }

            if (_fadingToBlack)
            {
                _currentFade += delta;

                if (_currentFade >= 1.0f)
                {
                    _currentFade = 1.0f;
                    _sceneManager.SwitchToNextScene();

                    _fadingToTransparent = true;
                    _fadingToBlack = false;
                }
            }
            else if (_fadingToTransparent)
            {
                _currentFade -= delta;

                if (_currentFade <= 0.0f)
                {
                    _currentFade = 0.0f;
                    _fadingToBlack = false;
                    _fadingToTransparent = false;
                }
            }
        }

        private void BeginTransition()
        {
            if (_fadingToBlack || _fadingToTransparent)
            {
                return;
            }

            _fadingToBlack = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentFade < 0.001f)
            {
                return;
            }

            spriteBatch.Begin();

            spriteBatch.Draw(_pixel, _viewManager.ViewPort.Bounds, Color.Black * _currentFade);

            spriteBatch.End();
        }
    }
}