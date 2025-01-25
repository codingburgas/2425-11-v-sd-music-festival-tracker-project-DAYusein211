namespace FestivalApp_DAL.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int ArtistId { get; set; }
        public int RatingValue { get; set; }
    }
}