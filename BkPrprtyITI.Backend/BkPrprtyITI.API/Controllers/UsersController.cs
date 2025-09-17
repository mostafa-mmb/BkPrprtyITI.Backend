using BkPrprtyITI.Core.DTOs;
using BkPrprtyITI.Core.Models;
using BkPrprtyITI.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BkPrprtyITI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new UserDto(
                    u.Id,
                    u.FullName,
                    u.Email
                ))
                .ToListAsync();

            return Ok(users);
        }



        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserDto(
                    u.Id,
                    u.FullName,
                    u.Email
                ))
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            return Ok(user);
        }


        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(CreateUserDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var createdUser = new UserDto(user.Id, user.FullName, user.Email);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, createdUser);
        }


        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> PutUser(int id, CreateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.FullName = dto.FullName;
            user.Email = dto.Email;

            await _context.SaveChangesAsync();

            var updatedUser = new UserDto(user.Id, user.FullName, user.Email);

            return Ok(updatedUser);
        }


        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDto>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            var deletedUser = new UserDto(user.Id, user.FullName, user.Email);

            return Ok(deletedUser);
        }


    }
}
