namespace Application.Content
{
    public interface IContentDeserializer
    {
        T Get<T>(string path);
    }
}