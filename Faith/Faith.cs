using Faith.Logging;
using Faith.Options;
using Faith.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using TreeSharp;
using Action = System.Action;

namespace Faith
{
    /// <summary>
    /// Faith Trust entry point.
    /// </summary>
    public class Faith
    {
        private Composite _root = new TreeSharp.Action();

        /// <summary>
        /// Dependency injection helper.
        /// </summary>
        private IServiceProvider _services;

        private readonly ILogger<Faith> _logger;

        /// <summary>
        /// BotBase settings window.
        /// </summary>
        internal Window SettingsWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="Faith"/> class.
        /// </summary>
        public Faith()
        {
            var extraDlls = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).GetFiles("*.dll");
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                var dll = extraDlls.FirstOrDefault(file => file.Name.Equals(args.Name));
                return dll == null ? null : Assembly.LoadFrom(dll.FullName);
            };

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            // Load config files and build dependency injection container
            IConfiguration config = BuildConfiguration();
            _services = BuildServiceProvider(config);

            // HACK: Manually request a few services -- DON'T DO THIS ELSEWHERE
            // DI normally handles this, but the botbase's top level class is in a weird situation
            _logger = _services.GetService<ILogger<Faith>>();
            SettingsWindow = _services.GetService<BotbaseWindow>();

            _logger.LogInformation("Faith()");
        }

        /// <summary>
        /// Gets the start of the BotBase's behavior tree.
        /// </summary>
        /// <returns></returns>
        public Composite Root => _root;

        /// <summary>
        /// Called when pressing the "Botbase Settings" button in RebornBuddy.
        /// </summary>
        public Action OnButtonPress => new Action(() =>
        {
            _logger.LogInformation("OnButtonPress()");
            SettingsWindow.Show();
        });

        public Action OnInitialize => new Action(() =>
        {
            _logger.LogInformation("OnInitialize()");
        });

        public Action OnStart => new Action(() =>
        {
            _logger.LogInformation("OnStart()");
        });

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
                .SetBasePath(Directory.GetCurrentDirectory())  // TODO: Find RebornBuddy Settings Directory
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
                logging.AddFaithLogger(options =>
                {
                    config.GetSection("Faith:Logger").Bind(options);
                });
            });

            services.AddScoped<BotbaseWindow>();
            services.AddScoped<BotbaseWindowViewModel>();

            services.Configure<FaithOptions>(config.GetSection("Faith"));

            return services.BuildServiceProvider();
        }
    }
}
