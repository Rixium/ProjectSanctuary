using Application.FileSystem;

namespace Application.Configuration
{
    internal class OptionsManager : IOptionsManager
    {
        private readonly IApplicationFolder _applicationFolder;

        public ControlOptions ControlOptions { get; private set; }
        public PronounOptions PronounOptions { get; private set; }

        public OptionsManager(IApplicationFolder applicationFolder, ControlOptions controlOptions, PronounOptions pronounOptions)
        {
            _applicationFolder = applicationFolder;
            
            ControlOptions = controlOptions;
            PronounOptions = pronounOptions;
        }

        public void Initialize()
        {
            ControlOptions = ControlOptions.Load(_applicationFolder);
            PronounOptions = PronounOptions.Load(_applicationFolder);
        }

        public void Save(bool overwrite)
        {
            ControlOptions.Save(_applicationFolder, overwrite);
            PronounOptions.Save(_applicationFolder, overwrite);
        }
    }
}