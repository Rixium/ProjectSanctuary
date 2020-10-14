using System;
using System.Linq;
using Application.Configuration;
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

            _options = ControlOptions.LoadFrom("controls.xml");

            _viewManager = new ViewManager(_graphics);
            _viewManager.Initialize();

            _contentChest = new ContentChest(new MonoGameContentManager(Content, "assets"));

            base.Initialize();
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