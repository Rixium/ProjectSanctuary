using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Application.FileSystem;
using Application.Input;
using Microsoft.Xna.Framework.Input;

namespace Application.Configuration
{
    public class Pronoun
    {
        public string Subjective { get; set; }
        public string Objective { get; set; }
        public string PossessiveAdjective { get; set; }
        public string PossessivePronoun { get; set; }
        public string Reflexive { get; set; }
    }

    public class PronounOptions
    {
        private const string FileName = "pronouns";

        public Pronoun[] Pronouns { get; set; } =
        {
            new Pronoun
            {
                Subjective = "He",
                Objective = "Him",
                PossessiveAdjective = "His",
                PossessivePronoun = "His",
                Reflexive = "Himself"
            },
            new Pronoun
            {
                Subjective = "She",
                Objective = "Her",
                PossessiveAdjective = "Her",
                PossessivePronoun = "Hers",
                Reflexive = "Herself"
            },
            new Pronoun
            {
                Subjective = "They",
                Objective = "Them",
                PossessiveAdjective = "Their",
                PossessivePronoun = "Theirs",
                Reflexive = "Themself"
            }
        };

        public static PronounOptions Load(IApplicationFolder applicationFolder)
        {
            var folderPath = applicationFolder.Create();
            var filePath = Path.Join(folderPath, $"{FileName}.xml");
            var xmlDeserializer = new XmlSerializer(typeof(PronounOptions));
            using Stream reader = new FileStream(filePath, FileMode.Open);
            var options = (PronounOptions) xmlDeserializer.Deserialize(reader);
            return options;
        }

        public void Save(IApplicationFolder applicationFolder, bool overwrite) =>
            applicationFolder.Save($"{FileName}.xml", this, overwrite);
    }
}