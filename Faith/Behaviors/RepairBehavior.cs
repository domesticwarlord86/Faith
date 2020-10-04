using Faith.Localization;
using ff14bot.Managers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Repairs the current gearset.  Prefers self-repair over NPC menders.
    /// </summary>
    class RepairBehavior : AbstractBehavior
    {
        /// <summary>
        /// Lowest durability allowed before attempting to repair.
        /// </summary>
        private int _minDurability = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepairBehavior"/> class.
        /// </summary>
        public RepairBehavior(ILogger<RepairBehavior> logger) : base(logger) { }

        public override async Task<bool> Run()
        {
            if (ShouldRepair(InventoryManager.EquippedItems))
            {
                _logger.LogInformation(Translations.LOG_REPAIR_LOW_DURABILITY);
            }

            return PASS_EXECUTION;
        }

        /// <summary>
        /// Decides if at least one of the given items needs repairing.
        /// </summary>
        /// <param name="items">Items to check durability of.</param>
        /// <returns><see langword="true"/> if at least one item has low durability.</returns>
        private bool ShouldRepair(IEnumerable<BagSlot> items)
        {
            return items.Any(bagSlot =>
                bagSlot.Item != null
                && bagSlot.Item.RepairClass != 0
                && bagSlot.Condition <= _minDurability
            );
        }
    }
}
