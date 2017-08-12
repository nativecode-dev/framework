namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Reflection;
    using Enums;
    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract for registering dependencies.
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// Registers the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>IDependencyRegistrar.</returns>
        IDependencyRegistrar Register([NotNull] Type type, string key = default(string),
            DependencyLifetime lifetime = default(DependencyLifetime));

        /// <summary>
        /// Registers the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="implementation">The implementation.</param>
        /// <param name="key">The key.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>IDependencyRegistrar.</returns>
        IDependencyRegistrar Register(
            [NotNull] Type type,
            [NotNull] Type implementation,
            string key = default(string),
            DependencyLifetime lifetime = default(DependencyLifetime));

        /// <summary>
        /// Registers the specified key.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>IDependencyRegistrar.</returns>
        IDependencyRegistrar Register<T>(string key = default(string),
            DependencyLifetime lifetime = default(DependencyLifetime));

        /// <summary>
        /// Registers the specified key.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <typeparam name="TImplementation">The type of the t implementation.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>IDependencyRegistrar.</returns>
        IDependencyRegistrar Register<T, TImplementation>(string key = default(string),
            DependencyLifetime lifetime = default(DependencyLifetime))
            where TImplementation : T;

        /// <summary>
        /// Registers the specified key.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <typeparam name="TImplementation">The type of the t implementation.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>IDependencyRegistrar.</returns>
        IDependencyRegistrar Register<T, TImplementation>(DependencyKey key,
            DependencyLifetime lifetime = default(DependencyLifetime))
            where TImplementation : T;

        /// <summary>
        /// Registers the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>IDependencyRegistrar.</returns>
        IDependencyRegistrar RegisterAssembly([NotNull] Assembly assembly);

        /// <summary>
        /// Registers the factory.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="key">The key.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>IDependencyRegistrar.</returns>
        IDependencyRegistrar RegisterFactory(
            [NotNull] Type type,
            [NotNull] Func<IDependencyResolver, object> factory,
            string key = default(string),
            DependencyLifetime lifetime = default(DependencyLifetime));

        /// <summary>
        /// Registers the factory.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="factory">The factory.</param>
        /// <param name="key">The key.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>IDependencyRegistrar.</returns>
        IDependencyRegistrar RegisterFactory<T>(
            [NotNull] Func<IDependencyResolver, T> factory,
            string key = default(string),
            DependencyLifetime lifetime = default(DependencyLifetime));

        /// <summary>
        /// Registers the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>IDependencyRegistrar.</returns>
        IDependencyRegistrar RegisterInstance([NotNull] Type type, [NotNull] object instance,
            DependencyLifetime lifetime = default(DependencyLifetime));

        /// <summary>
        /// Registers the instance.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>IDependencyRegistrar.</returns>
        IDependencyRegistrar RegisterInstance<T>([NotNull] T instance,
            DependencyLifetime lifetime = default(DependencyLifetime));
    }
}