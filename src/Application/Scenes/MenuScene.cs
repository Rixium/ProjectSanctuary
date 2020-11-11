﻿using Application.Content;
using Application.Menus;
using Application.UI;
using Application.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Scenes
{
    public class MenuScene : IScene
    {
        private IMenu _activeMenu;
        private readonly TitleMenu _mainTitleMenu;
        private readonly MainOptionsMenu _mainOptionsMenu;
        private readonly IContentChest _contentChest;
        private readonly CharacterCreationMenu _characterCreationMenu;

        public Color BackgroundColor => Color.White;

        public MenuScene(IContentChest contentChest, CharacterCreationMenu characterCreationMenu)
        {
            _mainTitleMenu = new TitleMenu(contentChest);
            _contentChest = contentChest;
            _characterCreationMenu = characterCreationMenu;
            _mainOptionsMenu = new MainOptionsMenu(contentChest);

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
            ((IClickable)_mainOptionsMenu.BackButton).OnClick += () => _activeMenu = _mainTitleMenu;
            _characterCreationMenu.BackButton.OnClick += () => _activeMenu = _mainTitleMenu;
        }

        public void Update(float delta) => _activeMenu.Update(delta);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_contentChest.Get<Texture2D>("background"),
                new Rectangle(0, 0, ViewManager.ViewPort.Width, ViewManager.ViewPort.Height),
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