using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FestivalApp_DAL.Contexts;
using FestivalApp_DAL.Models;

namespace FestivalApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly FestivalDbContext _context;

        public TicketsController(FestivalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();
            return ticket;
        }
       
        [HttpGet("user/{guestId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetUserTickets(int guestId)
        {
            var tickets = await _context.Tickets
                .Where(t => t.GuestId == guestId)
                .Join(_context.Festivals,
                    ticket => ticket.FestivalId,
                    festival => festival.Id,
                    (ticket, festival) => new
                    {
                        ticket.FestivalId,
                        FestivalName = festival.Name,
                        FestivalLocation = festival.Location,
                        FestivalDate = festival.Date,
                        ArtistId = festival.ArtistId,
                        ArtistName = _context.Artists
                            .Where(a => a.Id == festival.ArtistId)
                            .Select(a => a.FirstName + " " + a.LastName)
                            .FirstOrDefault(),
                        ArtistRating = _context.Artists
                            .Where(a => a.Id == festival.ArtistId)
                            .Select(a => a.Rating)
                            .FirstOrDefault()
                    })
                .ToListAsync();

            return Ok(tickets);
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket([FromBody] Ticket ticket)
        {
            // ✅ Ensure the Guest exists
            var guestExists = await _context.Guests.AnyAsync(g => g.Id == ticket.GuestId);
            if (!guestExists)
            {
                return BadRequest("Guest not found.");
            }

            // ✅ Ensure the Festival exists
            var festivalExists = await _context.Festivals.AnyAsync(f => f.Id == ticket.FestivalId);
            if (!festivalExists)
            {
                return BadRequest("Festival not found.");
            }

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
