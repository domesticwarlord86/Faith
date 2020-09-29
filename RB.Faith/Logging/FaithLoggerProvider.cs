using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RB.Faith.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Faith.Logging
{
    [ProviderAlias("Faith")]
    class FaithLoggerProvider : ILoggerProvider
    {
        private readonly IOptionsMonitor<FaithLoggerOptions> _optionsMonitor;
        public FaithLoggerOptions Options => _optionsMonitor.CurrentValue;

        public FaithLoggerProvider(IOptionsMonitor<FaithLoggerOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
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
