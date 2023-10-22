using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Ruse2023.Models.Account
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = null!;
        [Required]
        [PasswordPropertyText]
        [Display(Name = "Парола")]
        public string Password { get; set; } = null!;

        public string? ReturnUrl { get; set; }
    }
}
