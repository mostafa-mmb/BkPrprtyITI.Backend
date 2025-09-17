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
    public class PropertiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PropertiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/properties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetProperties()
        {
            var properties = await _context.Properties
                .Select(p => new PropertyDto(
                    p.Id,
                    p.Title,
                    p.Description,
                    p.PricePerNight,
                    p.Location
                ))
                .ToListAsync();

            return Ok(properties);
        }

        // GET: api/properties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDto>> GetProperty(int id)
        {
            var property = await _context.Properties
                .Where(p => p.Id == id)
                .Select(p => new PropertyDto(
                    p.Id,
                    p.Title,
                    p.Description,
                    p.PricePerNight,
                    p.Location
                ))
                .FirstOrDefaultAsync();

            if (property == null)
                return NotFound();

            return Ok(property);
        }



        // POST: api/properties
        [HttpPost]
        public async Task<ActionResult<PropertyDto>> PostProperty(CreatePropertyDto dto)
        {
            var property = new Property
            {
                Title = dto.Title,
                Description = dto.Description,
                PricePerNight = dto.PricePerNight,
                Location = dto.Location
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            var createdProperty = new PropertyDto(
                property.Id,
                property.Title,
                property.Description,
                property.PricePerNight,
                property.Location
            );

            return CreatedAtAction(nameof(GetProperty), new { id = property.Id }, createdProperty);
        }

        // PUT: api/properties/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PropertyDto>> PutProperty(int id, CreatePropertyDto dto)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
                return NotFound();

            property.Title = dto.Title;
            property.Description = dto.Description;
            property.PricePerNight = dto.PricePerNight;
            property.Location = dto.Location;

            await _context.SaveChangesAsync();

            var updatedProperty = new PropertyDto(
                property.Id,
                property.Title,
                property.Description,
                property.PricePerNight,
                property.Location
            );

            return Ok(updatedProperty);
        }

        // DELETE: api/properties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PropertyDto>> DeleteProperty(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
                return NotFound();

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            var deletedProperty = new PropertyDto(
                property.Id,
                property.Title,
                property.Description,
                property.PricePerNight,
                property.Location
            );

            return Ok(deletedProperty);
        }

    }
}
