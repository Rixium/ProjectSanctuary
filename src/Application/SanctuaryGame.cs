using System;
using System.Linq;
using System.Xml.Serialization;
using Application.Configuration;
using Application.FileSystem;
using Application.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectSanctuary.View;
using ProjectSanctuary.View.Content;
using ProjectSanctuary.View.Scenes;

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
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Initialize()
        {
            UpdateWindowTitle();

            InitializeApplicationFolder();

            // Now we've created the app data folder,
            // and saved the defaults if required,
            // The options can be loaded using it.
            _optionsManager = new OptionsManager(_applicationFolder);
            _optionsManager.Initialize();

            _viewManager = new ViewManager(_graphics);
            _viewManager.Initialize();

            _contentChest = new ContentChest(new MonoGameContentManager(Content, "assets"));

            base.Initialize();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _viewManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _viewManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        public static string GameVersion() => $"{Major}.{Minor}.{Revision}";
    }
}