using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Performs transactions with Gil, Tomestones, Grand Company Seals, etc.
    /// </summary>
    public class VendorBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VendorBehavior"/> class.
        /// </summary>
        public VendorBehavior(
            ILogger<VendorBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor) { }

        /// <inheritdoc/>
        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
