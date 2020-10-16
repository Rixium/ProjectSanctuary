using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View.Menus;

namespace ProjectSanctuary.View.Scenes
{
    public class MenuScene : IScene
    {
        private TitleMenu _mainTitleMenu;
        private MainOptionsMenu _mainOptionsMenu;
        
        public Color BackgroundColor => Color.White;

        public MenuScene()
        {
            _mainTitleMenu = new TitleMenu();
            _mainOptionsMenu = new MainOptionsMenu();

        }

        public void Update(float delta)
        {
            _mainTitleMenu.Update(delta);
            _mainOptionsMenu.Update(delta);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _mainTitleMenu.Draw(spriteBatch);
            _mainOptionsMenu.Draw(spriteBatch);
        }
    }
}