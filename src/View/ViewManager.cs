using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View.Content;
using ProjectSanctuary.View.Scenes;
using ProjectSanctuary.View.Transitions;

namespace ProjectSanctuary.View
{
    public class ViewManager : IViewManager
    {
        public static Viewport ViewPort;
        
        private readonly GraphicsDeviceManager _graphics;
        
        private ITransitionManager _transitionManager;
        private ISceneManager _sceneManager;
        
        public static ViewManager Instance { get; private set; } 
        
        public ViewManager(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            Instance = this;
        }

        public void Initialize()
        {
            ViewPort = new Viewport(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            
            _sceneManager = new SceneManager();
            _transitionManager = new TransitionManager(_sceneManager, ContentChest.Instance);
            
            _sceneManager.AddScene(new MenuScene());
            _sceneManager.SetNextScene<MenuScene>();
        }

        public void Update(float delta)
        {
            _sceneManager.ActiveScene?.Update(delta);
            _transitionManager.Update(delta);
        }
            
        public void Draw(SpriteBatch spriteBatch)
        {
            _graphics.GraphicsDevice.Clear(_sceneManager.BackgroundColor);
            _sceneManager.ActiveScene?.Draw(spriteBatch);
            _transitionManager.Draw(spriteBatch);
        }

        public void RequestExit() => OnExitRequest?.Invoke();

        public Action OnExitRequest { get; set; }
    }
}