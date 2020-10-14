using System.IO;
using System.Xml.Serialization;
using Application.Input;

namespace Application.Configuration
{
    public class ControlOptions
    {

        public InputBinding[] Bindings;
        
        public static ControlOptions LoadFrom(string path)
        {
            var xmlDeserializer = new XmlSerializer(typeof(ControlOptions));
            using Stream reader = new FileStream(path, FileMode.Open);
            var options = (ControlOptions) xmlDeserializer.Deserialize(reader);
            return options;
        }
    }
    
}