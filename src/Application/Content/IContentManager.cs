namespace Application.Content
{
    public interface IContentManager
    {

        string RootDirectory { get; }
        T Load<T>(string assetName);
        void Unload();

    }
}