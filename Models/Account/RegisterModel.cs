using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Ruse2023.Data;

namespace Ruse2023.Models.Account
{
    public class RegisterModel
    {
        [Required]
        [StringLength(Constants.User.FirstNameMaxLength, MinimumLength = Constants.User.FirstNameMinLength)]
        [Display(Name = "Първо име")]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(Constants.User.LastNameMaxLength, MinimumLength = Constants.User.LastNameMinLength)]
        [Display(Name = "Последно име")]
        public string LastName { get; set; } = null!;
        [Required]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = null!;
        [Required]
        [Display(Name = "Телефонен номера")]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        [Display(Name = "Рожденна дата")]
        public DateTime BirthDate { get; set; }
        [Required]
        [PasswordPropertyText]
        [Compare(nameof(PasswordRepeat))]
        [Display(Name = "Парола")]
        public string Password { get; set; } = null!;
        [Required]
        [PasswordPropertyText]
        [Display(Name = "Потвърди парола")]
        public string PasswordRepeat { get; set; } = null!;
    }
}
