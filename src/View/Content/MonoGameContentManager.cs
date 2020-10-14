using Microsoft.Xna.Framework.Content;

namespace ProjectSanctuary.View.Content
{
    public class MonoGameContentManager : IContentManager
    {
        private readonly ContentManager _contentManager;

        public MonoGameContentManager(ContentManager contentManager, string rootDirectory)
        {
            _contentManager = contentManager;
            _contentManager.RootDirectory = rootDirectory;
        }

        public T Load<T>(string assetName) => _contentManager.Load<T>(assetName);

        public void Unload() => _contentManager.Unload();
    }
}