using Faith.Logging;
using Microsoft.Extensions.Logging;

namespace Faith.Windows
{
    /// <summary>
    /// ViewModel-style DataContext behind <see cref="BotBaseWindow"/>.
    /// </summary>
    public class BotBaseWindowViewModel : AbstractLoggable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BotBaseWindowViewModel"/> class.
        /// </summary>
        public BotBaseWindowViewModel(ILogger<BotBaseWindowViewModel> logger) : base(logger)
        {
        }
    }
}