using Clio.Utilities;
using Faith.Localization;

namespace Faith.Navigation
{
    /// <summary>
    /// Represents an individual destination along a <see cref="IRoute"/>.
    /// </summary>
    public class Waypoint
    {
        /// <summary>
        /// Zone ID of the destination.
        /// </summary>
        public uint ZoneId { get; }

        /// <summary>
        /// Sub-zone ID of the destination.
        /// </summary>
        public uint SubZoneId { get; }

        /// <summary>
        /// Coordinates of the destination.
        /// </summary>
        public Vector3 Location { get; }

        /// <summary>
        /// Short description of the destination.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Waypoint"/> class.
        /// </summary>
        /// <param name="zoneId">Zone ID of the destination.</param>
        /// <param name="subZoneId">Sub-Zone ID of the destination.</param>
        /// <param name="location">Coordinates of the destination.</param>
        /// <param name="description">Short description of the destination.</param>
        public Waypoint(uint zoneId, uint subZoneId, Vector3 location, string description = null)
        {
            ZoneId = zoneId;
            SubZoneId = subZoneId;
            Location = location;
            Description = description;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"<{Translations.WAYPOINT}: {ZoneId}, {SubZoneId}, {Location}, \"{Description}\">";
        }
    }
}
