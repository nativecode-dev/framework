namespace NativeCode.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Performs validation against a <c>string</c> to ensure that it meets the
    /// specified complexity.
    /// </summary>
    /// <seealso cref="NativeCode.Core.Validation.IObjectValidator" />
    public class StringComplexityValidator : IObjectValidator
    {
        /// <summary>
        /// Determines whether this instance can validate the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns><c>true</c> if this instance can validate the specified instance; otherwise, <c>false</c>.</returns>
        public bool CanValidate(object instance)
        {
            return instance != null && this.CanValidate(instance.GetType());
        }

        /// <summary>
        /// Determines whether this instance can validate the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this instance can validate the specified type; otherwise, <c>false</c>.</returns>
        public bool CanValidate(Type type)
        {
            return type == typeof(string);
        }

        /// <summary>
        /// Determines whether this instance can validate.
        /// </summary>
        /// <typeparam name="T">The type to validate.</typeparam>
        /// <returns><c>true</c> if this instance can validate; otherwise, <c>false</c>.</returns>
        public bool CanValidate<T>()
        {
            return this.CanValidate(typeof(T));
        }

        /// <summary>
        /// Validates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>Returns a <see cref="ValidationResult" />.</returns>
        public ValidationResult Validate(object instance, ValidationContext validationContext)
        {
            return null;
        }
    }
}