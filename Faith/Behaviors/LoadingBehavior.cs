using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Waits for loading screens, cutscenes, dungeon barriers, etc.
    /// </summary>
    class LoadingBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingBehavior"/> class.
        /// </summary>
        public LoadingBehavior(ILogger<LoadingBehavior> logger) : base(logger)
        {
        }

        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
