using System.ComponentModel.DataAnnotations;

namespace Ruse2023.Models.Store
{
    public class ProductCheckOutModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
