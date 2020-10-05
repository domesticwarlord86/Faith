using Microsoft.Extensions.Logging;

namespace Faith.Logging
{
    /// <summary>
    /// Adds logging under a standard field name.
    /// </summary>
    public abstract class AbstractLoggable
    {
        /// <summary>
        /// <see cref="ILogger"/> instance configured to display current class in log lines.
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractLoggable"/> class.
        /// </summary>
        public AbstractLoggable(ILogger logger)
        {
            Logger = logger;
        }
    }
}
