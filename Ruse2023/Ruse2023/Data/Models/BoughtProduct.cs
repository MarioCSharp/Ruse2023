using System.ComponentModel.DataAnnotations.Schema;

namespace Ruse2023.Data.Models
{
    public class BoughtProduct
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

    }
}
