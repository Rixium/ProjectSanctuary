using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View.Menus;

namespace ProjectSanctuary.View.Scenes
{
    public class MenuScene : IScene
    {
        private TitleMenu _mainTitleMenu;
        private MainOptionsMenu _mainOptionsMenu;
        private IMenu _activeMenu;

        public Color BackgroundColor => Color.White;

        public MenuScene()
        {
            _mainTitleMenu = new TitleMenu();
            _mainOptionsMenu = new MainOptionsMenu();

            _activeMenu = _mainTitleMenu;

            _mainTitleMenu.LoadGameButton.OnClick += () =>
            {
                _activeMenu = _mainOptionsMenu;
            };
        }

        public void Update(float delta) => _activeMenu.Update(delta);

        public void Draw(SpriteBatch spriteBatch) => _activeMenu.Draw(spriteBatch);
    }
}