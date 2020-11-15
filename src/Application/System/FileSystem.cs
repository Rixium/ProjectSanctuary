using System.IO;

namespace Application.System
{
    public class FileSystem : IFileSystem
    {
        public string ReadAllText(string path) => File.ReadAllText(path);
    }
}