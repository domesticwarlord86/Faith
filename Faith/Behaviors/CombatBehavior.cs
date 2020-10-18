using Faith.Helpers;
using Faith.Localization;
using Faith.Options;
using ff14bot;
using ff14bot.Behavior;
using ff14bot.Helpers;
using ff14bot.Managers;
using ff14bot.Objects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TreeSharp;

namespace Faith.Behaviors
{
    /// <summary>
    /// Defers general combat situations to the active combat routine.
    /// </summary>
    public class CombatBehavior : AbstractBehavior
    {
        private readonly object context = new object();

        private readonly Composite _rest;
        private readonly Composite _heal;
        private readonly Composite _preCombatBuff;
        private readonly Composite _preCombatLogic;
        private readonly Composite _pull;
        private readonly Composite _combatBuff;
        private readonly Composite _combat;

        /// <summary>
        /// Initializes a new instance of the <see cref="CombatBehavior"/> class.
        /// </summary>
        public CombatBehavior(
            ILogger<CombatBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor)
        {
            _rest = new HookExecutor("Rest", null, RoutineManager.Current.RestBehavior ?? new ActionAlwaysFail());
            _heal = new HookExecutor("Heal", null, RoutineManager.Current.HealBehavior ?? new ActionAlwaysFail());
            _preCombatBuff = new HookExecutor("PreCombatBuff", null, RoutineManager.Current.PreCombatBuffBehavior ?? new ActionAlwaysFail());
            _preCombatLogic = new HookExecutor("PreCombatLogic", null, new ActionAlwaysFail());
            _pull = new HookExecutor("Pull", null, RoutineManager.Current.PullBehavior ?? new ActionAlwaysFail());
            _combatBuff = new HookExecutor("CombatBuff", null, RoutineManager.Current.CombatBuffBehavior ?? new ActionAlwaysFail());
            _combat = new HookExecutor("Combat", null, RoutineManager.Current.CombatBehavior ?? new ActionAlwaysFail());
        }

        /// <inheritdoc/>
        public override async Task<bool> Run()
        {
            // Always dodge above all else
            if (AvoidanceManager.IsRunningOutOfAvoid)
            {
                Logger.LogTrace(Translations.LOG_COMBAT_AVOIDING_AOE);

                return HANDLED_EXECUTION;
            }

            // Non-combat preparations
            if (!Core.Player.InCombat)
            {
                if (await Rest())
                {
                    Logger.LogInformation(Translations.LOG_COMBAT_RESTING);
                    await CommonTasks.StopMoving();
                    await Heal();

                    return HANDLED_EXECUTION;
                }

                if (await PreCombatBuff())
                {
                    return HANDLED_EXECUTION;
                }
            }

            // Target management
            CombatTargeting.Instance.Pulse();  // TODO: Add reduced tick rate for pulse?
            BattleCharacter nextCombatTarget = CombatTargeting.Instance.FirstUnit;
            if (nextCombatTarget != null && (Poi.Current?.Unit == null || Poi.Current.Unit.Pointer != nextCombatTarget.Pointer))
            {
                Poi.Current = new Poi(nextCombatTarget, PoiType.Kill);
            }

            if (Poi.Current?.BattleCharacter == null || Poi.Current.Type != PoiType.Kill)
            {
                // No combat target to fight
                return PASS_EXECUTION;
            }

            if (Poi.Current?.BattleCharacter == null || !Poi.Current.BattleCharacter.IsValid || !Poi.Current.BattleCharacter.IsAlive)
            {
                Logger.LogInformation(Translations.LOG_COMBAT_TARGET_DIED);
                Poi.Clear(Translations.LOG_COMBAT_TARGET_DIED);

                return HANDLED_EXECUTION;
            }

            // Start combat
            StatusBar.Text = string.Format(Translations.STATUS_COMBAT_TARGET, Poi.Current.Name);

            if (Poi.Current.Unit.IsTargetable && Core.Player.PrimaryTargetPtr != Poi.Current.Unit.Pointer)
            {
                Poi.Current.Unit.Target();

                return HANDLED_EXECUTION;
            }

            if (await PreCombatLogic())
            {
                return HANDLED_EXECUTION;
            }

            if (!Core.Player.InCombat)
            {
                await Pull();

                return HANDLED_EXECUTION;
            }
            else
            {
                if (await Heal())
                {
                    return HANDLED_EXECUTION;
                }

                if (await CombatBuff())
                {
                    return HANDLED_EXECUTION;
                }

                await Combat();

                return HANDLED_EXECUTION;
            }
        }

        private async Task<bool> Rest()
        {
            Logger.LogTrace(Translations.LOG_COMBAT_INTERNAL_CALL, nameof(Rest));

            return await _rest.ExecuteCoroutine(context);
        }

        private async Task<bool> Heal()
        {
            Logger.LogTrace(Translations.LOG_COMBAT_INTERNAL_CALL, nameof(Heal));

            return await _heal.ExecuteCoroutine(context);
        }

        private async Task<bool> PreCombatBuff()
        {
            Logger.LogTrace(Translations.LOG_COMBAT_INTERNAL_CALL, nameof(PreCombatBuff));

            return await _preCombatBuff.ExecuteCoroutine(context);
        }

        private async Task<bool> PreCombatLogic()
        {
            Logger.LogTrace(Translations.LOG_COMBAT_INTERNAL_CALL, nameof(PreCombatLogic));

            return await _preCombatLogic.ExecuteCoroutine(context);
        }

        private async Task<bool> Pull()
        {
            Logger.LogTrace(Translations.LOG_COMBAT_INTERNAL_CALL, nameof(Pull));

            return await _pull.ExecuteCoroutine(context);
        }

        private async Task<bool> CombatBuff()
        {
            Logger.LogTrace(Translations.LOG_COMBAT_INTERNAL_CALL, nameof(CombatBuff));

            return await _combatBuff.ExecuteCoroutine(context);
        }

        private async Task<bool> Combat()
        {
            Logger.LogTrace(Translations.LOG_COMBAT_INTERNAL_CALL, nameof(Combat));

            return await _combat.ExecuteCoroutine(context);
        }
    }
}
