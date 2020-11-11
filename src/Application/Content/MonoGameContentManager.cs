using Microsoft.Xna.Framework.Content;

namespace Application.Content
{
    public class MonoGameContentManager : IContentManager
    {
        public ContentManager ContentManager { get; set; }
        
        public T Load<T>(string assetName) => ContentManager.Load<T>(assetName);

        public void Unload() => ContentManager.Unload();
    }
}