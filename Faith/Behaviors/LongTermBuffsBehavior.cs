using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Manages long term buffs, including food, non-combat potions, manuals, Free Company Actions, etc.
    /// </summary>
    class LongTermBuffsBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LongTermBuffsBehavior"/> class.
        /// </summary>
        public LongTermBuffsBehavior(ILogger<LongTermBuffsBehavior> logger) : base(logger)
        {
        }

        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
