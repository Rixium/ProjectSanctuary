namespace Application.Content
{
    public interface IContentChest
    {
        void Preload<T>(params string[] assets);
        void Unload();
        T Get<T>(string assetName);
        IContentManager Content { get; set; }
    }
}