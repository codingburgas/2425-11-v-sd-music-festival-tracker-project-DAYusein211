using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FestivalApp_DAL.Contexts;
using FestivalApp_DAL.Models;

namespace FestivalApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly FestivalDbContext _context;

        public ArtistsController(FestivalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            return await _context.Artists.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return NotFound();
            return artist;
        }

        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        {
            Console.WriteLine($"Received Artist Registration: {artist.Email}");

            if (artist == null || string.IsNullOrWhiteSpace(artist.Email))
            {
                Console.WriteLine("Invalid artist data received.");
                return BadRequest("Invalid artist data.");
            }

            // Check if the email already exists
            var existingArtist = await _context.Artists.FirstOrDefaultAsync(a => a.Email == artist.Email);
            if (existingArtist != null)
            {
                Console.WriteLine($"Artist with email {artist.Email} already exists.");
                return Conflict("User with this email already exists.");
            }

            try
            {
                artist.PasswordHash = BCrypt.Net.BCrypt.HashPassword(artist.PasswordHash);
                _context.Artists.Add(artist);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Artist {artist.Email} registered successfully.");
                return CreatedAtAction(nameof(GetArtist), new { id = artist.Id }, artist);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding artist: {ex.Message}");
                return StatusCode(500, "An error occurred while registering the artist.");
            }
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, Artist artist)
        {
            if (id != artist.Id) return BadRequest();
            _context.Entry(artist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("rating")]
        public async Task<IActionResult> UpdateArtistRating([FromBody] ArtistRatingUpdateRequest request)
        {
            var artist = await _context.Artists.FindAsync(request.ArtistId);
            if (artist == null)
            {
                return NotFound("Artist not found.");
            }

            // ✅ Simple rating formula: (Previous Rating + New Rating) / 2
            artist.Rating = (artist.Rating + request.NewRating) / 2.0;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Artist rating updated successfully.", artist.Rating });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return NotFound();
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
public class ArtistRatingUpdateRequest
{
    public int ArtistId { get; set; }
    public int NewRating { get; set; }
}