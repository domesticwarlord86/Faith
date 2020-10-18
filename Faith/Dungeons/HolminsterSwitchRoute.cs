using Clio.Utilities;
using Faith.Logging;
using Faith.Navigation;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Faith.Dungeons
{
    /// <summary>
    /// 
    /// </summary>
    public class HolminsterSwitchRoute : AbstractLoggable, IRoute
    {
        /// <inheritdoc/>
        protected const uint ZoneId = 837;

        /// <inheritdoc/>
        protected Waypoint[] _waypoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="HolminsterSwitchRoute"/> class.
        /// </summary>
        public HolminsterSwitchRoute(
            ILogger<HolminsterSwitchRoute> logger
        ) : base(logger)
        {
            // TODO: Load from file
            _waypoints = new Waypoint[]
            {
                new Waypoint(ZoneId, 0, new Vector3(-97.21527f, 0.05143476f, 326.7719f), "Web Wall 1"),
                new Waypoint(ZoneId, 0, new Vector3(-13.47375f, 0.3121536f, 286.1218f), "Web Wall 2"),
                new Waypoint(ZoneId, 0, new Vector3(-14.02307f, 2.384186E-07f, 231.9219f), "Boss: Forgiven Dissonance"),
            };
        }

        /// <inheritdoc/>
        public Queue<Waypoint> Calculate(Vector3 startingPos)
        {
            int nearest = GetIndexOfNearest(startingPos);

            // Skip the to the physically nearest waypoint and continue through end
            // TODO: Translations
            Logger.LogInformation($"Starting from waypoint {nearest + 1}/{_waypoints.Length}: {_waypoints[nearest]}");
            return new Queue<Waypoint>(_waypoints.Skip(nearest));
        }


        private int GetIndexOfNearest(Vector3 startingPos)
        {
            int nearest = 0;
            float minDistance = float.MaxValue;

            for (int i = 0; i < _waypoints.Count(); i++)
            {
                float distance = _waypoints[i].Location.Distance3D(startingPos);
                if (distance < minDistance)
                {
                    nearest = i;
                    minDistance = distance;
                }
            }

            return nearest;
        }
    }
}
