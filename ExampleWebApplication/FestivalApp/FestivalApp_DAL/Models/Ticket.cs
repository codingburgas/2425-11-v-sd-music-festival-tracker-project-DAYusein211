namespace FestivalApp_DAL.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int FestivalId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public decimal Price { get; set; }
    }
}