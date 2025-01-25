using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FestivalApp_DAL.Contexts;
using FestivalApp_DAL.Models;

namespace FestivalApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly FestivalDbContext _context;

        public GuestsController(FestivalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guest>>> GetGuests()
        {
            return await _context.Guests.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Guest>> GetGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return NotFound();
            return guest;
        }

        [HttpPost]
        public async Task<ActionResult<Guest>> PostGuest(Guest guest)
        {
            Console.WriteLine($"Received Guest Registration: {guest.Email}");

            if (guest == null || string.IsNullOrWhiteSpace(guest.Email))
            {
                Console.WriteLine("Invalid guest data received.");
                return BadRequest("Invalid guest data.");
            }

            // Check if the email already exists
            var existingGuest = await _context.Guests.FirstOrDefaultAsync(g => g.Email == guest.Email);
            if (existingGuest != null)
            {
                Console.WriteLine($"Guest with email {guest.Email} already exists.");
                return Conflict("User with this email already exists.");
            }

            try
            {
                guest.PasswordHash = BCrypt.Net.BCrypt.HashPassword(guest.PasswordHash);
                _context.Guests.Add(guest);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Guest {guest.Email} registered successfully.");
                return CreatedAtAction(nameof(GetGuest), new { id = guest.Id }, guest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding guest: {ex.Message}");
                return StatusCode(500, "An error occurred while registering the guest.");
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuest(int id, Guest guest)
        {
            if (id != guest.Id) return BadRequest();
            _context.Entry(guest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return NotFound();
            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}