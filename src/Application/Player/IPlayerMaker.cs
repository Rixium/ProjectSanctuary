using Application.Configuration;
using Microsoft.Xna.Framework;

namespace Application.Player
{
    public interface IPlayerMaker
    {
        void SetName(string name);
        void SetPronouns(Pronoun pronoun);
        void SetHair(int hair);
        void SetBodyColor(Color color);
        void SetHairColor(Color color);
    }
}