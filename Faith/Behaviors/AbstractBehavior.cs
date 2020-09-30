using Faith.Logging;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Base class for AI behaviors.
    /// </summary>
    abstract class AbstractBehavior : Loggable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractBehavior"/> class.
        /// </summary>
        public AbstractBehavior(ILogger logger) : base(logger)
        {
        }

        /// <summary>
        /// Executes this Behavior's logic.
        /// </summary>
        /// <returns><see langword="true"/> if this Behavior expected/handled execution.<br/>
        /// <see langword="false"/> if execution should move on to next Behavior.</returns>
        public abstract Task<bool> Run();
    }
}
