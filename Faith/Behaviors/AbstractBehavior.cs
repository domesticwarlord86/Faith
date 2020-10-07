using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
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
        public string Name { get; protected set; }

        /// <summary>
        /// Controls whether the behavior runs at all.
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// (Optional) Tracks time elapsed since last full run of the behavior.  Used with <seealso cref="Cooldown"/> to rate-limit behaviors.
        /// </summary>
        protected readonly Stopwatch Stopwatch = new Stopwatch();

        /// <summary>
        /// (Optional) How often the behavior should fully run.  Used with <seealso cref="Stopwatch"/> to rate-limit behaviors.
        /// </summary>
        protected TimeSpan Cooldown = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Return from <see cref="Run"/> if execution was handled by this behavior and should restart from top.
        /// </summary>
        protected const bool HANDLED_EXECUTION = true;

        /// <summary>
        /// Return from <see cref="Run"/> if execution should pass to next behavior.
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
        /// <returns><see langword="true"/> if this behavior expected/handled execution.<br/>
        /// <see langword="false"/> if execution should move on to next behavior.</returns>
        public abstract Task<bool> Run();

        /// <summary>
        /// Checks if the behavior is off cooldown and ready to fully execute.
        /// </summary>
        /// <returns><see langword="true"/> if cooldown not in use, or cooldown has ended.</returns>
        protected bool IsBehaviorReady()
        {
            return !Stopwatch.IsRunning || Stopwatch.Elapsed > Cooldown;
        }
    }
}
