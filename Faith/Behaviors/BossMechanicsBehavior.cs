using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Performs special mechanics required by bosses.
    /// </summary>
    class BossMechanicsBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BossMechanicsBehavior"/> class.
        /// </summary>
        public BossMechanicsBehavior(ILogger<BossMechanicsBehavior> logger) : base(logger)
        {
        }

        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
