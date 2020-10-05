using System;

namespace Faith.Factories
{
    /// <summary>
    /// Basic object factory for use with <see cref="IServiceProvider"/>.
    /// </summary>
    interface IFactory<T>
    {
        /// <summary>
        /// Creates a new instance of <typeparamref name="T" />.
        /// </summary>
        T Create();
    }
}
