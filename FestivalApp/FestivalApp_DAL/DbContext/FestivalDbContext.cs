using FestivalApp_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FestivalApp_DAL.Contexts
{
    public class FestivalDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public FestivalDbContext(DbContextOptions<FestivalDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Guest> Guests { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Festival> Festivals { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
    }
}