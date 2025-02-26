﻿using System;
using Application.Content;
using Application.Menus;
using Application.UI;
using Application.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Scenes
{
    public class MenuScene : IScene
    {
        public SceneType SceneType => SceneType.Menu;

        private IMenu _activeMenu;
        private readonly TitleMenu _mainTitleMenu;
        private readonly MainOptionsMenu _mainOptionsMenu;
        private readonly IContentChest _contentChest;
        private readonly IViewPortManager _viewPortManager;
        private readonly CharacterCreationMenu _characterCreationMenu;

        public Action<SceneType> RequestNextScene { get; set; }
        public Color BackgroundColor => Color.White;

        public MenuScene(IContentChest contentChest, IViewPortManager viewPortManager,
            CharacterCreationMenu characterCreationMenu,
            TitleMenu mainTitleMenu, MainOptionsMenu mainOptionsMenu)
        {
            _contentChest = contentChest;
            _viewPortManager = viewPortManager;
            _characterCreationMenu = characterCreationMenu;
            _mainTitleMenu = mainTitleMenu;
            _mainOptionsMenu = mainOptionsMenu;

            _activeMenu = _mainTitleMenu;
        }

        public void Initialize()
        {
            _mainOptionsMenu.Initialize();
            _characterCreationMenu.Initialize();
            _mainTitleMenu.Initialize();

            SetupButtons();
        }

        private void SetupButtons()
        {
            _mainTitleMenu.NewGameButton.OnClick += () => _activeMenu = _characterCreationMenu;
            _mainTitleMenu.LoadGameButton.OnClick += () => _activeMenu = _mainOptionsMenu;
            ((IClickable) _mainOptionsMenu.BackButton).OnClick += () => _activeMenu = _mainTitleMenu;
            _characterCreationMenu.BackButton.OnClick += () => _activeMenu = _mainTitleMenu;
            _characterCreationMenu.DoneButton.OnClick += StartGame;
        }

        private void StartGame() => 
            RequestNextScene?.Invoke(SceneType.Game);

        public void Update(float delta) => _activeMenu.Update(delta);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_contentChest.Get<Texture2D>("background"),
                new Rectangle(0, 0, _viewPortManager.ViewPort.Width, _viewPortManager.ViewPort.Height),
                Color.White * 0.2f);
            spriteBatch.End();

            _activeMenu.Draw(spriteBatch);
        }

        public void WindowResized()
        {
            _mainTitleMenu.WindowResized();
            _mainOptionsMenu.WindowResized();
            _characterCreationMenu.WindowResized();

            SetupButtons();
        }

        public void Finish()
        {
            _mainTitleMenu.Finish();
            _mainOptionsMenu.Finish();
            _characterCreationMenu.Finish();
        }

        public void Start()
        {
        }
    }
}