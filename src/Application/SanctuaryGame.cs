﻿using System;
using System.Globalization;
using System.Threading;
using Application.Configuration;
using Application.Content;
using Application.FileSystem;
using Application.Input;
using Application.UI;
using Application.Utils;
using Application.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Steamworks;

namespace Application
{
    public class SanctuaryGame : Game
    {
        private const string GameName = "Project Sanctuary";
        private const int Major = 0;
        private const int Minor = 1;
        private const int Revision = 0;

        public static IKeyboardDispatcher KeyboardDispatcher;
        public static IMouseManager MouseManager;
        public static IOptionsManager OptionsManager;
        public static Cursor Cursor;

        private readonly GraphicsDeviceManager _graphics;
        private IApplicationFolder _applicationFolder;
        private IViewManager _viewManager;
        private IContentChest _contentChest;

        private SpriteBatch _spriteBatch;

        public SanctuaryGame(IContentChest contentChest, IViewManager viewManager)
        {
            _contentChest = contentChest;
            _viewManager = viewManager;

            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowOnClientSizeChanged;

            IsMouseVisible = false;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            _viewManager.Graphics = _graphics;
            //InitializeSteam();
        }

        private void InitializeSteam()
        {
            try
            {
                SteamAPI.Init();
            }
            catch (Exception value)
            {
                Console.WriteLine(value);
                Exit();
            }
        }

        public static bool Debug { get; set; }

        private void WindowOnClientSizeChanged(object sender, EventArgs e)
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

            ViewManager.Instance?.WindowResized();

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

            KeyboardDispatcher = new KeyboardDispatcher();
            MouseManager = new MonoGameMouseManager();

            Content.RootDirectory = "assets";
            _contentChest.Content = new MonoGameContentManager
            {
                ContentManager = Content
            };

            ShapeHelpers.ContentChest = _contentChest;

            Cursor = new Cursor(_contentChest);

            OptionsManager = new OptionsManager(_applicationFolder);
            OptionsManager.Initialize();

            _viewManager.Initialize();
            ViewManager.ViewPort = _graphics.GraphicsDevice.Viewport;
            _viewManager.OnExitRequest += OnExit;
            _viewManager.RequestControls += () => Enum.GetNames(typeof(InputAction));

            KeyboardDispatcher.SubscribeToKeyPress(Keys.OemTilde, () => { Debug = !Debug; });

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

            var pronounOptions = new PronounOptions();
            pronounOptions.Save(_applicationFolder, false);
        }

        private void UpdateWindowTitle() => Window.Title = $"{GameName} - {GameVersion()}";

        protected override void Update(GameTime gameTime)
        {
            var delta = (float) gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;

            KeyboardDispatcher.Update(delta);
            MouseManager.Update(delta);

            _viewManager.Update(delta);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _viewManager.Draw(_spriteBatch);
            Cursor.Draw(_spriteBatch);
            base.Draw(gameTime);
        }

        public static string GameVersion() => $"{Major}.{Minor}.{Revision}";
    }
}