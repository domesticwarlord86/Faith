using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Navigates towards the end of the current dungeon while not in combat.
    /// </summary>
    class DungeonNavigationBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonNavigationBehavior"/> class.
        /// </summary>
        public DungeonNavigationBehavior(ILogger<DungeonNavigationBehavior> logger) : base(logger)
        {
        }

        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
