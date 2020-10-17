namespace Application.Content
{
    public interface IContentManager
    {

        T Load<T>(string assetName);
        void Unload();
        
    }
}