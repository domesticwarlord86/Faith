using Clio.Utilities;
using Faith.Dungeons;
using Faith.Logging;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Faith.Navigation
{
    /// <summary>
    /// Plans <see cref="IRoute"/>s through dungeons that complete all objectives.
    /// </summary>
    public class DungeonRouteCalculator : AbstractLoggable
    {
        private readonly Dictionary<DungeonId, IRoute> _dungeonRoutes;

        // TODO: Load from file?
        private static readonly Dictionary<uint, DungeonId> _zoneToDungeon = new Dictionary<uint, DungeonId>
        {
            { 837, DungeonId.HolminsterSwitch },
            { 821, DungeonId.DohnMheg },
            { 823, DungeonId.TheQitanaRavel},
            { 836, DungeonId.MalikahsWell },
            { 822, DungeonId.MtGulg },
            { 838, DungeonId.Amaurot },
            { 884, DungeonId.TheGrandCosmos },
            { 898, DungeonId.AnamnesisAnyder},
            { 916, DungeonId.TheHeroesGauntlet },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonRouteCalculator"/> class.
        /// </summary>
        public DungeonRouteCalculator(
            ILogger<DungeonRouteCalculator> logger,
            Dictionary<DungeonId, IRoute> dungeonRoutes
        ) : base(logger)
        {
            _dungeonRoutes = dungeonRoutes;
        }

        /// <summary>
        /// Gets the remaining <see cref="Waypoint"/>s in a dungeon, relative to the given starting position.
        /// </summary>
        /// <param name="dungeon">Dungeon to plan a <see cref="IRoute"/> for.</param>
        /// <param name="startingPos">Position to resume the <see cref="IRoute"/> from.</param>
        /// <returns>Remaining <see cref="Waypoint"/>s to complete the dungeon.</returns>
        public Queue<Waypoint> Calculate(DungeonId dungeon, Vector3 startingPos)
        {
            return _dungeonRoutes[dungeon].Calculate(startingPos);
        }

        /// <summary>
        /// Gets the remaining <see cref="Waypoint"/>s in a dungeon, relative to the given starting position.
        /// </summary>
        /// <param name="dungeonZoneId">Dungeon to plan a <see cref="IRoute"/> for.</param>
        /// <param name="startingPos">Position to resume the <see cref="IRoute"/> from.</param>
        /// <returns>Remaining <see cref="Waypoint"/>s to complete the dungeon.</returns>
        public Queue<Waypoint> Calculate(uint dungeonZoneId, Vector3 startingPos)
        {
            return Calculate(_zoneToDungeon[dungeonZoneId], startingPos);
        }
    }
}
