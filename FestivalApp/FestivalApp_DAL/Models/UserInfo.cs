namespace FestivalApp_DAL.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // "Guest" or "Artist"
        public double? Rating { get; set; } // Nullable, because Guests don't have a rating
    }
}