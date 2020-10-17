using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View.Menus;

namespace ProjectSanctuary.View.Scenes
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
        }

        public void Update(float delta) => _activeMenu.Update(delta);

        public void Draw(SpriteBatch spriteBatch) => _activeMenu.Draw(spriteBatch);
    }
}