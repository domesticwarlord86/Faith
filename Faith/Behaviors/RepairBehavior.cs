using Buddy.Coroutines;
using Faith.Localization;
using Faith.Options;
using ff14bot;
using ff14bot.Enums;
using ff14bot.Managers;
using ff14bot.RemoteWindows;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Repairs the current gearset.  Prefers self-repair over NPC menders.
    /// </summary>
    public class RepairBehavior : AbstractBehavior
    {
        /// <summary>
        /// How many crafter class levels the player can be BELOW the item's equip level to repair.
        /// </summary>
        private const int _repairLevelRange = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepairBehavior"/> class.
        /// </summary>
        public RepairBehavior(
            ILogger<RepairBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor)
        {
            Cooldown = TimeSpan.FromMinutes(5);
        }

        /// <inheritdoc/>
        public override async Task<bool> Run()
        {
            if (!IsBehaviorReady()) { return PASS_EXECUTION; }

            foreach (BagSlot slot in InventoryManager.EquippedItems.Where(s => s.IsFilled))
            {
                if (ShouldRepair(slot))
                {
                    Logger.LogInformation(Translations.LOG_REPAIR_LOW_DURABILITY);


                    if (CanSelfRepair(slot))
                    {
                        await DoSelfRepair();
                    }
                    else if (CanNpcMenderRepair())
                    {
                        await DoNpcMenderRepair();
                    }
                }
            }

            Stopwatch.Restart();

            return PASS_EXECUTION;
        }

        /// <summary>
        /// Checks if an item needs to be repaired.
        /// </summary>
        /// <param name="slot">Item to check durability of.</param>
        /// <returns><see langword="true"/> if the item has low durability.</returns>
        private bool ShouldRepair(BagSlot slot)
        {
            Logger.LogTrace($"{slot.Item.RepairClass} != {0} && {slot.Condition} <= {FaithOptions.RepairDurabilityThreshold}");
            return slot.Item.RepairClass != 0 && slot.Condition <= FaithOptions.RepairDurabilityThreshold;
        }

        /// <summary>
        /// Checks if an item can be self-repaired.
        /// </summary>
        /// <param name="slot">Item to check the self-repairability of.</param>
        /// <returns><see langword="true"/> if the item can be self repaired.</returns>
        private bool CanSelfRepair(BagSlot slot)
        {
            ClassJobType repairClass = (ClassJobType)slot.Item.RepairClass;
            ushort classLevel = Core.Player.Levels[repairClass];
            byte equipLevel = slot.Item.RequiredLevel;
            Logger.LogTrace(Translations.LOG_REPAIR_LEVELS, slot.Item.CurrentLocaleName, equipLevel, repairClass, classLevel);

            return (equipLevel - classLevel) <= _repairLevelRange;
        }

        /// <summary>
        /// Checks that the player can access an NPC mender.
        /// </summary>
        /// <returns><see langword="true"/> if able to access an NPC mender.</returns>
        private bool CanNpcMenderRepair()
        {
            return false;  // TODO: Make sure not in instance and know where menders are
        }

        /// <summary>
        /// Checks that the player has enough repair materials for the given items.
        /// </summary>
        /// <param name="slots">Repairable items to check materials for.</param>
        /// <returns></returns>
        private bool HaveRepairMaterials(IEnumerable<BagSlot> slots)
        {
            // Find strongest repair material required.  This will "waste" strong materials on low level items,
            // but who wastes inventory carrying multiple grades over one 999 stack of the best?
            uint strongestMaterialId = slots.Max(s => s.Item.RepairItemId);  // Assuming higher item ID = better material will probably explode one day
            int damagedItemCount = slots.Count(s => s.Condition < 100); // "Repair All" only cares about < 100%, not our minimum

            // Each slot takes one unit of repair material, no matter how badly damaged
            bool hasEnough = slots.Any(s => s.Item.Id == strongestMaterialId && s.Count >= damagedItemCount);

            if (Logger.IsEnabled(LogLevel.Trace))
            {
                var repairMaterial = DataManager.GetItem(strongestMaterialId);
                Logger.LogTrace(Translations.LOG_REPAIR_MATERIAL_COST, damagedItemCount, repairMaterial.CurrentLocaleName, hasEnough);
            }

            return hasEnough;
        }

        /// <summary>
        /// Repairs current gearset via self-repair.
        /// </summary>
        private async Task DoSelfRepair()
        {
            Logger.LogInformation(Translations.LOG_REPAIR_SELF);

            if (!HaveRepairMaterials(InventoryManager.EquippedItems.Where(s => s.IsFilled)))
            {
                // Buy from NPC, or fail if unable (can't travel, dungeon, low gil, etc)
            }

            if (!Repair.IsOpen)
            {
                ActionManager.ToggleRepairWindow();
                await Coroutine.Sleep(250);
            }

            // Only clicks the button, Yes/No is separate
            Repair.RepairAll();

            await Coroutine.Wait(1000, () => SelectYesno.IsOpen);
            SelectYesno.ClickYes();

            await Coroutine.Sleep(2500);
            Repair.Close();
        }

        /// <summary>
        /// Repairs current gearset via NPC mender.
        /// </summary>
        private Task DoNpcMenderRepair()
        {
            Logger.LogInformation(Translations.LOG_REPAIR_NPC_MENDER);
            // Get to NPC mender
            // Activate NPC mender window
            return Task.CompletedTask;
        }
    }
}
