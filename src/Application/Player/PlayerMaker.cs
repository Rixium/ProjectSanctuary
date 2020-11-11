using Microsoft.Xna.Framework;

namespace Application.Player
{
    internal class PlayerMaker : IPlayerMaker
    {
        
        public string Name { get; private set; }
        public int Pronoun { get; private set; }
        public int Hair { get; private set; }
        public Color BodyColor { get; private set; }
        public Color HairColor { get; private set; }

        public void SetName(string name) => Name = name;

        public void SetPronouns(int pronoun) => Pronoun = pronoun;

        public void SetHair(int hair) => Hair = hair;

        public void SetBodyColor(Color color) => BodyColor = color;

        public void SetHairColor(Color color) => HairColor = color;
    }
}