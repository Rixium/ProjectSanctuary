namespace Application.System
{
    public interface IFileSystem
    {
        string ReadAllText(string path);
    }
}