using Microsoft.Extensions.Logging;

namespace Faith.Windows
{
    /// <summary>
    /// ViewModel-style DataContexxt behind <see cref="BotbaseWindow"/>.
    /// </summary>
    public class BotbaseWindowViewModel
    {
        private readonly ILogger<BotbaseWindowViewModel> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BotbaseWindowViewModel"/> class.
        /// </summary>
        /// <param name="logger"></param>
        public BotbaseWindowViewModel(ILogger<BotbaseWindowViewModel> logger)
        {
            _logger = logger;
        }
    }
}