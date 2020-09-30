using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Exits the dungeon upon completion.
    /// </summary>
    class DungeonExitBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonExitBehavior"/> class.
        /// </summary>
        public DungeonExitBehavior(ILogger<DungeonExitBehavior> logger) : base(logger)
        {
        }

        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
