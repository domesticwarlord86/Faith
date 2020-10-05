using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Faith.Logging
{
    [ProviderAlias("Faith")]
    class FaithLoggerProvider : ILoggerProvider
    {
        private readonly IOptionsMonitor<FaithOptions> _faithOptionsMonitor;
        private readonly IOptionsMonitor<LoggerFilterOptions> _loggingOptionsMonitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FaithLoggerProvider"/> class.
        /// </summary>
        public FaithLoggerProvider(
            IOptionsMonitor<FaithOptions> faithOptionsMonitor,
            IOptionsMonitor<LoggerFilterOptions> loggingOptionsMonitor
        )
        {
            _faithOptionsMonitor = faithOptionsMonitor;
            _loggingOptionsMonitor = loggingOptionsMonitor;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FaithLogger(_faithOptionsMonitor, _loggingOptionsMonitor, categoryName);
        }

        public void Dispose()
        {
        }
    }
}
