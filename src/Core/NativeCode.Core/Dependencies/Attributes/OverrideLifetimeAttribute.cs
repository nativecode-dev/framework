namespace NativeCode.Core.Dependencies.Attributes
{
    using System;
    using Enums;

    /// <summary>
    /// Marks a class as overriding the default registration and specifies a lifetime to be
    /// used when registered.
    /// This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class OverrideLifetimeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OverrideLifetimeAttribute" /> class.
        /// </summary>
        /// <param name="lifetime">The lifetime.</param>
        public OverrideLifetimeAttribute(DependencyLifetime lifetime)
        {
            this.Lifetime = lifetime;
        }

        /// <summary>
        /// Gets the lifetime.
        /// </summary>
        /// <value>The lifetime.</value>
        public DependencyLifetime Lifetime { get; }
    }
}