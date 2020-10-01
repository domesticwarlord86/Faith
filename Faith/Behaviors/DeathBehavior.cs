using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Handles dying (release, accept raise, raise other) and returning to the previous location.
    /// </summary>
    class DeathBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractBehavior"/> class.
        /// </summary>
        public DeathBehavior(ILogger<DeathBehavior> logger) : base(logger)
        {
        }

        public override Task<bool> Run()
        {
            return Task.FromResult(HANDLED_EXECUTION);
        }
    }
}
