using Application.FileSystem;

namespace Application.Configuration
{
    internal class OptionsManager : IOptionsManager
    {
        private readonly IApplicationFolder _applicationFolder;

        public ControlOptions ControlOptions { get; private set; }

        public OptionsManager(IApplicationFolder applicationFolder)
        {
            _applicationFolder = applicationFolder;
        }

        public void Initialize()
        {
            ControlOptions = ControlOptions.Load(_applicationFolder);
        }
        
    }
}