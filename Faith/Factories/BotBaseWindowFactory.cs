using Faith.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Faith.Factories
{
    /// <summary>
    /// Factory for <see cref="BotBaseWindow"/>.
    /// </summary>
    public class BotBaseWindowFactory : IFactory<BotBaseWindow>
    {
        private readonly IServiceProvider _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="BotBaseWindowFactory"/> class.
        /// </summary>
        /// <param name="services">Dependency injection container.</param>
        public BotBaseWindowFactory(IServiceProvider services)
        {
            _services = services;
        }

        /// <summary>
        /// Creates a new instance of <see cref="BotBaseWindow"/>.
        /// </summary>
        public BotBaseWindow Create()
        {
            return _services.GetRequiredService<BotBaseWindow>();
        }
    }
}
