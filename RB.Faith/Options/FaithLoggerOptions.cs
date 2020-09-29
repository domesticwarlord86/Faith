using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RB.Faith.Options
{
    /// <summary>
    /// Options for FaithLogger.
    /// </summary>
    public class FaithLoggerOptions
    {
        /// <summary>
        /// Controls how detailed Faith logging is.
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Information;

        /// <summary>
        /// Text color of Faith log lines in the RebornBuddy console.
        /// </summary>
        public Color LogColor { get; set; } = Colors.Aqua;
    }
}
