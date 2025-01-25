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

        // ✅ GET: api/ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
        {
            return await _context.Ratings.ToListAsync();
        }

        // ✅ GET: api/ratings/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null) return NotFound();
            return rating;
        }

        // ✅ POST: api/ratings
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating([FromBody] Rating rating)
        {
            if (rating == null)
                return BadRequest("Rating data is null.");

            // ✅ Validate RatingValue (must be between 1 and 5)
            if (rating.RatingValue < 1 || rating.RatingValue > 5)
                return BadRequest("Rating value must be between 1 and 5.");

            rating.RatingValue = (float)rating.RatingValue; // ✅ Ensure float type

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            // ✅ Update Artist's average rating
            var artist = await _context.Artists.FindAsync(rating.ArtistId);
            if (artist != null)
            {
                var artistRatings = await _context.Ratings
                    .Where(r => r.ArtistId == rating.ArtistId)
                    .ToListAsync();

                artist.Rating = artistRatings.Any() ? artistRatings.Average(r => r.RatingValue) : 0;

                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetRating), new { id = rating.Id }, rating);
        }
    }
}
