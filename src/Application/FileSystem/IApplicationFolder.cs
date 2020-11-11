namespace Application.FileSystem
{
    public interface IApplicationFolder
    {
        void SetDirectoryName(string projectSanctuary);
        string Create();

        string Save<T>(string path, T data, bool shouldOverwrite);
    }
}