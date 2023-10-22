using System.ComponentModel.DataAnnotations;

namespace Ruse2023.Models.Shopping
{
    public class ShoppingApplicationModel
    {
        [Required]
        public string Description { get; set; } = null!;
        public byte[] Image { get; set; }
    }
}
