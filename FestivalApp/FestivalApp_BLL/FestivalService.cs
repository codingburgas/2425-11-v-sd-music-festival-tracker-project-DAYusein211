using FestivalApp.DAL.Models;
namespace FestivalApp.BLL
{
    public class FestivalService : IFestivalService
    {
        private readonly IAppDbContext _context;

        public FestivalService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Festival>> GetAllFestivalsAsync()
        {
            return await _context.Festivals.ToListAsync();
        }

        public async Task<Festival> GetFestivalByIdAsync(int id)
        {
            return await _context.Festivals.FindAsync(id);
        }

        public async Task AddFestivalAsync(Festival festival)
        {
            _context.Festivals.Add(festival);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFestivalAsync(Festival festival)
        {
            _context.Festivals.Update(festival);
            await _context.SaveChangesAsync();
        }
    }
}