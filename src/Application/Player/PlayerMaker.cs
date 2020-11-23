using Microsoft.Xna.Framework;

namespace Application.Player
{
    internal class PlayerMaker : IPlayerMaker
    {

        public string Name { get; private set; } = string.Empty;
        public int Pronouns { get; private set; }
        public int Hair { get; private set; }
        public int Head { get; set; }
        public Color BodyColor { get; private set; }
        public Color HairColor { get; private set; }

        public void SetName(string name) => Name = name;

        public void SetPronouns(int pronoun) => Pronouns = pronoun;

        public void SetHair(int hair) => Hair = hair;
        public void SetHead(int head) => Head = head;

        public void SetBodyColor(Color color) => BodyColor = color;

        public void SetHairColor(Color color) => HairColor = color;
    }
}