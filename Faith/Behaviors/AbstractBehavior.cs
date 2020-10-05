using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Base class for AI behaviors.
    /// </summary>
    public abstract class AbstractBehavior : AbstractLoggableWithOptions
    {
        /// <summary>
        /// Behavior type name, used in debug logging.
        /// </summary>
        public string Name;

        /// <summary>
        /// Return from <see cref="Run"/> if execution was handled by this Behavior and should restart from top.
        /// </summary>
        protected const bool HANDLED_EXECUTION = true;

        /// <summary>
        /// Return from <see cref="Run"/> if execution should pass to next Behavior.
        /// </summary>
        protected const bool PASS_EXECUTION = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractBehavior"/> class.
        /// </summary>
        public AbstractBehavior(
            ILogger logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor)
        {
            Name = GetType().Name;
        }

        /// <summary>
        /// Executes this Behavior's logic.
        /// </summary>
        /// <returns><see langword="true"/> if this Behavior expected/handled execution.<br/>
        /// <see langword="false"/> if execution should move on to next Behavior.</returns>
        public abstract Task<bool> Run();
    }
}
