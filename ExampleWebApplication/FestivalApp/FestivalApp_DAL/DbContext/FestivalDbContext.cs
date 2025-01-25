using Microsoft.EntityFrameworkCore;
using FestivalApp_DAL.Models;

namespace FestivalApp_DAL.Contexts
{
    public class FestivalDbContext : DbContext
    {
        public FestivalDbContext(DbContextOptions<FestivalDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:festival-server.database.windows.net,1433;Initial Catalog=FestivalDB;Persist Security Info=False;User ID=sqladmin;Password=Denis999@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
                    b => b.MigrationsAssembly("FestivalApp_API")); // 👈 Add this line
            }
        }

        public DbSet<Guest> Guests { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Festival> Festivals { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
    }
}