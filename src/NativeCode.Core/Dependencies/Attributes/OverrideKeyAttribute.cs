namespace NativeCode.Core.Dependencies.Attributes
{
    using System;

    using NativeCode.Core.Dependencies.Enums;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class OverrideKeyAttribute : Attribute
    {
        public OverrideKeyAttribute(DependencyKey key)
        {
            this.Key = key;
        }

        public DependencyKey Key { get; }
    }
}