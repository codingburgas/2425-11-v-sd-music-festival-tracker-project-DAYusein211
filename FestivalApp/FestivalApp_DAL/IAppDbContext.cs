using Microsoft.EntityFrameworkCore;

namespace FestivalApp.Data
{
    public interface IAppDbContext
    {
        DbSet<Festival> Festivals { get; set; }
        // Other DbSets for additional models

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); // Only declare once
    }
}
