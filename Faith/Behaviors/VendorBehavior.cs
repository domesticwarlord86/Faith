using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Performs transactions with Gil, Tomestones, Grand Company Seals, etc.
    /// </summary>
    class VendorBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VendorBehavior"/> class.
        /// </summary>
        public VendorBehavior(ILogger<VendorBehavior> logger) : base(logger)
        {
        }

        public override Task<bool> Run()
        {
            return Task.FromResult(false);
        }
    }
}
