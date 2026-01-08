using System.ComponentModel.DataAnnotations;
using Partys.Models;

namespace Partys.Models
{
    public class Party
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Event date is required")]
        public DateTime EventDate { get; set; }

        public string? Location { get; set; }

        public bool IsDeleted { get; set; } = false;

        public List<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}
