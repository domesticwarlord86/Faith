using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Loots nearby treasure chests and rolls for loot.
    /// </summary>
    public class LootingBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LootingBehavior"/> class.
        /// </summary>
        public LootingBehavior(
            ILogger<LootingBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor) { }

        /// <inheritdoc/>
        public override Task<bool> Run()
        {
            // TODO: Find nearby chests and set as POI + open when close
            return Task.FromResult(false);
        }
    }
}
