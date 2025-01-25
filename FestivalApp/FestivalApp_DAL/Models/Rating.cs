using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FestivalApp_DAL.Models
{
    public class Rating
    {
        public int Id { get; set; }

        // Foreign Key for Guest
        [Required]
        public int GuestId { get; set; }

        [ForeignKey("GuestId")]
        public Guest Guest { get; set; } = null!;

        // Foreign Key for Artist
        [Required]
        public int ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; } = null!;

        [Required]
        [Range(1, 5)]
        [Column(TypeName = "float")]  // ✅ Ensure correct mapping
        public double RatingValue { get; set; }
    }
}