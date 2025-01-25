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
        public decimal Price { get; set; }

        // Foreign Key for Guest
        [Required]
        public int GuestId { get; set; }

        [ForeignKey("GuestId")]
        public Guest Guest { get; set; } = null!;

        // Foreign Key for Festival
        [Required]
        public int FestivalId { get; set; }

        [ForeignKey("FestivalId")]
        public Festival Festival { get; set; } = null!;
    }
}