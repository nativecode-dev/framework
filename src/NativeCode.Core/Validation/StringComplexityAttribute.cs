namespace NativeCode.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Dependencies;

    /// <summary>
    /// Marks a field or property that is a <c>string</c> as requiring a specific set of complexities.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class StringComplexityAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringComplexityAttribute" /> class.
        /// </summary>
        /// <param name="rules">The rules.</param>
        /// <param name="errorMessage">The error message.</param>
        public StringComplexityAttribute(StringComplexityRules rules,
            string errorMessage = default(string)) : base(errorMessage)
        {
            this.StringComplexityRules = rules;
        }

        /// <summary>
        /// Gets the string complexity rules.
        /// </summary>
        /// <value>The string complexity rules.</value>
        public StringComplexityRules StringComplexityRules { get; }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }

            using (var resolver = DependencyLocator.CreateResolver())
            {
                var validator = resolver.Resolve<StringComplexityValidator>();

                if (validator != null && validator.CanValidate(value))
                {
                    return validator.Validate(value, validationContext);
                }

                return null;
            }
        }
    }
}