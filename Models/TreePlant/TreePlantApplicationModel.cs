using System.ComponentModel.DataAnnotations;

namespace Ruse2023.Models.TreePlant
{
    public class TreePlantApplicationModel
    {
        [Required]
        public string Description { get; set; } = null!;
        public byte[] Image { get; set; }
    }
}
