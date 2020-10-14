using Microsoft.Xna.Framework.Content;

namespace ProjectSanctuary.View.Content
{
    public class MonoGameContentManager : IContentManager
    {
        private readonly ContentManager _contentManager;

        public MonoGameContentManager(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public T Load<T>(string assetName) => _contentManager.Load<T>(assetName);

        public void Unload() => _contentManager.Unload();
    }
}