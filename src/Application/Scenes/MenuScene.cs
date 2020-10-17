using Application.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Scenes
{
    public class MenuScene : IScene
    {
        private IMenu _activeMenu;

        public Color BackgroundColor => Color.White;

        public MenuScene()
        {
            var mainTitleMenu = new TitleMenu();
            var mainOptionsMenu = new MainOptionsMenu();

            _activeMenu = mainTitleMenu;

            mainTitleMenu.LoadGameButton.OnClick += () =>
            {
                _activeMenu = mainOptionsMenu;
            };
        }

        public void Update(float delta) => _activeMenu.Update(delta);

        public void Draw(SpriteBatch spriteBatch) => _activeMenu.Draw(spriteBatch);
    }
}