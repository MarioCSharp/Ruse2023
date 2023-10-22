using System.ComponentModel.DataAnnotations;

namespace Ruse2023.Models.Moderator
{
    public class ApplicationDeclineModel
    {
        public int Id { get; set; }
        [Required]
        public string Feedback { get; set; } = null!;
    }
}
