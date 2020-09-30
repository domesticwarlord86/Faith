using Microsoft.Extensions.Logging;

namespace Faith.Logging
{
    /// <summary>
    /// Convenience-inheritable that adds logging under a standard field name.
    /// </summary>
    public abstract class Loggable
    {
        /// <summary>
        /// <see cref="ILogger"/> instance configured to display current class in log lines.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Loggable"/> class.
        /// </summary>
        public Loggable(ILogger logger)
        {
            _logger = logger;
        }
    }
}
