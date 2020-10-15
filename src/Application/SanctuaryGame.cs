using System;
using System.Globalization;
using System.Threading;
using Application.Configuration;
using Application.FileSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View;
using ProjectSanctuary.View.Content;

namespace Application
{
    public class SanctuaryGame : Game
    {
        private const string GameName = "Project Sanctuary";
        private const int Major = 0;
        private const int Minor = 1;
        private const int Revision = 0;

        private IApplicationFolder _applicationFolder;
        private IOptionsManager _optionsManager;
        private IViewManager _viewManager;
        private IContentChest _contentChest;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ControlOptions _options;

        public SanctuaryGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowOnClientSizeChanged;

            IsMouseVisible = true;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        private void WindowOnClientSizeChanged(object? sender, EventArgs e)
        {
            Window.ClientSizeChanged -= WindowOnClientSizeChanged;

            var w = Window.ClientBounds.Width;
            var h = Window.ClientBounds.Height;

            if (w < 1280)
            {
                _graphics.PreferredBackBufferWidth = 1280;
                w = 1280;
            }

            if (h < 720)
            {
                _graphics.PreferredBackBufferHeight = 720;
                h = 720;
            }

            _graphics.GraphicsDevice.Viewport = new Viewport(new Rectangle(0, 0, w,
                h));

            ViewManager.ViewPort = _graphics.GraphicsDevice.Viewport;
            _graphics.ApplyChanges();


            Window.ClientSizeChanged += WindowOnClientSizeChanged;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            _spriteBatch.Dispose();
            _contentChest.Unload();
            base.UnloadContent();
        }

        protected override void Initialize()
        {
            IsFixedTimeStep = true;

            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.ApplyChanges();

            UpdateWindowTitle();

            InitializeApplicationFolder();

            _contentChest = new ContentChest(new MonoGameContentManager(Content, "assets"));

            // Now we've created the app data folder,
            // and saved the defaults if required,
            // The options can be loaded using it.
            _optionsManager = new OptionsManager(_applicationFolder);
            _optionsManager.Initialize();

            _viewManager = new ViewManager(_graphics);
            _viewManager.Initialize();
            ViewManager.ViewPort = _graphics.GraphicsDevice.Viewport;
            _viewManager.OnExitRequest += OnExit;

            base.Initialize();
        }

        private void OnExit()
        {
            Console.WriteLine("Game is closing down.");
            Exit();
        }

        private void InitializeApplicationFolder()
        {
            // Create the AppData folder.
            // Also create the default file for settings files if they don't exist.
            _applicationFolder = new ApplicationFolder(GameName);
            _applicationFolder.Create();

            var controlOptions = new ControlOptions();
            controlOptions.Save(_applicationFolder, false);
        }

        private void UpdateWindowTitle() => Window.Title = $"{GameName} - {GameVersion()}";

        protected override void Update(GameTime gameTime)
        {
            var delta = (float) gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
            _viewManager.Update(delta);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _viewManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        public static string GameVersion() => $"{Major}.{Minor}.{Revision}";
    }
}