using Faith.Logging;
using Faith.Options;
using Faith.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using TreeSharp;
using Action = System.Action;

namespace Faith
{
    /// <summary>
    /// Faith BotBase entry point.
    /// </summary>
    public class Faith
    {
        private Composite _root = new TreeSharp.Action();

        /// <summary>
        /// Dependency injection helper.
        /// </summary>
        private IServiceProvider _services;
        private BotbaseWindow _botbaseWindow;
        private readonly ILogger<Faith> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Faith"/> class.  Called when BotBase is loaded during RebornBuddy startup.
        /// </summary>
        public Faith()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            // Load config files and build dependency injection container
            IConfiguration config = BuildConfiguration();
            _services = BuildServiceProvider(config);

            // HACK: Manually request a few services -- DON'T DO THIS IN OTHER CLASSES
            // DI normally handles this, but the botbase's top level class is in a weird situation
            _logger = _services.GetService<ILogger<Faith>>();
        }

        /// <summary>
        /// Gets the start of the BotBase's behavior tree.
        /// </summary>
        public Composite Root => _root;

        /// <summary>
        /// Called when pressing the "Botbase Settings" button in RebornBuddy.
        /// </summary>
        public Action OnButtonPress => new Action(() =>
        {
            if (_botbaseWindow == null)
            {
                // Always create a new window; can't be reused after closing
                _botbaseWindow = _services.GetService<BotbaseWindow>();
                // Stop tracking window after it closes
                _botbaseWindow.Closed += (sender, e) => _botbaseWindow = null;

                _botbaseWindow.Show();
            }
            else
            {
                // Window already open; bring to top
                _botbaseWindow.Activate();
            }
        });

        public Action OnInitialize => new Action(() =>
        {
            _logger.LogInformation("OnInitialize()");
        });

        /// <summary>
        /// Called when pressing the "Start" button in RebornBuddy.
        /// </summary>
        public Action OnStart => new Action(() =>
        {
            _logger.LogInformation("OnStart()");
        });

        /// <summary>
        /// Called when pressing the "Stop" button in RebornBuddy.
        /// </summary>
        public Action OnStop => new Action(() =>
        {
            _logger.LogInformation("OnStop()");
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
            services.AddLogging(logging =>
            {
                logging.AddConfiguration(config.GetSection("Logging"));
                logging.AddFaithLogger(options => config.GetSection("Faith:Logger").Bind(options));
            });

            services.AddTransient<BotbaseWindow>();
            services.AddTransient<BotbaseWindowViewModel>();

            services.Configure<FaithOptions>(config.GetSection("Faith"));

            return services.BuildServiceProvider();
        }
    }
}
