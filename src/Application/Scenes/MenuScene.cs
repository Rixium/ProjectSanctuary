using Application.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Scenes
{
    public class MenuScene : IScene
    {
        private IMenu _activeMenu;
        private readonly TitleMenu _mainTitleMenu;
        private readonly MainOptionsMenu _mainOptionsMenu;
        private readonly CharacterCreationMenu _characterCreationMenu;

        public Color BackgroundColor => Color.White;

        public MenuScene()
        {
            _mainTitleMenu = new TitleMenu();
            _characterCreationMenu = new CharacterCreationMenu();
            _mainOptionsMenu = new MainOptionsMenu();

            _activeMenu = _mainTitleMenu;

            _mainTitleMenu.NewGameButton.OnClick += () => _activeMenu = _characterCreationMenu;
            _mainTitleMenu.LoadGameButton.OnClick += () => _activeMenu = _mainOptionsMenu;
            _mainOptionsMenu.BackButton.OnClick += () => _activeMenu = _mainTitleMenu;
        }

        public void Update(float delta) => _activeMenu.Update(delta);

        public void Draw(SpriteBatch spriteBatch) => _activeMenu.Draw(spriteBatch);

        public void WindowResized()
        {
            _mainTitleMenu.WindowResized();
            _mainOptionsMenu.WindowResized();
            _characterCreationMenu.WindowResized();
        }
    }
}