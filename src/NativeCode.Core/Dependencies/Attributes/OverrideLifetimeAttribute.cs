namespace NativeCode.Core.Dependencies.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class OverrideLifetimeAttribute : Attribute
    {
        public OverrideLifetimeAttribute(DependencyLifetime lifetime)
        {
            this.Lifetime = lifetime;
        }

        public DependencyLifetime Lifetime { get; }
    }
}