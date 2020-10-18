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
        public void WindowResized()
        {
            _mainTitleMenu.WindowResized();
            _mainOptionsMenu.WindowResized();
        }
    }
}