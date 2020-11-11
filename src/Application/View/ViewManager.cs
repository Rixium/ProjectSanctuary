using System;
using System.Collections.Generic;
using Application.Content;
using Application.Scenes;
using Application.Transitions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.View
{
    public class ViewManager : IViewManager
    {
        private readonly IContentChest _contentChest;
        private readonly SplashScene _splashScene;
        public static Viewport ViewPort;
        
        
        private ITransitionManager _transitionManager;
        private ISceneManager _sceneManager;
        
        public GraphicsDeviceManager Graphics { get; set; }
        public static ViewManager Instance { get; private set; } 
        
        public ViewManager(IContentChest contentChest, SplashScene splashScene)
        {
            _contentChest = contentChest;
            _splashScene = splashScene;
            Instance = this;
        }

        public void Initialize()
        {
            ViewPort = new Viewport(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            
            _sceneManager = new SceneManager();
            _transitionManager = new TransitionManager(_sceneManager, _contentChest);
            _transitionManager.Initialize();
            
            _sceneManager.AddScene(_splashScene);
            _sceneManager.SetNextScene<SplashScene>();
            
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