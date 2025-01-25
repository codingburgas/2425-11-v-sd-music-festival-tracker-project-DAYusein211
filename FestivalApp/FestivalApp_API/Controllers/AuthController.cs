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

        [HttpPost("login")] // ✅ Make sure this is correctly defined
        public async Task<ActionResult<UserInfo>> Login([FromBody] LoginRequest request)
        {
            Console.WriteLine($"Login attempt: {request.Email}");

            var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Email == request.Email);
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.Email == request.Email);

            var user = guest ?? (object)artist; // Check both tables

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return Unauthorized("Invalid email or password.");
            }

            var storedPasswordHash = user is Guest g ? g.PasswordHash : ((Artist)user).PasswordHash;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, storedPasswordHash))
            {
                Console.WriteLine("Incorrect password.");
                return Unauthorized("Invalid email or password.");
            }

            var response = new UserInfo
            {
                Id = user is Guest ? ((Guest)user).Id : ((Artist)user).Id,
                FirstName = user is Guest ? ((Guest)user).FirstName : ((Artist)user).FirstName,
                LastName = user is Guest ? ((Guest)user).LastName : ((Artist)user).LastName,
                Email = request.Email,
                Role = user is Guest ? "Guest" : "Artist",
                Rating = user is Artist ? ((Artist)user).Rating : null // Only Artists have a rating
            };

            Console.WriteLine($"Login successful: {response.Email}");
            return Ok(response);
        }
    }
}
