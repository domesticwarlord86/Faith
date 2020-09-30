using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Defers general combat situations to the active combat routine.
    /// </summary>
    class CombatBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CombatBehavior"/> class.
        /// </summary>
        public CombatBehavior(ILogger<CombatBehavior> logger) : base(logger)
        {
        }

        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
