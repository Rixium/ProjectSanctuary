using System;
using System.Collections.Generic;
using Application.Content;
using Application.Scenes;
using Application.Transitions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.View
{
    public interface IViewPortManager
    {
        Viewport ViewPort { get; set; }
    }

    public class ViewPortManager : IViewPortManager
    {
        public Viewport ViewPort { get; set; }
    }
    
    public class ViewManager : IViewManager
    {
        private readonly IContentChest _contentChest;
        private readonly ISceneManager _sceneManager;
        private readonly IViewPortManager _viewPortManager;

        private ITransitionManager _transitionManager;

        public Viewport ViewPort
        {
            get => _viewPortManager.ViewPort;
            set => _viewPortManager.ViewPort = value;
        }
        
        public GraphicsDeviceManager Graphics { get; set; }

        public ViewManager(IContentChest contentChest, ISceneManager sceneManager, IViewPortManager viewPortManager, ITransitionManager transitionManager)
        {
            _contentChest = contentChest;
            _sceneManager = sceneManager;
            _viewPortManager = viewPortManager;
            _transitionManager = transitionManager;
        }

        public void Initialize()
        {
            ViewPort = new Viewport(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);

            _transitionManager.Initialize();
            _sceneManager.Initialize();
            _sceneManager.SetNextScene<GameScene>();
        }

        public void Update(float delta)
        {
            _sceneManager.ActiveScene?.Update(delta);
            _transitionManager.Update(delta);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Graphics.GraphicsDevice.Clear(_sceneManager.BackgroundColor);
            _sceneManager.ActiveScene?.Draw(spriteBatch);
            _transitionManager.Draw(spriteBatch);
        }

        public void RequestExit() => OnExitRequest?.Invoke();

        public Action OnExitRequest { get; set; }

        public IEnumerable<string> GetControls()
        {
            var controls = RequestControls?.Invoke();
            return controls;
        }

        public Func<IList<string>> RequestControls { get; set; }

        public void WindowResized() => _sceneManager?.WindowResized();
    }
}