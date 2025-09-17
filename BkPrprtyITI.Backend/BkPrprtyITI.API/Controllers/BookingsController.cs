using BkPrprtyITI.Core.DTOs;
using BkPrprtyITI.Core.Models;
using BkPrprtyITI.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BkPrprtyITI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            var bookings = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Property)
                .Select(b => new BookingDto(
                    b.Id,
                    b.UserId,
                    b.User.FullName,
                    b.PropertyId,
                    b.Property.Title,
                    b.StartDate,
                    b.EndDate
                ))
                .ToListAsync();

            return Ok(bookings);
        }

        // GET: api/bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Property)
                .Where(b => b.Id == id)
                .Select(b => new BookingDto(
                    b.Id,
                    b.UserId,
                    b.User.FullName,
                    b.PropertyId,
                    b.Property.Title,
                    b.StartDate,
                    b.EndDate
                ))
                .FirstOrDefaultAsync();

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<ActionResult<BookingDto>> PostBooking(CreateBookingDto dto)
        {
            // validate User exists
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
                return BadRequest($"User with Id {dto.UserId} not found.");

            // validate Property exists
            var property = await _context.Properties.FindAsync(dto.PropertyId);
            if (property == null)
                return BadRequest($"Property with Id {dto.PropertyId} not found.");

            // create booking
            var booking = new Booking
            {
                UserId = dto.UserId,
                PropertyId = dto.PropertyId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            var createdBooking = new BookingDto(
                booking.Id,
                booking.UserId,
                user.FullName,
                booking.PropertyId,
                property.Title,
                booking.StartDate,
                booking.EndDate
            );

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, createdBooking);
        }


        // DELETE: api/bookings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookingDto>> DeleteBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Property)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound();

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            var deletedBooking = new BookingDto(
                booking.Id,
                booking.UserId,
                booking.User.FullName,
                booking.PropertyId,
                booking.Property.Title,
                booking.StartDate,
                booking.EndDate
            );

            return Ok(deletedBooking);
        }

        // PUT: api/bookings/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BookingDto>> PutBooking(int id, CreateBookingDto dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            // validate User exists
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
                return BadRequest($"User with Id {dto.UserId} not found.");

            // validate Property exists
            var property = await _context.Properties.FindAsync(dto.PropertyId);
            if (property == null)
                return BadRequest($"Property with Id {dto.PropertyId} not found.");

            // update booking
            booking.UserId = dto.UserId;
            booking.PropertyId = dto.PropertyId;
            booking.StartDate = dto.StartDate;
            booking.EndDate = dto.EndDate;

            await _context.SaveChangesAsync();

            var updatedBooking = new BookingDto(
                booking.Id,
                booking.UserId,
                user.FullName,
                booking.PropertyId,
                property.Title,
                booking.StartDate,
                booking.EndDate
            );

            return Ok(updatedBooking);
        }

    }
}
