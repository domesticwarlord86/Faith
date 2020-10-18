using Faith.Localization;
using Faith.Navigation;
using Faith.Options;
using ff14bot;
using ff14bot.Behavior;
using ff14bot.Managers;
using ff14bot.Pathing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Navigates towards the end of the current dungeon while not in combat.
    /// </summary>
    public class DungeonNavigationBehavior : AbstractBehavior
    {
        private const float _minDistance = 2.5f;
        private readonly DungeonRouteCalculator _routeCalculator;
        private Queue<Waypoint> _waypoints;

        private Waypoint _current;
        private MoveToParameters _moveParams;

        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonNavigationBehavior"/> class.
        /// </summary>
        public DungeonNavigationBehavior(
            ILogger<DungeonNavigationBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor,
            DungeonRouteCalculator routeCalculator
        ) : base(logger, faithOptionsMonitor)
        {
            _routeCalculator = routeCalculator;
        }

        /// <inheritdoc/>
        public override async Task<bool> Run()
        {
            if (_waypoints == null)
            {
                _waypoints = _routeCalculator.Calculate(WorldManager.ZoneId, Core.Player.Location);
            }

            if (_current == null)
            {
                if (_waypoints.Count == 0)
                {
                    // Nowhere left to go (end of dungeon?)
                    Logger.LogInformation(Translations.LOG_NAVIGATION_NO_WAYPOINT);

                    return PASS_EXECUTION;
                }
                else
                {
                    _current = _waypoints.Dequeue();
                    Logger.LogInformation(Translations.LOG_NAVIGATION_NEW_WAYPOINT, _current.ZoneId, _current.SubZoneId, _current.Location);
                }
            }

            if (_current.Location.Distance(Core.Player.Location) > _minDistance)
            {
                if (_moveParams == null)
                {
                    _moveParams = new MoveToParameters(_current.Location, _current.Description);
                }

                await CommonTasks.MoveAndStop(_moveParams, _minDistance, stopInRange: true);
            }
            else
            {
                _moveParams = null;
                _current = null;
                Logger.LogInformation(Translations.LOG_NAVIGATION_REACHED_WAYPOINT, _current.Description);
            }

            return HANDLED_EXECUTION;
        }
    }
}
