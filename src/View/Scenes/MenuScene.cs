using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View.Content;

namespace ProjectSanctuary.View.Scenes
{
    public class MenuScene : IScene
    {
        private Texture2D _menuButtons;
        private Rectangle _titleImageSource;
        private float titleYOffset;
        
        public Color BackgroundColor => Color.White;

        public MenuScene()
        {
            _menuButtons = ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons");
            _titleImageSource = new Rectangle(0, 241, 258, 67);
            titleYOffset = ViewManager.ViewPort.Height / 2.0f - 50;
        }

        public void Update(float delta)
        {
            if (titleYOffset > 0)
            {
                titleYOffset = MathHelper.Lerp(titleYOffset, 0, delta);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_menuButtons, new Vector2(ViewManager.ViewPort.Width, ViewManager.ViewPort.Height - ViewManager.ViewPort.Height / 2.0f) / 2.0f + new Vector2(0, titleYOffset),
                _titleImageSource, Color.White, 0f,
                new Vector2(_titleImageSource.Width, _titleImageSource.Height) / 2.0f, 2f, SpriteEffects.None, 0.2f);
            spriteBatch.End();
        }
    }
}