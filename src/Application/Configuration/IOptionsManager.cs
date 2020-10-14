namespace Application.Configuration
{
    public interface IOptionsManager
    {
        ControlOptions ControlOptions { get; }
        void Initialize();
    }
}