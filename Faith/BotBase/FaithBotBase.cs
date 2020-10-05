using Faith.Factories;
using Faith.Localization;
using Faith.Logging;
using Faith.Windows;
using ff14bot;
using ff14bot.Navigation;
using ff14bot.Pathing.Service_Navigation;
using Microsoft.Extensions.Logging;
using TreeSharp;

namespace Faith.BotBase
{
    /// <summary>
    /// Faith BotBase entry point.
    /// </summary>
    public class FaithBotBase : AbstractLoggable, IProxiedBotBase
    {
        private readonly BotBaseWindowFactory _botBaseWindowFactory;
        private readonly MainBehaviorFactory _mainBehaviorFactory;

        /// <summary>
        /// The current active instance of the BotBase Settings window.
        /// </summary>
        private BotBaseWindow _botBaseWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="FaithBotBase"/> class.  Called when BotBase is loaded during RebornBuddy startup.
        /// </summary>
        public FaithBotBase(
            ILogger<FaithBotBase> logger,
            BotBaseWindowFactory botBaseWindowFactory,
            MainBehaviorFactory mainBehaviorFactory
        ) : base(logger)
        {
            _botBaseWindowFactory = botBaseWindowFactory;
            _mainBehaviorFactory = mainBehaviorFactory;

            // Initialize LlamaLibrary for game window access
            LlamaLibrary.Memory.OffsetManager.Init();
        }

        /// <inheritdoc/>
        public Composite Root { get; private set; }

        /// <inheritdoc/>
        public void OnButtonPress()
        {
            Logger.LogTrace(Translations.LOG_SETTINGS_CLICKED);

            if (_botBaseWindow == null)
            {
                Logger.LogTrace(Translations.LOG_SETTINGS_NEW_WINDOW);

                // Always create a new window; can't be reused after closing
                _botBaseWindow = _botBaseWindowFactory.Create();
                // Stop tracking window after it closes
                _botBaseWindow.Closed += (sender, e) => _botBaseWindow = null;

                _botBaseWindow.Show();
            }
            else
            {
                Logger.LogTrace(Translations.LOG_SETTINGS_WINDOW_TO_TOP);

                // Window already open; bring to top
                _botBaseWindow.Activate();
            }
        }

        /// <inheritdoc/>
        public void OnStart()
        {
            Logger.LogInformation(Translations.LOG_BOTBASE_STARTED);

            // Pathing
            Navigator.NavigationProvider = new ServiceNavigationProvider();
            Navigator.PlayerMover = new SlideMover();

            // Behaviors
            Root = new PrioritySelector(
                _mainBehaviorFactory.Create().Root,
                new Action(x => TreeRoot.Stop(Translations.LOG_BOTBASE_FINISHED))
            );
        }

        /// <inheritdoc/>
        public void OnStop()
        {
            Logger.LogInformation(Translations.LOG_BOTBASE_STOPPED);
            Root = null;
        }
    }
}
