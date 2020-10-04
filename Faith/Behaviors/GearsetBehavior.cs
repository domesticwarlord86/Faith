using Buddy.Coroutines;
using Faith.Helpers;
using Faith.Localization;
using LlamaLibrary.RemoteAgents;
using LlamaLibrary.RemoteWindows;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Equips the best gear available.
    /// </summary>
    class GearsetBehavior : AbstractBehavior
    {
        /// <summary>
        /// Timestamp of last Equip Recommended attempt.
        /// </summary>
        private DateTime lastEquipAttempt = DateTime.MinValue;

        /// <summary>
        /// How often the Equip Recommended can be run.
        /// </summary>
        private TimeSpan equipCooldown = TimeSpan.FromMinutes(15);

        /// <summary>
        /// Initializes a new instance of the <see cref="GearsetBehavior"/> class.
        /// </summary>
        public GearsetBehavior(ILogger<GearsetBehavior> logger) : base(logger) { }

        public override async Task<bool> Run()
        {
            if (ShouldTryEquip())
            {
                await EquipRecommendedGear();

                return HANDLED_EXECUTION;
            }

            return PASS_EXECUTION;
        }

        /// <summary>
        /// Determines if the bot should try equipping new gear.
        /// </summary>
        private bool ShouldTryEquip()
        {
            return !CurrentInstance.IsInInstance && (DateTime.Now - lastEquipAttempt) > equipCooldown;
        }

        /// <summary>
        /// Triggers the in-game Equip Recommended Gear feature.
        /// </summary>
        private async Task EquipRecommendedGear()
        {
            if (!RecommendEquip.Instance.IsOpen)
            {
                AgentRecommendEquip.Instance.Toggle();
                await Coroutine.Wait(500, () => RecommendEquip.Instance.IsOpen);

                RecommendEquip.Instance.Confirm();
                lastEquipAttempt = DateTime.Now;
                _logger.LogInformation(Translations.LOG_GEARSET_EQUIPPED_RECOMMENDED);
            }
        }
    }
}
