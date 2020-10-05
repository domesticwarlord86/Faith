using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Manages long term buffs, including food, non-combat potions, manuals, Free Company Actions, etc.
    /// </summary>
    public class LongTermBuffsBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LongTermBuffsBehavior"/> class.
        /// </summary>
        public LongTermBuffsBehavior(
            ILogger<LongTermBuffsBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor) { }

        /// <inheritdoc/>
        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
