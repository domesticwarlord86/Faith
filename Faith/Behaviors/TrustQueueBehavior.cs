using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Configures Trust Party and queues for Trust Dungeons.
    /// </summary>
    class TrustQueueBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrustQueueBehavior"/> class.
        /// </summary>
        public TrustQueueBehavior(ILogger<TrustQueueBehavior> logger) : base(logger)
        {
        }

        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
