using HotelListing.Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly HotelListingDbContext _context;

        public HotelController(HotelListingDbContext context)
        {
            _context = context; 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotelResult = await _context.Hotels.FindAsync(id);

            if(hotelResult == null)
                return NotFound();
            else
                return Ok(hotelResult);
        }
        [HttpGet]
        public async Task<ActionResult<Hotel>> GetHotels()
        {
            var hotelResults = await _context.Hotels.ToListAsync();

            return Ok(hotelResults);
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> AddHotel(Hotel hotel)
        {
             _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, Hotel hotel)
        { 
            if(id != hotel.Id)
                return BadRequest("The id didn't match.");

            _context.Entry(hotel).State = EntityState.Modified;

            try 
            { 
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExits(id))
                    return NotFound();     
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var result = await _context.Hotels.FindAsync(id);
            if (result == null)
                return NotFound();

                _context.Hotels.Remove(result);
                await _context.SaveChangesAsync();
            
            return NoContent();
        }
        private bool HotelExits(int id)
        { 
            return _context.Hotels.Any(h => h.Id == id);
        }
    }
}
