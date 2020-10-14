using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Input;

namespace Application.Input
{
    
    public class InputBinding
    {
        public InputAction Name { get; set; }
        
        [XmlArray(ElementName = "KeyBindings")]
        [XmlArrayItem(ElementName = "Key")]
        public List<Keys> KeyBinding { get; set; }
    }
    
}