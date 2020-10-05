using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Navigates towards the end of the current dungeon while not in combat.
    /// </summary>
    public class DungeonNavigationBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonNavigationBehavior"/> class.
        /// </summary>
        public DungeonNavigationBehavior(
            ILogger<DungeonNavigationBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor) { }

        /// <inheritdoc/>
        public override Task<bool> Run()
        {
            //instanceDirector.GetTodoArgs() // determine dungeon progress?
            return Task.FromResult(false);
        }
    }
}
