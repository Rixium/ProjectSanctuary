namespace ProjectSanctuary.View.Content
{
    public class ContentChest : IContentChest
    {
        private readonly IContentManager _content;

        public ContentChest(IContentManager content) =>
            _content = content;

        public void Preload<T>(params string[] assets)
        {
            foreach (var asset in assets)
            {
                _content.Load<T>(asset);
            }
        }

        public void Unload() => _content.Unload();

        public T Get<T>(string assetName) => _content.Load<T>(assetName);
    }
}