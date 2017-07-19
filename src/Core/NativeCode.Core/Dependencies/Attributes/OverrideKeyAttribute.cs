namespace NativeCode.Core.Dependencies.Attributes
{
    using System;
    using Enums;

    /// <summary>
    /// Marks a class as overriding the default registration and specifies a string key to be
    /// used when registered.
    /// This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class OverrideKeyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OverrideKeyAttribute" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public OverrideKeyAttribute(DependencyKey key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public DependencyKey Key { get; }
    }
}