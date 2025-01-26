using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FestivalApp_DAL.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        
        [Required]
        public int GuestId { get; set; }

        [Required]
        public int FestivalId { get; set; }
    }
}