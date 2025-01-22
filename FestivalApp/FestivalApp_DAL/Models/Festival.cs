namespace FestivalApp.DAL.Models
{
    public class Festival
    {
        public int Id { get; set; }  // Primary key
        public string Name { get; set; }  // Festival name
        public DateTime StartDate { get; set; }  // Start date of the festival
        public DateTime EndDate { get; set; }  // End date of the festival
        public string Location { get; set; }  // Location of the festival
        public string Description { get; set; }  // Description of the festival

        // Optional: any other fields, such as TicketPrice, Categories, etc.
    }
}