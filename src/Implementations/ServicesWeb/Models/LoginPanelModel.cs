namespace ServicesWeb.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginPanelModel
    {
        [Required]
        [MaxLength(256)]
        public string Login { get; set; }

        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        public string RedirectUrl { get; set; }
    }
}