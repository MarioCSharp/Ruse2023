using System.ComponentModel.DataAnnotations;

namespace Ruse2023.Models.Store
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public byte[] Image { get; set; }
    }
}
