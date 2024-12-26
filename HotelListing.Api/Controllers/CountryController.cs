using AutoMapper;
using HotelListing.Api.Data;
using HotelListing.Api.Data.Dtos.CountryDto;
using HotelListing.Api.Data.Dtos.CountryDtos;
using HotelListing.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var countryResult = await _countryRepository.GetDetails(id);
            if (countryResult == null)
                return NotFound();

            var result = _mapper.Map<CountryDto>(countryResult);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countriesResult = await _countryRepository.GetAllAsync();

            var result = _mapper.Map<List<GetCountryDto>>(countriesResult);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Country>> AddCountry(CreateCountryDto createCountry)
        {
            var country = _mapper.Map<Country>(createCountry);
            await _countryRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, UpdateCountryDto updateCountry)
        {
            if (id != updateCountry.Id)
                return BadRequest();
             var country = await _countryRepository.GetAsync(id);

            if (country == null)
                return NotFound();

            _mapper.Map(updateCountry, country);

            try
            {
                await _countryRepository.UpdateAsync(country);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var result = await _countryRepository.GetAsync(id);

            if (result == null)
                return NotFound();

            await _countryRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        { 
            return await _countryRepository.Exist(id);
        }
    }  
}
