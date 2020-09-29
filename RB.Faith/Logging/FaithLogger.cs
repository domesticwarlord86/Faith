using Buddy.Service.Core;
using Clio.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RB.Faith.Logging
{
    class FaithLogger : ILogger
    {
        private static readonly AssemblyName assembly = Assembly.GetExecutingAssembly().GetName();
        private readonly FaithLoggerProvider _loggerProvider;
        private readonly string _categoryName;

        public FaithLogger([NotNull] FaithLoggerProvider loggerProvider, string categoryName)
        {
            _loggerProvider = loggerProvider;
            _categoryName = categoryName;
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

            var logLine = $"[{assembly.Name}][{assembly.Version}][{logLevel}] {formatter(state, exception)} {(exception != null ? exception.StackTrace : string.Empty)}";

            ff14bot.Helpers.Logging.Write(_loggerProvider.Options.LogColor, logLine);
            Console.WriteLine(logLine);
        }
    }
}
