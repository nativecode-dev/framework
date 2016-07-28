namespace NativeCode.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Dependencies;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class StringComplexityAttribute : ValidationAttribute
    {
        public StringComplexityAttribute(StringComplexityRules rules, string errorMessage = default(string))
            : base(errorMessage)
        {
            this.StringComplexityRules = rules;
        }

        public StringComplexityRules StringComplexityRules { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }

            var validator = DependencyLocator.Resolver.Resolve<StringComplexityValidator>();

            if (validator != null && validator.CanValidate(value))
            {
                return validator.Validate(value, validationContext);
            }

            return null;
        }
    }
}