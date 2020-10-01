using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Equips the best gear available and repairs as needed.
    /// </summary>
    class GearsetBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GearsetBehavior"/> class.
        /// </summary>
        public GearsetBehavior(ILogger<GearsetBehavior> logger) : base(logger)
        {
        }

        public override Task<bool> Run()
        {
            return Task.FromResult(true);
        }
    }
}
