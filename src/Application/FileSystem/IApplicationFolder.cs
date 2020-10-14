namespace Application.FileSystem
{
    public interface IApplicationFolder
    {
        string Create();

        string Save<T>(string path, T data, bool shouldOverwrite);
    }
}