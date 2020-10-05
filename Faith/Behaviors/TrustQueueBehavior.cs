using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Configures Trust Party and queues for Trust Dungeons.
    /// </summary>
    public class TrustQueueBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrustQueueBehavior"/> class.
        /// </summary>
        public TrustQueueBehavior(
            ILogger<TrustQueueBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor) { }

        /// <inheritdoc/>
        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
