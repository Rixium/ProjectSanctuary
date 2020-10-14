using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Application.FileSystem;
using Application.Input;
using Microsoft.Xna.Framework.Input;

namespace Application.Configuration
{
    public class ControlOptions
    {
        private const string FileName = "controls";
        
        public InputBinding[] Bindings { get; set; } = {
            new InputBinding
            {
                Name = InputAction.MoveUp,
                KeyBinding = new List<Keys>
                {
                    Keys.W
                }
            },
            new InputBinding
            {
                Name = InputAction.MoveLeft,
                KeyBinding = new List<Keys>
                {
                    Keys.A
                }
            },
            new InputBinding
            {
                Name = InputAction.MoveRight,
                KeyBinding = new List<Keys>
                {
                    Keys.D
                }
            },
            new InputBinding
            {
                Name = InputAction.MoveDown,
                KeyBinding = new List<Keys>
                {
                    Keys.S
                }
            }
        };
        
        public static ControlOptions Load(IApplicationFolder applicationFolder)
        {
            var folderPath = applicationFolder.Create();
            var filePath = Path.Join(folderPath, $"{FileName}.xml");
            var xmlDeserializer = new XmlSerializer(typeof(ControlOptions));
            using Stream reader = new FileStream(filePath, FileMode.Open);
            var options = (ControlOptions) xmlDeserializer.Deserialize(reader);
            return options;
        }

        public void Save(IApplicationFolder applicationFolder, bool overwrite) => 
            applicationFolder.Save($"{FileName}.xml", this, overwrite);
    }
    
}