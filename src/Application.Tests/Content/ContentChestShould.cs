using Application.Content;
using Microsoft.Xna.Framework.Graphics;
using NSubstitute;
using NUnit.Framework;

namespace ProjectSanctuary.Application.Tests.Content
{
    internal class ContentChestShould
    {
        private ContentChest _contentChest;
        private IContentManager _contentManager;

        [SetUp]
        public void SetUp()
        {
            _contentManager = Substitute.For<IContentManager>();
            _contentChest = new ContentChest
            {
                Content = _contentManager
            };
        }

        [Test]
        public void PreloadContent()
        {
            _contentChest.Preload<Texture2D>("someAsset");

            _contentManager.Received(1).Load<Texture2D>("someAsset");
        }

        [Test]
        public void UnloadContent()
        {
            _contentChest.Unload();

            _contentManager.Received(1).Unload();
        }
    }
}