using Buddy.Coroutines;
using Faith.Helpers;
using Faith.Localization;
using Faith.Options;
using LlamaLibrary.RemoteAgents;
using LlamaLibrary.RemoteWindows;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Equips the best gear available.
    /// </summary>
    public class GearsetBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GearsetBehavior"/> class.
        /// </summary>
        public GearsetBehavior(
            ILogger<GearsetBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor)
        {
            Cooldown = TimeSpan.FromMinutes(10);
        }

        /// <inheritdoc/>
        public override async Task<bool> Run()
        {
            if (ShouldTryEquip())
            {
                await EquipRecommendedGear();
                Stopwatch.Restart();

                return HANDLED_EXECUTION;
            }

            return PASS_EXECUTION;
        }

        /// <summary>
        /// Determines if the bot should try equipping new gear.
        /// </summary>
        private bool ShouldTryEquip()
        {
            return !CurrentInstance.IsInInstance && IsBehaviorReady();
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
            }

            RecommendEquip.Instance.Confirm();
            Logger.LogInformation(Translations.LOG_GEARSET_EQUIPPED_RECOMMENDED);
        }
    }
}
