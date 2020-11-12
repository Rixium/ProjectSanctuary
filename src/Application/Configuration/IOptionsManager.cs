namespace Application.Configuration
{
    public interface IOptionsManager
    {
        ControlOptions ControlOptions { get; }
        PronounOptions PronounOptions { get; }
        void Initialize();
        void Save(bool overwrite);
    }
}