using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View.Menus;

namespace ProjectSanctuary.View.Scenes
{
    public class MenuScene : IScene
    {
        private TitleMenu _menu;
        
        public Color BackgroundColor => Color.White;

        public MenuScene() => _menu = new TitleMenu();

        public void Update(float delta) => _menu.Update(delta);

        public void Draw(SpriteBatch spriteBatch) => _menu.Draw(spriteBatch);
    }
}