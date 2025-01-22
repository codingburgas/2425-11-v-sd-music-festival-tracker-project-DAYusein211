using Microsoft.EntityFrameworkCore;
using FestivalApp.DAL.Models;
namespace FestivalApp.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Festival> Festivals { get; set; }

        // Add other DbSets here as needed (e.g., for other models like Artist, Ticket, etc.)

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // You can override OnModelCreating if needed for configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure model relationships, etc.
        }
    }
}