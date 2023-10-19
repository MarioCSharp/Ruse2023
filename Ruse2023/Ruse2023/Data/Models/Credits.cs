using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruse2023.Data.Models
{
    public class Credits
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        public int Ammount { get; set; }
    }
}
