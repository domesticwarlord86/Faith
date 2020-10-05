using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Exits the dungeon upon completion.
    /// </summary>
    public class DungeonExitBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonExitBehavior"/> class.
        /// </summary>
        public DungeonExitBehavior(
            ILogger<DungeonExitBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor) { }

        /// <inheritdoc/>
        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
