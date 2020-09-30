using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Loots nearby treasure chests and rolls for loot.
    /// </summary>
    class LootingBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LootingBehavior"/> class.
        /// </summary>
        public LootingBehavior(ILogger<LootingBehavior> logger) : base(logger)
        {
        }

        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
