using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Faith.Logging
{
    [ProviderAlias("Faith")]
    class FaithLoggerProvider : ILoggerProvider
    {
        private readonly IOptionsMonitor<FaithOptions> _faithOptionsMonitor;
        public FaithOptions FaithOptions => _faithOptionsMonitor.CurrentValue;

        public FaithLoggerProvider(IOptionsMonitor<FaithOptions> optionsMonitor)
        {
            _faithOptionsMonitor = optionsMonitor;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FaithLogger(this, categoryName);
        }

        public void Dispose()
        {
        }
    }
}
