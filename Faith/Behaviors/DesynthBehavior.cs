using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Desynthesizes eligible items looted during dungeon runs.
    /// </summary>
    class DesynthBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DesynthBehavior"/> class.
        /// </summary>
        public DesynthBehavior(ILogger<DesynthBehavior> logger) : base(logger)
        {
        }

        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
