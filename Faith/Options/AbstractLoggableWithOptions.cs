using Faith.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Faith.Options
{
    /// <summary>
    /// Adds options under a standard field name.
    /// </summary>
    public abstract class AbstractLoggableWithOptions : AbstractLoggable
    {
        /// <summary>
        /// Tracks the live state of <see cref="FaithOptions"/> in settings file, env vars, etc.
        /// </summary>
        private readonly IOptionsMonitor<FaithOptions> _faithOptionsMonitor;

        /// <summary>
        /// Gets the current values for <see cref="FaithOptions"/>.
        /// </summary>
        protected FaithOptions FaithOptions => _faithOptionsMonitor.CurrentValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractLoggableWithOptions"/> class.
        /// </summary>
        public AbstractLoggableWithOptions(
            ILogger logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger)
        {
            _faithOptionsMonitor = faithOptionsMonitor;
        }
    }
}
