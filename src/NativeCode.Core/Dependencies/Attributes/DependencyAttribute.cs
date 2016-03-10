namespace NativeCode.Core.Dependencies.Attributes
{
    using System;

    using NativeCode.Core.Dependencies.Enums;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DependencyAttribute : Attribute
    {
        public DependencyAttribute(Type contract = default(Type), string key = default(string), DependencyLifetime lifetime = default(DependencyLifetime))
        {
            this.Contract = contract;
            this.Key = key;
            this.Lifetime = lifetime;
        }

        public Type Contract { get; }

        public string Key { get; }

        public DependencyLifetime Lifetime { get; }
    }
}