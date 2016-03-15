namespace ServicesWeb.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Web.Validation;

    public class DatabaseUpgradeModel
    {
        public string UpgradeErrorMessage { get; set; }

        [Required(ErrorMessage = "You must provide the verification token.")]
        [ValidateSettingsValue("Settings.DatabaseVerificationToken", typeof(string))]
        public string VerificationToken { get; set; }
    }
}