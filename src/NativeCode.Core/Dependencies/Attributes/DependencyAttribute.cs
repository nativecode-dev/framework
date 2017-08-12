namespace NativeCode.Core.Dependencies.Attributes
{
    using System;
    using Enums;

    /// <summary>
    /// Marks a class as being an implementation dependency.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class DependencyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute" /> class.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <param name="key">The key.</param>
        /// <param name="lifetime">The lifetime.</param>
        public DependencyAttribute(Type contract = null, string key = default(string),
            DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            this.Contract = contract;
            this.Key = key;
            this.Lifetime = lifetime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute" /> class.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <param name="dependencyKey">The dependency key.</param>
        /// <param name="lifetime">The lifetime.</param>
        public DependencyAttribute(Type contract, DependencyKey dependencyKey,
            DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            this.Contract = contract;
            this.KeyType = dependencyKey;
            this.Lifetime = lifetime;
        }

        /// <summary>
        /// Gets the contract.
        /// </summary>
        /// <value>The contract.</value>
        public Type Contract { get; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; }

        /// <summary>
        /// Gets the type of the key.
        /// </summary>
        /// <value>The type of the key.</value>
        public DependencyKey KeyType { get; }

        /// <summary>
        /// Gets the lifetime.
        /// </summary>
        /// <value>The lifetime.</value>
        public DependencyLifetime Lifetime { get; }
    }
}