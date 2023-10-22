using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ruse2023.Models.Moderator
{
    public class ApplicationApprovalModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public bool Approved { get; set; }
        public string Feedback { get; set; } = null!;
    }
}
