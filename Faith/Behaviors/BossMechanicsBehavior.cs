using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Performs special mechanics required by bosses.
    /// </summary>
    public class BossMechanicsBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BossMechanicsBehavior"/> class.
        /// </summary>
        public BossMechanicsBehavior(
            ILogger<BossMechanicsBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor)
        {
        }

        /// <inheritdoc/>
        public override Task<bool> Run()
        {
            // TODO: Handle boss mechanics
            return Task.FromResult(false);
        }
    }
}
