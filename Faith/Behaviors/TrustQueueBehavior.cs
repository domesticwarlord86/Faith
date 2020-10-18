using Buddy.Coroutines;
using Faith.Helpers;
using Faith.Localization;
using Faith.Options;
using ff14bot;
using ff14bot.Behavior;
using ff14bot.RemoteWindows;
using LlamaLibrary.RemoteAgents;
using LlamaLibrary.RemoteWindows;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Faith.Behaviors
{
    /// <summary>
    /// Configures Trust Party and queues for Trust Dungeons.
    /// </summary>
    public class TrustQueueBehavior : AbstractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrustQueueBehavior"/> class.
        /// </summary>
        public TrustQueueBehavior(
            ILogger<TrustQueueBehavior> logger,
            IOptionsMonitor<FaithOptions> faithOptionsMonitor
        ) : base(logger, faithOptionsMonitor) { }

        /// <inheritdoc/>
        public override async Task<bool> Run()
        {
            if (CurrentInstance.IsInInstance) { return PASS_EXECUTION; }

            if (!Dawn.Instance.IsOpen)
            {
                AgentDawn.Instance.Toggle();
                await Coroutine.Wait(1000, () => Dawn.Instance.IsOpen);
            }

            ConfigureTrust();
            await EnterTrust();

            return HANDLED_EXECUTION;
        }

        /// <summary>
        /// Selects the preferred Trust dungeon and party members.
        /// </summary>
        private void ConfigureTrust()
        {
            // For prototyping, let's just roll with player's existing selection
            // TODO: Set Trust Dungeon ID
            // TODO: Set Party Members
        }

        /// <summary>
        /// Queues for and accepts previously configured Trust dungeon.
        /// </summary>
        private async Task EnterTrust()
        {

            Logger.LogInformation(Translations.LOG_QUEUE_REGISTERING, GetTrustInfo());
            Dawn.Instance.Register();
            await Coroutine.Wait(5000, () => ContentsFinderConfirm.IsOpen);

            Logger.LogInformation(Translations.LOG_QUEUE_ACCEPTING);
            ContentsFinderConfirm.Commence();
            await Coroutine.Wait(60000, () => CommonBehaviors.IsLoading);
        }

        /// <summary>
        /// Prettifies selected Trust dungeon + team information.
        /// </summary>
        private string GetTrustInfo()
        {
            string[] team = new string[] {
                Core.Player.CurrentJob.ToString(),
                Dawn.Instance.SelectedNpc1.Name,
                Dawn.Instance.SelectedNpc2.Name,
                Dawn.Instance.SelectedNpc3.Name,
            };

            return $"({Dawn.Instance.SelectedTrustId}) {Dawn.Instance.SelectedTrustName} [{string.Join(", ", team)}]";
        }
    }
}
