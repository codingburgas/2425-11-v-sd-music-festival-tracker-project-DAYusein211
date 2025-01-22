using FestivalApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.Data
{
    public interface IAppDbContext
    {
        DbSet<Festival> Festivals { get; set; }
        // Add other DbSets here as needed (e.g., for other models like Artist, Ticket, etc.)

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}