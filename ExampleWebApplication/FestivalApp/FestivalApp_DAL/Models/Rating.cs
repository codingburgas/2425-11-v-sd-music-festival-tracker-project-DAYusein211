using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FestivalApp_DAL.Models
{
    public class Rating
    {
        [Key] 
        public int Id { get; set; }

        [ForeignKey("Guest")] 
        public int GuestId { get; set; }  
        
        [ForeignKey("Artist")]
        public int ArtistId { get; set; } 
        
        public float RatingValue { get; set; } 
        
    }
}