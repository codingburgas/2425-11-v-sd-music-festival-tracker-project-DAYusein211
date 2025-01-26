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
        public async Task<ActionResult<IEnumerable<object>>> GetFestivals()
        {
            var festivals = await _context.Festivals
                .Join(_context.Artists,
                    festival => festival.ArtistId,
                    artist => artist.Id,
                    (festival, artist) => new
                    {
                        festival.Id,
                        festival.Name,
                        festival.Description,
                        festival.Location,
                        festival.Date,
                        festival.ArtistId,
                        ArtistName = artist.FirstName + " " + artist.LastName,  // Fetch full name
                        ArtistRating = artist.Rating  // Fetch artist rating
                    })
                .ToListAsync();

            return Ok(festivals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetFestival(int id)
        {
            var festival = await _context.Festivals
                .Join(_context.Artists,
                    f => f.ArtistId,
                    a => a.Id,
                    (f, a) => new
                    {
                        f.Id,
                        f.Name,
                        f.Description,
                        f.Location,
                        f.Date,
                        f.ArtistId,
                        ArtistName = a.FirstName + " " + a.LastName,
                        ArtistRating = a.Rating
                    })
                .FirstOrDefaultAsync(f => f.Id == id);

            if (festival == null) return NotFound();

            return Ok(festival);
        }

        [HttpPost]
        public async Task<ActionResult<Festival>> PostFestival([FromBody] Festival festival)
        {
            // 🔥 Ensure the artist exists
            var artistExists = await _context.Artists.AnyAsync(a => a.Id == festival.ArtistId);
            if (!artistExists)
            {
                return BadRequest("Artist not found.");
            }

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
