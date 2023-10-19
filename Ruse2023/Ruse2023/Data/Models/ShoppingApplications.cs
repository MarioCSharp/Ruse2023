using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruse2023.Data.Models
{
    public class ShoppingApplications
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public byte[] Image { get; set; }
    }
}
