using System.ComponentModel.DataAnnotations;
using Partys.Data;

namespace Partys.Models
{
    public class Invitation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Guest name is required")]
        public string? GuestName { get; set; }

        [Required(ErrorMessage = "Guest email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string? GuestEmail { get; set; }

        public bool IsSent { get; set; } = false;
        public string Status { get; set; } = "Pending";  // Pending / Accepted / Declined
        public DateTime? SentDate { get; set; }

        // FK
        public int PartyId { get; set; }
        public Party? Party { get; set; }
    }
}
