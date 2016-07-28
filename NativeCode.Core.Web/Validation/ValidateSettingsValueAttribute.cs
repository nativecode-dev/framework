namespace NativeCode.Core.Web.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ValidateSettingsValueAttribute : ValidationAttribute
    {
        public ValidateSettingsValueAttribute(string key, Type type, bool requireSettingValue = false)
        {
            this.Key = key;
            this.RequireSettingsValue = requireSettingValue;
            this.Type = type;
        }

        public string Key { get; }

        public bool RequireSettingsValue { get; }

        public Type Type { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }

            var application = DependencyLocator.Resolver.Resolve<IApplication>();
            var setting = application.Settings.GetValue<object>(this.Key);

            if (setting == null && this.RequireSettingsValue)
            {
                return new ValidationResult("Settings value was null and expected it to exist.");
            }

            if (Equals(setting, value) == false)
            {
                var message = this.FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult($"Settings value did not match value supplied. {message}.");
            }

            return null;
        }
    }
}