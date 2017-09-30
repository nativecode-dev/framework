namespace NativeCode.Core.Dependencies.Attributes
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using JetBrains.Annotations;

    /// <summary>
    /// Marks a class so that the dependency container does not register the class as
    /// an implementation service.
    /// This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class IgnoreDependencyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreDependencyAttribute" /> class.
        /// </summary>
        public IgnoreDependencyAttribute() : this("Do not register as dependency.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreDependencyAttribute" /> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="inheritable">if set to <c>true</c> [inheritable].</param>
        /// <param name="throwOnError">if set to <c>true</c> [throw on error].</param>
        public IgnoreDependencyAttribute(string reason, bool inheritable = true, bool throwOnError = true)
        {
            this.Inheritable = inheritable;
            this.Reason = reason;
            this.ThrowOnError = throwOnError;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IgnoreDependencyAttribute" /> is inheritable.
        /// </summary>
        /// <value><c>true</c> if inheritable; otherwise, <c>false</c>.</value>
        public bool Inheritable { get; }

        /// <summary>
        /// Gets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public string Reason { get; }

        /// <summary>
        /// Gets a value indicating whether [throw on error].
        /// </summary>
        /// <value><c>true</c> if [throw on error]; otherwise, <c>false</c>.</value>
        public bool ThrowOnError { get; }

        /// <summary>
        /// Validates the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static bool ValidateType([NotNull] Type type)
        {
            var attribute = type.GetTypeInfo().GetCustomAttribute<IgnoreDependencyAttribute>(true);

            if (attribute != null)
            {
                Debug.WriteLine(attribute.Reason);

                if (attribute.Inheritable && attribute.ThrowOnError)
                {
                    throw new InvalidOperationException(
                        $"Registration for {type.Name} not allowed due to {attribute.Reason}.");
                }

                return false;
            }

            return true;
        }
    }
}