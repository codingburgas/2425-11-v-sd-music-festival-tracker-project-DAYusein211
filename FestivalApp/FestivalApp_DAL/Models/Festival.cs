using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FestivalApp_DAL.Models
{
    public class Festival
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int ArtistId { get; set; }
    }
}