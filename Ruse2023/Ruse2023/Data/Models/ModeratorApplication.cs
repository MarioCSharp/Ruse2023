using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruse2023.Data.Models
{
    public class ModeratorApplication
    {
        public int Id { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 20)]
        public string Description { get; set; } = null!;
        [Required]
        [StringLength(13, MinimumLength = 8)]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public bool Approved { get; set; }
        public string Feedback { get; set; } = null!;
    }
}
