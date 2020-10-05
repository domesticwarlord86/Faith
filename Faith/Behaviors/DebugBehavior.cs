using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Forces the bot to restart the <see cref="MainBehavior"/> loop from the top.
    /// </summary>
    public class DebugBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugBehavior"/> class.
        /// </summary>
        public DebugBehavior(
            ILogger<DebugBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor) { }

        /// <inheritdoc/>
        public override Task<bool> Run()
        {
            return Task.FromResult(HANDLED_EXECUTION);
        }
    }
}
