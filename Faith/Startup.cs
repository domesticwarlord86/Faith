using Faith.Behaviors;
using Faith.BotBase;
using Faith.Factories;
using Faith.Localization;
using Faith.Logging;
using Faith.Options;
using Faith.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using TreeSharp;
using Action = System.Action;

namespace Faith
{
    /// <summary>
    /// Sets up the BotBase and forwards method calls to <see cref="IProxiedBotBase"/> instance.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Dependency injection service provider.
        /// </summary>
        private readonly IServiceProvider _services;

        private readonly LocalizationProvider _localizationProvider;

        /// <summary>
        /// <see cref="IProxiedBotBase"/> that BotBase method calls are forwarded to.
        /// </summary>
        private readonly IProxiedBotBase _proxiedBotBase;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        public Startup()
        {
            // Load config files and build dependency injection container
            IConfiguration config = BuildConfiguration();
            _services = BuildServiceProvider(config);

            _localizationProvider = _services.GetService<LocalizationProvider>();

            _proxiedBotBase = _services.GetService<IProxiedBotBase>();
        }

        /// <inheritdoc/>
        public Func<Composite> Root => () => _proxiedBotBase.Root;

        /// <inheritdoc/>
        public Action OnButtonPress => new Action(() => _proxiedBotBase.OnButtonPress());

        /// <inheritdoc/>
        public Action OnStart => new Action(() => _proxiedBotBase.OnStart());

        /// <inheritdoc/>
        public Action OnStop => new Action(() => _proxiedBotBase.OnStop());

        /// <summary>
        /// Inflates configuration object from configuration sources.
        /// </summary>
        /// <returns>Configuration</returns>
        private IConfiguration BuildConfiguration()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Settings"))
                .AddJsonFile("FaithSettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("FaithSettings.Development.json", optional: true, reloadOnChange: true);

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

            // BotBase
            services.AddScoped<IProxiedBotBase, FaithBotBase>();

            // Behaviors
            services.AddScoped<MainBehavior>();
            services.AddScoped<LoadingBehavior>();
            services.AddScoped<DeathBehavior>();
            services.AddScoped<BossMechanicsBehavior>();
            services.AddScoped<CombatBehavior>();
            services.AddScoped<GearsetBehavior>();
            services.AddScoped<RepairBehavior>();
            services.AddScoped<VendorBehavior>();
            services.AddScoped<DesynthBehavior>();
            services.AddScoped<LongTermBuffsBehavior>();
            services.AddScoped<TrustQueueBehavior>();
            services.AddScoped<LootingBehavior>();
            services.AddScoped<DungeonNavigationBehavior>();
            services.AddScoped<DungeonExitBehavior>();
            services.AddScoped<DebugBehavior>();

            // Windows
            services.AddTransient<BotBaseWindow>();
            services.AddTransient<BotBaseWindowViewModel>();

            // Factories
            services.AddScoped<BotBaseWindowFactory>();
            services.AddScoped<MainBehaviorFactory>();

            // Localization
            services.AddSingleton<LocalizationProvider>();

            return services.BuildServiceProvider();
        }
    }
}
