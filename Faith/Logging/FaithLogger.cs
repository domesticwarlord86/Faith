using Clio.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace Faith.Logging
{
    /// <summary>
    /// Custom logger that writes to RebornBuddy logs + console and terminal.
    /// </summary>
    class FaithLogger : ILogger
    {
        private static readonly Version _botbaseVersion = Assembly.GetExecutingAssembly().GetName().Version;
        private readonly FaithLoggerProvider _loggerProvider;
        /// <summary>
        /// Full name of type writing to log (e.g., "Faith.Behaviors.MainBehavior").
        /// </summary>
        private readonly string _callerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="FaithLogger"/> class.
        /// </summary>
        public FaithLogger([NotNull] FaithLoggerProvider loggerProvider, string callerName)
        {
            _loggerProvider = loggerProvider;
            _callerName = callerName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var logLine = $"[{_botbaseVersion}][{_callerName}][{logLevel}] {formatter(state, exception)} {(exception != null ? exception.StackTrace : string.Empty)}";

            ff14bot.Helpers.Logging.Write(_loggerProvider.FaithOptions.LogColor, logLine);
            Console.WriteLine(logLine);
        }
    }
}
