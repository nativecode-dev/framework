namespace Common.Models.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [Required]
        [StringLength(256)]
        public string Login { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        public string RedirectUrl { get; set; }
    }
}