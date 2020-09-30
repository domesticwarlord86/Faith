using Faith.Localization;
using Faith.Logging;
using Faith.Options;
using Faith.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using TreeSharp;
using Action = System.Action;

namespace Faith
{
    /// <summary>
    /// Faith BotBase entry point.
    /// </summary>
    public class Faith
    {
        /// <summary>
        /// Start of the BotBase's behavior tree.
        /// </summary>
        private Composite _root;

        /// <summary>
        /// Dependency injection helper.
        /// </summary>
        private readonly IServiceProvider _services;

        /// <summary>
        /// The current active instance of the BotBase Settings window.
        /// </summary>
        private BotbaseWindow _botbaseWindow;

        private readonly ILogger<Faith> _logger;

        private readonly IOptionsMonitor<FaithOptions> _faithOptionsMonitor;
        private FaithOptions FaithOptions => _faithOptionsMonitor.CurrentValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Faith"/> class.  Called when BotBase is loaded during RebornBuddy startup.
        /// </summary>
        public Faith()
        {
            // Load config files and build dependency injection container
            IConfiguration config = BuildConfiguration();
            _services = BuildServiceProvider(config);

            // HACK: Manually request a few services -- DON'T DO THIS IN OTHER CLASSES
            // DI normally handles this, but the botbase's top level class is in a weird situation
            _logger = _services.GetService<ILogger<Faith>>();
            _faithOptionsMonitor = _services.GetService<IOptionsMonitor<FaithOptions>>();
        }

        /// <summary>
        /// Gets the start of the BotBase's behavior tree.
        /// </summary>
        public Func<Composite> Root => () => _root;

        /// <summary>
        /// Called when pressing the "Botbase Settings" button in RebornBuddy.
        /// </summary>
        public Action OnButtonPress => new Action(() =>
        {
            _logger.LogTrace(Translations.LOG_SETTINGS_CLICKED);

            if (_botbaseWindow == null)
            {
                _logger.LogTrace(Translations.LOG_SETTINGS_NEW_WINDOW);

                // Always create a new window; can't be reused after closing
                _botbaseWindow = _services.GetService<BotbaseWindow>();
                // Stop tracking window after it closes
                _botbaseWindow.Closed += (sender, e) => _botbaseWindow = null;

                _botbaseWindow.Show();
            }
            else
            {
                _logger.LogTrace(Translations.LOG_SETTINGS_WINDOW_TO_TOP);

                // Window already open; bring to top
                _botbaseWindow.Activate();
            }
        });

        /// <summary>
        /// Called when pressing the "Start" button in RebornBuddy.
        /// </summary>
        public Action OnStart => new Action(() =>
        {
            _logger.LogInformation(Translations.LOG_BOTBASE_STARTED);
            _root = new PrioritySelector();
        });

        /// <summary>
        /// Called when pressing the "Stop" button in RebornBuddy.
        /// </summary>
        public Action OnStop => new Action(() =>
        {
            _logger.LogInformation(Translations.LOG_BOTBASE_STOPPED);
            _root = null;
        });

        /// <summary>
        /// Inflates configuration object from configuration sources.
        /// </summary>
        /// <returns>Configuration</returns>
        private IConfiguration BuildConfiguration()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Settings"))
                .AddJsonFile("Faith.json", optional: true, reloadOnChange: false)
                .AddJsonFile("Faith.Development.json", optional: true, reloadOnChange: false);

            return configBuilder.Build();
        }

        /// <summary>
        /// Configures and builds dependency injection service provider.
        /// </summary>
        /// <param name="config">Application configuration</param>
        /// <returns>Service provider</returns>
        private IServiceProvider BuildServiceProvider(IConfiguration config)
        {
            IServiceCollection services = new ServiceCollection();

            // Logging
            services.AddLogging(logging =>
            {
                logging.AddConfiguration(config.GetSection("Logging"));
                logging.AddFaithLogger();
            });

            // Config Files
            services.Configure<FaithOptions>(config.GetSection("Faith"));

            // Behaviors

            // Windows
            services.AddTransient<BotbaseWindow>();
            services.AddTransient<BotbaseWindowViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
