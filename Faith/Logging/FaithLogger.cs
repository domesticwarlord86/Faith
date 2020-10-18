using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;

namespace Faith.Logging
{
    /// <summary>
    /// Custom logger that writes to RebornBuddy logs + console and terminal.
    /// </summary>
    internal class FaithLogger : ILogger
    {
        private static readonly Version _botbaseVersion = Assembly.GetExecutingAssembly().GetName().Version;
        private readonly IOptionsMonitor<FaithOptions> _faithOptionsMonitor;
        private readonly IOptionsMonitor<LoggerFilterOptions> _loggingOptionsMonitor;

        private FaithOptions FaithOptions => _faithOptionsMonitor.CurrentValue;

        /// <summary>
        /// Full name of type writing to log (e.g., "Faith.Behaviors.MainBehavior").
        /// </summary>
        private readonly string _callerName;

        /// <summary>
        /// Current <see cref="Microsoft.Extensions.Logging.LogLevel"/> to filter logging.
        /// </summary>
        private LogLevel _logLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="FaithLogger"/> class.
        /// </summary>
        public FaithLogger(
            IOptionsMonitor<FaithOptions> faithOptionsMonitor,
            IOptionsMonitor<LoggerFilterOptions> loggingOptionsMonitor,
            string callerName
        )
        {
            _faithOptionsMonitor = faithOptionsMonitor;
            _loggingOptionsMonitor = loggingOptionsMonitor;
            _callerName = callerName;

            _loggingOptionsMonitor.OnChange((options) =>
                _logLevel = options.Rules.FirstOrDefault(r => r.CategoryName == "Faith")?.LogLevel ?? LogLevel.Information
            );
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            string logLine = $"[{_botbaseVersion}][{_callerName}][{logLevel}] {formatter(state, exception)} {(exception != null ? exception.StackTrace : string.Empty)}";

            ff14bot.Helpers.Logging.Write(FaithOptions.LogColor, logLine);
            Console.WriteLine(logLine);
        }
    }
}
