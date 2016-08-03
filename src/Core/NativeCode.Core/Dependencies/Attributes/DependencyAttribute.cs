namespace NativeCode.Core.Dependencies.Attributes
{
    using System;

    using NativeCode.Core.Dependencies.Enums;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class DependencyAttribute : Attribute
    {
        public DependencyAttribute(Type contract = null, string key = default(string), DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            this.Contract = contract;
            this.Key = key;
            this.Lifetime = lifetime;
        }

        public DependencyAttribute(Type contract, DependencyKey dependencyKey, DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            this.Contract = contract;
            this.KeyType = dependencyKey;
            this.Lifetime = lifetime;
        }

        public Type Contract { get; }

        public string Key { get; }

        public DependencyKey KeyType { get; }

        public DependencyLifetime Lifetime { get; }
    }
}