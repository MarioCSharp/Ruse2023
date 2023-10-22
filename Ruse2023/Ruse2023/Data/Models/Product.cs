using System.ComponentModel.DataAnnotations;

namespace Ruse2023.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int Price { get; set; }
        [Required]
        public byte[] Image { get; set; } = null!;
    }
}
