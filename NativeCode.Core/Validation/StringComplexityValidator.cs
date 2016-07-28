namespace NativeCode.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class StringComplexityValidator : IObjectValidator
    {
        public bool CanValidate(object instance)
        {
            if (instance == null)
            {
                return false;
            }

            return this.CanValidate(instance.GetType());
        }

        public bool CanValidate(Type type)
        {
            return type == typeof(string);
        }

        public ValidationResult Validate(object instance, ValidationContext validationContext)
        {
            return null;
        }
    }
}