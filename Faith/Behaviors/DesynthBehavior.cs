using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Desynthesizes eligible items looted during dungeon runs.
    /// </summary>
    public class DesynthBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DesynthBehavior"/> class.
        /// </summary>
        public DesynthBehavior(
            ILogger<DesynthBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor) { }

        /// <inheritdoc/>
        public override Task<bool> Run()
        {
            // TODO: Desynth eligible items
            return Task.FromResult(false);
        }
    }
}
