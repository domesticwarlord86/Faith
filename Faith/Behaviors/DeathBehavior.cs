using Buddy.Coroutines;
using Faith.Helpers;
using Faith.Localization;
using Faith.Options;
using ff14bot;
using ff14bot.RemoteWindows;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Handles dying (release, accept raise).
    /// </summary>
    public class DeathBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractBehavior"/> class.
        /// </summary>
        public DeathBehavior(
            ILogger<DeathBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor) { }

        /// <inheritdoc/>
        public override async Task<bool> Run()
        {
            // Open and accept revive notification
            if (NotificationRevive.IsOpen)
            {
                Logger.LogInformation(Translations.LOG_REVIVE_OPENED);
                NotificationRevive.Click();

                await Coroutine.Wait(250, () => SelectYesno.IsOpen);
                if (SelectYesno.IsOpen)
                {
                    Logger.LogInformation(Translations.LOG_REVIVE_ACCEPTED);
                    SelectYesno.ClickYes();
                }

                return HANDLED_EXECUTION;
            }

            // No opportunity to revive yet?
            if (Core.Player.IsDead || Core.Player.IsDying)
            {
                StatusBar.Text = Translations.STATUS_DEAD_WAITING;
                await Coroutine.Sleep(250);

                return HANDLED_EXECUTION;
            }

            return PASS_EXECUTION;
        }
    }
}
