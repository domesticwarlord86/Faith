using Faith.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Faith.Factories
{
    /// <summary>
    /// Factory for <see cref="MainBehavior"/>.
    /// </summary>
    public class MainBehaviorFactory : IFactory<MainBehavior>
    {
        private readonly IServiceProvider _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainBehaviorFactory"/> class.
        /// </summary>
        /// <param name="services">Dependency injection container.</param>
        public MainBehaviorFactory(IServiceProvider services)
        {
            _services = services;
        }

        /// <summary>
        /// Creates a new instance of <see cref="MainBehavior"/>.
        /// </summary>
        public MainBehavior Create()
        {
            return _services.GetService<MainBehavior>();
        }
    }
}
