using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FestivalApp_DAL.Contexts;
using FestivalApp_DAL.Models;

namespace FestivalApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly FestivalDbContext _context;

        public RatingsController(FestivalDbContext context)
        {
            _context = context;
        }

        // ✅ Get all ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
        {
            return await _context.Ratings.ToListAsync();
        }

        // ✅ Submit a new rating
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating([FromBody] Rating rating)
        {
            if (rating.RatingValue < 1 || rating.RatingValue > 5)
            {
                return BadRequest("Rating value must be between 1 and 5.");
            }

            // ✅ Ensure the artist exists
            var artist = await _context.Artists.FindAsync(rating.ArtistId);
            if (artist == null)
            {
                return BadRequest("Artist not found.");
            }

            // ✅ Ensure the guest exists
            var guest = await _context.Guests.FindAsync(rating.GuestId);
            if (guest == null)
            {
                return BadRequest("Guest not found.");
            }

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRatings), new { id = rating.Id }, rating);
        }
    }
}