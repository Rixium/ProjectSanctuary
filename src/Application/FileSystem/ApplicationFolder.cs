using System;
using System.IO;
using System.Xml.Serialization;

namespace Application.FileSystem
{
    internal class ApplicationFolder : IApplicationFolder
    {
        private string _gameName = "DefaultApplication";

        public void SetDirectoryName(string directoryName) => _gameName = directoryName;

        public string Create()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var applicationFolder = Path.Combine(appDataPath, _gameName);

            Directory.CreateDirectory(applicationFolder);

            return applicationFolder;
        }

        public string Save<T>(string path, T data, bool shouldOverwrite)
        {
            var appDataFolder = Create();
            path = Path.Join(appDataFolder, path);
            
            if (File.Exists(path) && !shouldOverwrite)
            {
                return path;
            }

            var writer = new XmlSerializer(typeof(T));
            
            using var file = File.Create(path);
            writer.Serialize(file, data);  
            
            return path;
        }
    }
}