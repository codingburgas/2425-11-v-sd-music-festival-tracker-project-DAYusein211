using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FestivalApp_DAL.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        [Column(TypeName = "float")]
        public double Rating { get; set; }  
        
    }
}