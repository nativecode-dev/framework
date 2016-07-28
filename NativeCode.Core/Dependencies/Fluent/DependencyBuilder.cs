namespace NativeCode.Core.Dependencies.Fluent
{
    using System;

    public class DependencyBuilder
    {
        private readonly DependencyProperties properties;

        public DependencyBuilder() : this(new DependencyProperties())
        {
        }

        private DependencyBuilder(DependencyProperties properties)
        {
            this.properties = properties;
        }

        public DependencyBuilder If(Predicate<Type> filter)
        {
            this.properties.TypeFilter = filter;

            return this;
        }

        private class DependencyProperties
        {
            public Predicate<Type> TypeFilter { get; set; }
        }
    }
}