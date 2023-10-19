using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Ruse2023.Models.Account
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;

        public string? ReturnUrl { get; set; }
    }
}
