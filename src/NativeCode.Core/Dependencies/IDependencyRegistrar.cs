namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Reflection;

    using JetBrains.Annotations;

    using NativeCode.Core.Dependencies.Enums;

    public interface IDependencyRegistrar
    {
        IDependencyRegistrar RegisterAssembly([NotNull] Assembly assembly);

        IDependencyRegistrar RegisterFactory([NotNull] Type type, [NotNull] Func<IDependencyResolver, object> factory, string key = default(string), DependencyLifetime lifetime = default(DependencyLifetime));

        IDependencyRegistrar RegisterFactory<T>([NotNull] Func<IDependencyResolver, T> factory, string key = default(string), DependencyLifetime lifetime = default(DependencyLifetime));

        IDependencyRegistrar Register([NotNull] Type type, string key = default(string), DependencyLifetime lifetime = default(DependencyLifetime));

        IDependencyRegistrar Register([NotNull] Type type, [NotNull] Type implementation, string key = default(string), DependencyLifetime lifetime = default(DependencyLifetime));

        IDependencyRegistrar Register<T>(string key = default(string), DependencyLifetime lifetime = default(DependencyLifetime));

        IDependencyRegistrar Register<T, TImplementation>(string key = default(string), DependencyLifetime lifetime = default(DependencyLifetime)) where TImplementation : T;

        IDependencyRegistrar Register<T, TImplementation>(DependencyKey key, DependencyLifetime lifetime = default(DependencyLifetime)) where TImplementation : T;

        IDependencyRegistrar RegisterInstance([NotNull] Type type, [NotNull] object instance);

        IDependencyRegistrar RegisterInstance<T>([NotNull] T instance);
    }
}