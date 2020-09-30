using System.Windows.Media;

namespace Faith.Options
{
    /// <summary>
    /// Strongly typed configuration for Faith BotBase.
    /// </summary>
    public class FaithOptions
    {
        /// <summary>
        /// Text color of Faith log lines in the RebornBuddy console.
        /// </summary>
        public Color LogColor { get; set; } = Colors.Aqua;

        /// <summary>
        /// Faith's UI and logging language (e.g., "en-US", "zh", "es-ES").
        /// 
        /// See this list of <a href="http://www.codedigest.com/CodeDigest/207-Get-All-Language-Country-Code-List-for-all-Culture-in-C---ASP-Net.aspx">culture codes</a>.
        /// </summary>
        public string Localization { get; set; } = "en-US";
    }
}
