using HotelListing.Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly HotelListingDbContext _context;

        public CountryController(HotelListingDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var countryResult = await _context.Countries.FindAsync(id);
            if (countryResult == null)
                return NotFound();
            else
                return Ok(countryResult);
        }

        [HttpGet]
        public async Task<ActionResult<Country>> GetCountries()
        {
            var countriesResult = await _context.Countries.ToListAsync();

            return Ok(countriesResult);
        }

        [HttpPost]
        public async Task<ActionResult<Country>> AddCountry([FromBody] Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, Country country)
        {
            if (id != country.Id)
                return BadRequest();

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var result = await _context.Countries.FindAsync(id);

            if (result == null)
                return NotFound();

            _context.Countries.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        { 
            return _context.Countries.Any(c => c.Id == id);
        }
    }  
}
