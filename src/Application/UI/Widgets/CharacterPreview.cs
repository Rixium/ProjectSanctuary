using Application.Content.ContentTypes;
using Application.Entities;
using Application.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    internal class CharacterPreview : Widget, ICharacter
    {
        private readonly ICharacterRenderer _characterRenderer;

        public CharacterPreview(ICharacterRenderer characterRenderer, Vector2 position)
        {
            _characterRenderer = characterRenderer;
            Position = position;
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            _characterRenderer.Render(spriteBatch, this);
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {
        }

        public Vector2 Position { get; set; }
        public Head Head { get; set; }
        public Hair Hair { get; set; }
    }
}