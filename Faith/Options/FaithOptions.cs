using ff14bot.Managers;
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

        /// <summary>
        /// Lowest percent durability allowed before attempting to repair gearset.
        /// </summary>
        public float RepairDurabilityThreshold { get; set; } = 10.0f;

        /// <summary>
        /// Food <see cref="Item"/> ID to maintain buff with during dungeons.
        /// </summary>
        public uint FoodId { get; internal set; }

        /// <summary>
        /// Medicine <see cref="Item"/> ID to maintain buff with during dungeons.
        /// </summary>
        public uint MedicineId { get; internal set; }

        /// <summary>
        /// Squadron Manual <see cref="Item"/> ID to maintain buff with during dungeons.
        /// </summary>
        public uint SquadronManualId { get; internal set; }

        /// <summary>
        /// Company Action ID to maintain buff with during dungeons.
        /// </summary>
        public uint CompanyActionId { get; internal set; }
    }
}
