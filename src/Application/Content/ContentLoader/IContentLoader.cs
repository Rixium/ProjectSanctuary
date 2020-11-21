namespace Application.Content.ContentLoader
{
    public interface IContentLoader<T>
    {
        T GetContent(string data);
    }
}