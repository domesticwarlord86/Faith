using Faith.Options;
using ff14bot;
using ff14bot.Managers;
using ff14bot.Objects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Manages long term buffs, including Food, Medicine, Squadron Manuals, Free Company Actions, etc.
    /// </summary>
    public class LongTermBuffsBehavior : AbstractBehavior
    {
        /// <summary>
        /// Minimum item buff duration allowed before refresh attempt.
        /// </summary>
        private readonly TimeSpan _minItemBuffDuration = TimeSpan.FromMinutes(2);

        /// <summary>
        /// Food to keep active.
        /// </summary>
        private Item _food;

        /// <summary>
        /// Medicine to keep active (Spiritbond, Durability Draught, etc).
        /// </summary>
        private Item _medicine;

        /// <summary>
        /// Squadron Manual to keep active (effectively self-FC buffs).
        /// </summary>
        private Item _squadronManual;

        /// <summary>
        /// Free Company Actions to keep active.
        /// </summary>
        private Item _companyAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="LongTermBuffsBehavior"/> class.
        /// </summary>
        public LongTermBuffsBehavior(
            ILogger<LongTermBuffsBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor)
        {
            UpdateTargets(FaithOptions);
            faithOptionsMonitor.OnChange((options) => UpdateTargets(options));

            IsEnabled = false; // TODO: Finish Behavior
        }

        /// <summary>
        /// Updates target buff sources from options.
        /// </summary>
        private void UpdateTargets(FaithOptions options)
        {
            _food = DataManager.GetItem(options.FoodId);
            _medicine = DataManager.GetItem(options.MedicineId);
            _squadronManual = DataManager.GetItem(options.SquadronManualId);
            _companyAction = null;
        }

        /// <inheritdoc/>
        public override async Task<bool> Run()
        {
            if (!IsBehaviorReady()) { return PASS_EXECUTION; }

            TryRefreshItemBuff(_food);
            TryRefreshItemBuff(_medicine);
            TryRefreshItemBuff(_squadronManual);
            TryRefreshCompanyActionBuff();

            Stopwatch.Restart();

            return PASS_EXECUTION;
        }

        /// <summary>
        /// Tries to refresh the buff resulting from using an <see cref="Item"/>.
        /// </summary>
        /// <param name="item"><see cref="Item"/> to be used.</param>
        /// <returns><see langword="true"/> if the buff was refreshed.</returns>
        private bool TryRefreshItemBuff(Item item)
        {
            // TODO: Find Aura ID from Item.ItemAction -> ItemFood; ItemAction not inflated in RB
            uint auraId = 0;
            Aura aura = Core.Player.GetAuraById(auraId);
            if (item == null || aura == null) { return false; }

            if (aura.TimespanLeft > _minItemBuffDuration) { return false; }

            BagSlot slot = InventoryManager.FilledSlots.FirstOrDefault(s => s.IsFilled && s.TrueItemId == item.Id);

            if (slot == null || !slot.CanUse()) { return false; }

            slot.UseItem();

            return true;
        }

        /// <summary>
        /// Refreshes Company Action buff.
        /// </summary>
        /// <returns><see langword="true"/> if Company Action was used.</returns>
        private bool TryRefreshCompanyActionBuff()
        {
            // TODO: Add FC buff support
            return false;
        }
    }
}
