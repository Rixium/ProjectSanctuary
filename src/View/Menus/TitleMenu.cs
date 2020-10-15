using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSanctuary.View.Content;
using ProjectSanctuary.View.Graphics;
using ProjectSanctuary.View.UI;
using ProjectSanctuary.View.Utils;

namespace ProjectSanctuary.View.Menus
{
    public class TitleMenu : Menu
    {
        private readonly Sprite _signTopSprite;
        private readonly Vector2 _signTopPosition;

        private readonly Texture2D _menuButtons;
        private readonly Rectangle _titleImageSource;
        private float _titleYOffset;

        private readonly Camera _camera = new Camera();

        public TitleMenu()
        {
            _titleImageSource = new Rectangle(0, 241, 258, 67);
            _titleYOffset = ViewManager.ViewPort.Height / 2.0f - 50;

            _menuButtons = ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons");

            _signTopSprite = new Sprite(_menuButtons, new Rectangle(0, 0, 48, 22));
            _signTopPosition = ViewManager.ViewPort.Center();

            var newButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 22, 48, 14)),
                new Sprite(_menuButtons, new Rectangle(48, 22, 48, 14)),
                _signTopPosition + new Vector2(0, 18));

            var loadButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 36, 48, 16)),
                new Sprite(_menuButtons, new Rectangle(48, 36, 48, 16)),
                _signTopPosition + new Vector2(0, 18 + newButton.Height));

            var exitButton = new TexturedButton(
                new Sprite(_menuButtons, new Rectangle(0, 52, 48, 16)),
                new Sprite(_menuButtons, new Rectangle(48, 52, 48, 16)),
                _signTopPosition + new Vector2(0, 18 + newButton.Height + loadButton.Height));

            Clickables.Add(newButton);
            Clickables.Add(loadButton);
            Clickables.Add(exitButton);
            
            _camera.Position = ViewManager.ViewPort.Center();
            _camera.Zoom = 3;
        }

        public override void Update(float delta)
        {
            if (_titleYOffset > 0)
            {
                _titleYOffset = MathHelper.Lerp(_titleYOffset, 0, delta);
            }

            base.Update(delta);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.GetMatrix());
            spriteBatch.Draw(_menuButtons,
                ViewManager.ViewPort.Center() - new Vector2(0, ViewManager.ViewPort.Center().Y / 2.0f / _camera.Zoom + _titleYOffset),
                _titleImageSource, Color.White, 0f,
                new Vector2(_titleImageSource.Width, _titleImageSource.Height) / 2.0f, 1f, SpriteEffects.None, 0.2f);

            spriteBatch.Draw(_signTopSprite.Texture, _signTopPosition, _signTopSprite.Source, Color.White, 0f,
                _signTopSprite.Origin, 1f,
                SpriteEffects.None, 0);

            foreach (var clickable in Clickables)
            {
                clickable.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}