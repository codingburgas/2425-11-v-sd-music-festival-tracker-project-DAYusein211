using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FestivalApp_DAL.Contexts;
using FestivalApp_DAL.Models;

namespace FestivalApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FestivalDbContext _context;

        public AuthController(FestivalDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserInfo>> Login([FromBody] LoginRequest request)
        {
            Console.WriteLine($"Login attempt: {request.Email}");

            var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Email == request.Email);
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.Email == request.Email);

            if (guest == null && artist == null)
            {
                Console.WriteLine("User not found.");
                return Unauthorized("Invalid email or password.");
            }

            var storedPasswordHash = guest != null ? guest.PasswordHash : artist.PasswordHash;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, storedPasswordHash))
            {
                Console.WriteLine("Incorrect password.");
                return Unauthorized("Invalid email or password.");
            }

            var response = new UserInfo
            {
                Id = guest != null ? guest.Id : artist.Id,
                FirstName = guest != null ? guest.FirstName : artist.FirstName,
                LastName = guest != null ? guest.LastName : artist.LastName,
                Email = request.Email,
                Role = guest != null ? "Guest" : "Artist",
                Rating = artist != null ? artist.Rating : null // Only Artists have a rating
            };

            Console.WriteLine($"Login successful: {response.Email}");
            return Ok(new
            {
                Id = guest != null ? guest.Id : artist.Id,
                FirstName = guest != null ? guest.FirstName : artist.FirstName,
                LastName = guest != null ? guest.LastName : artist.LastName,
                Email = request.Email,
                Role = guest != null ? "Guest" : "Artist",
                Rating = artist != null ? artist.Rating : 0, // Only Artists have ratings
                Password = request.Password // Store the entered password for the profile page
            });

        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public double? Rating { get; set; } // Nullable for Guests
    }
}
