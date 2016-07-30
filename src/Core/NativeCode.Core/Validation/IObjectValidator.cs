namespace NativeCode.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Provides a contract to validate an object's properties.
    /// </summary>
    public interface IObjectValidator
    {
        /// <summary>
        /// Determines whether this instance can validate the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns><c>true</c> if this instance can validate the specified instance; otherwise, <c>false</c>.</returns>
        bool CanValidate(object instance);

        /// <summary>
        /// Determines whether this instance can validate the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this instance can validate the specified type; otherwise, <c>false</c>.</returns>
        bool CanValidate(Type type);

        /// <summary>
        /// Determines whether this instance can validate.
        /// </summary>
        /// <typeparam name="T">The type to validate.</typeparam>
        /// <returns><c>true</c> if this instance can validate; otherwise, <c>false</c>.</returns>
        bool CanValidate<T>();

        /// <summary>
        /// Validates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>Returns a <see cref="ValidationResult" />.</returns>
        ValidationResult Validate(object instance, ValidationContext validationContext);
    }
}