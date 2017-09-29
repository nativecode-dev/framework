namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract to resolve dependencies.
    /// </summary>
    /// <seealso cref="System.IServiceProvider" />
    public interface IDependencyResolver : IDisposable, IServiceProvider
    {
        IDependencyResolver CreateChildResolver();

        /// <summary>
        /// Resolves the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        object Resolve(Type type, string key = default(string));

        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        T Resolve<T>(string key = default(string));

        /// <summary>
        /// Resolves all.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IEnumerable&lt;System.Object&gt;.</returns>
        IEnumerable<object> ResolveAll([NotNull] Type type);

        /// <summary>
        /// Resolves all.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> ResolveAll<T>();
    }
}