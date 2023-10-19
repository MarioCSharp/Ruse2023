using System.ComponentModel.DataAnnotations;

namespace Ruse2023.Models.Moderator
{
    public class ApplicationModel
    {
        [Required]
        [StringLength(500, MinimumLength = 20)]
        public string Description { get; set; } = null!;
        [Required]
        [StringLength(13, MinimumLength = 8)]
        public string PhoneNumber { get; set; } = null!;
    }
}
