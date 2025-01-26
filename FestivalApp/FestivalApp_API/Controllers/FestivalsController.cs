using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FestivalApp_DAL.Contexts;
using FestivalApp_DAL.Models;

namespace FestivalApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FestivalsController : ControllerBase
    {
        private readonly FestivalDbContext _context;

        public FestivalsController(FestivalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Festival>>> GetFestivals()
        {
            var festivals = await _context.Festivals
                .Include(f => f.Artist) // Include artist details
                .ToListAsync();

            return Ok(festivals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Festival>> GetFestival(int id)
        {
            var festival = await _context.Festivals.FindAsync(id);
            if (festival == null) return NotFound();
            return festival;
        }

        [HttpPost]
        public async Task<ActionResult<Festival>> PostFestival(Festival festival)
        {
            _context.Festivals.Add(festival);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFestival), new { id = festival.Id }, festival);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFestival(int id, Festival festival)
        {
            if (id != festival.Id) return BadRequest();
            _context.Entry(festival).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFestival(int id)
        {
            var festival = await _context.Festivals.FindAsync(id);
            if (festival == null) return NotFound();
            _context.Festivals.Remove(festival);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}