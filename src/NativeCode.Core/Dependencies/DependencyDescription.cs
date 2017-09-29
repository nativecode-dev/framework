namespace NativeCode.Core.Dependencies
{
    using System;
    using Enums;

    public class DependencyDescription
    {
        public Type Contract { get; set; }

        public Type Implementation { get; set; }

        public DependencyKey Key { get; set; }

        public string KeyValue { get; set; }

        public DependencyLifetime Lifetime { get; set; }
    }
}