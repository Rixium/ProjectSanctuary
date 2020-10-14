using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Application.Input;
using Microsoft.Xna.Framework.Input;

namespace Application.Configuration
{
    public class ControlOptions
    {

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
        
        public static ControlOptions LoadFrom(string path)
        {
            path = Path.Join(path, "controls.xml");
            var xmlDeserializer = new XmlSerializer(typeof(ControlOptions));
            using Stream reader = new FileStream(path, FileMode.Open);
            var options = (ControlOptions) xmlDeserializer.Deserialize(reader);
            return options;
        }
    }
    
}