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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
        {
            return await _context.Ratings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null) return NotFound();
            return rating;
        }

        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            // Update Artist's average rating
            var artist = await _context.Artists.FindAsync(rating.ArtistId);
            if (artist != null)
            {
                var artistRatings = _context.Ratings.Where(r => r.ArtistId == rating.ArtistId);
                artist.Rating = artistRatings.Average(r => r.RatingValue);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetRating), new { id = rating.Id }, rating);
        }
    }
}