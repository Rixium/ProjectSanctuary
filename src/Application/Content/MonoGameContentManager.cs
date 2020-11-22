using System.IO;
using Microsoft.Xna.Framework.Content;

namespace Application.Content
{
    public class MonoGameContentManager : IContentManager
    {
        public ContentManager ContentManager { get; set; }
        public string RootDirectory => ContentManager.RootDirectory;

        public T Load<T>(string assetName)
        {
            var fixedPath = assetName.Replace($"assets{Path.DirectorySeparatorChar}", "");
            return ContentManager.Load<T>(fixedPath);
        }

        public void Unload() => ContentManager.Unload();
    }
}