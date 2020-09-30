using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Faith.Logging
{
    /// <summary>
    /// Extension methods for FaithLogger classes.
    /// </summary>
    public static class FaithLoggerExtensions
    {
        /// <summary>
        /// Registers <see cref="FaithLoggerProvider"/> with Microsoft's Dependency Injection and Logging extensions.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddFaithLogger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, FaithLoggerProvider>();

            return builder;
        }
    }
}
