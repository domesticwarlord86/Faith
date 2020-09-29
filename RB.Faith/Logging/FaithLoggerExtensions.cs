using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RB.Faith.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Faith.Logging
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
        /// <param name="configure"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddFaithLogger(this ILoggingBuilder builder, Action<FaithLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, FaithLoggerProvider>();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
