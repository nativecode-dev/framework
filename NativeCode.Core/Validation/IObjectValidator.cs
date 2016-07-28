namespace NativeCode.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public interface IObjectValidator
    {
        bool CanValidate(object instance);

        bool CanValidate(Type type);

        ValidationResult Validate(object instance, ValidationContext validationContext);
    }
}