using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        
        public ViewManager(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
        }

        public void Initialize()
        {
            ViewPort = new Viewport(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            
            _sceneManager = new SceneManager();
            _transitionManager = new TransitionManager(_sceneManager);
            
            _sceneManager.AddScene(new SplashScene());
            _sceneManager.SetNextScene<SplashScene>();
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
    }
}