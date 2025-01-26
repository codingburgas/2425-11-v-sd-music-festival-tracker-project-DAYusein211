using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FestivalApp_DAL.Models
{
    public class Rating
    {
        public int Id { get; set; }
        
        [Required]
        public int GuestId { get; set; }
        
        [Required]
        public int ArtistId { get; set; }

        [Required]
        [Range(1, 5)]
        [Column(TypeName = "float")]  
        public double RatingValue { get; set; }
    }
}