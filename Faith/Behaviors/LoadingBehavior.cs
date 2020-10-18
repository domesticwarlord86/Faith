using Buddy.Coroutines;
using Faith.Helpers;
using Faith.Localization;
using Faith.Options;
using ff14bot.Behavior;
using ff14bot.Managers;
using ff14bot.RemoteAgents;
using ff14bot.RemoteWindows;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Waits for loading screens, cutscenes, dungeon barriers, etc.
    /// </summary>
    public class LoadingBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingBehavior"/> class.
        /// </summary>
        public LoadingBehavior(
            ILogger<LoadingBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor) { }

        /// <inheritdoc/>
        public override async Task<bool> Run()
        {
            if (CommonBehaviors.IsLoading)
            {
                StatusBar.Text = Translations.STATUS_LOADING_WAIT;
                Logger.LogInformation(Translations.STATUS_LOADING_WAIT);

                await Coroutine.Wait(Timeout.Infinite, () => !CommonBehaviors.IsLoading);

                return HANDLED_EXECUTION;
            }

            if (QuestLogManager.InCutscene)
            {
                StatusBar.Text = Translations.STATUS_CUTSCENE_WAIT;
                Logger.LogInformation(Translations.STATUS_CUTSCENE_WAIT);

                AgentCutScene cutscene = AgentCutScene.Instance;
                if (cutscene != null && cutscene.CanSkip)
                {
                    cutscene.PromptSkip();
                    await Coroutine.Wait(500, () => SelectString.IsOpen);
                    if (SelectString.IsOpen)
                    {
                        Logger.LogInformation(Translations.LOG_CUTSCENE_SKIPPING);

                        SelectString.ClickSlot(0);
                    }

                    return HANDLED_EXECUTION;
                }
            }

            if (CurrentInstance.IsInInstance)
            {
                if (!CurrentInstance.IsDutyCommenced)
                {
                    StatusBar.Text = Translations.STATUS_DUTY_WAIT_COMMENCED;
                    Logger.LogInformation(Translations.STATUS_DUTY_WAIT_COMMENCED);

                    await Coroutine.Wait(TimeSpan.FromMinutes(2), () => CurrentInstance.IsDutyCommenced);
                    Logger.LogInformation(Translations.LOG_DUTY_COMMENCED, CurrentInstance.Id, CurrentInstance.Name);

                    return HANDLED_EXECUTION;
                }
            }

            return PASS_EXECUTION;
        }
    }
}
