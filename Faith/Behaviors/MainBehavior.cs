using Faith.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreeSharp;

namespace Faith.Behaviors
{
    /// <summary>
    /// Entry point for BotBase AI.  Creates and organizes all sub-behaviors.
    /// </summary>
    class MainBehavior : AbstractBehavior
    {
        /// <summary>
        /// Behavior tree <see cref="Composite"/> executed by RebornBuddy.
        /// </summary>
        public Composite Root => new ActionRunCoroutine(x => Run());

        /// <summary>
        /// Sub-behaviors to execute in order, <see cref="PrioritySelector"/> style.
        /// </summary>
        private List<AbstractBehavior> _behaviors;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainBehavior"/> class.
        /// </summary>
        public MainBehavior(
            ILogger<MainBehavior> logger,
            LoadingBehavior loadingBehavior,
            DeathBehavior deathBehavior,
            BossMechanicsBehavior bossMechanicsBehavior,
            CombatBehavior combatBehavior,
            GearsetBehavior gearsetBehavior,
            VendorBehavior vendorBehavior,
            DesynthBehavior desynthBehavior,
            LongTermBuffsBehavior longTermBuffsBehavior,
            TrustQueueBehavior trustQueueBehavior,
            LootingBehavior lootingBehavior,
            DungeonNavigationBehavior dungeonNavigationBehavior,
            DungeonExitBehavior dungeonExitBehavior
        ) : base(logger)
        {
            _behaviors = new List<AbstractBehavior> {
                // Resolve things that lock the character before anything else
                loadingBehavior,
                deathBehavior,

                // Prioritize boss mechanics over rotation
                bossMechanicsBehavior,
                combatBehavior,

                // Non-combat behaviors once out of combat
                gearsetBehavior,
                vendorBehavior,
                desynthBehavior,
                longTermBuffsBehavior,

                // Dungeon logic
                trustQueueBehavior,
                lootingBehavior,
                dungeonNavigationBehavior,
                dungeonExitBehavior,
            };
        }

        public override async Task<bool> Run()
        {
            foreach (AbstractBehavior behavior in _behaviors)
            {
                string behaviorName = behavior.GetType().Name;
                _logger.LogTrace(Translations.LOG_BEHAVIOR_ENTERED, behaviorName);

                if (await behavior.Run())
                {
                    _logger.LogTrace(Translations.LOG_BEHAVIOR_EXITED_HANDLED, behaviorName);
                    return true;
                }
                else
                {
                    _logger.LogTrace(Translations.LOG_BEHAVIOR_EXITED_UNHANDLED, behaviorName);
                }
            }

            return false;
        }
    }
}
