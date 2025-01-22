using FestivalApp.DAL;
using FestivalApp.DAL.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.BLL
{
    public class FestivalService
    {
        private readonly AppDbContext _context;

        public FestivalService(AppDbContext context)
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