using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View.Scenes;

namespace ProjectSanctuary.View
{
    public class ViewManager : IViewManager
    {
        public static Viewport ViewPort;
        
        private readonly GraphicsDeviceManager _graphics;
        private SceneManager _sceneManager;
        
        public ViewManager(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
        }

        public void Initialize()
        {
            ViewPort = new Viewport(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            
            _sceneManager = new SceneManager();

            _sceneManager.AddScene(new SplashScene());
            _sceneManager.SetNextScene<SplashScene>();
            _sceneManager.SwitchToNextScene();

        }

        public void Update()
        {
            _sceneManager.ActiveScene?.Update();   
        }
            
        public void Draw(SpriteBatch spriteBatch)
        {
            _sceneManager.ActiveScene?.Draw(spriteBatch);
        }
    }
}