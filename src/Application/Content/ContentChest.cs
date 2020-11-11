using System.Threading.Tasks;

namespace Application.Content
{
    public class ContentChest : IContentChest
    {
        public IContentManager Content { get; set; }

        public async Task Preload<T>(params string[] assets)
        {
            foreach (var asset in assets)
            {
                await Task.Run(() => Content.Load<T>(asset));
            }
        }

        public void Unload() => Content.Unload();

        public T Get<T>(string assetName) => Content.Load<T>(assetName);
    }
}