using AutoMapper;
using HotelListing.Api.Data;
using HotelListing.Api.Data.Dtos.HotelDtos;
using HotelListing.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        //private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHotelRepository _hotelRepository;

        public HotelController(IHotelRepository hotelRepository, IMapper mapper)
        {
            _mapper = mapper;
            _hotelRepository = hotelRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotelResult = await _hotelRepository.GetAsync(id);

            if (hotelResult == null)
                return NotFound();
            else
            {
                var hotel = _mapper.Map<HotelDto>(hotelResult);
                return Ok(hotel);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotelResults = await _hotelRepository.GetAllAsync();
            var result = _mapper.Map <List<HotelDto>>(hotelResults);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> AddHotel(CreateHotelDto createHotel)
        {
            var hotel = _mapper.Map<Hotel>(createHotel);
            await _hotelRepository.AddAsync(hotel);
            
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, HotelDto updateHotel)
        { 
            if(id != updateHotel.Id)
                return BadRequest("The id didn't match.");

            var hotel = await _hotelRepository.GetAsync(id);
            _mapper.Map(updateHotel, hotel);

            try 
            { 
                await _hotelRepository.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExits(id))
                    return NotFound();     
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var result = await _hotelRepository.GetAsync(id);
            if (result == null)
                return NotFound();

            await _hotelRepository.DeleteAsync(id);
            
            return NoContent();
        }
        private async Task<bool> HotelExits(int id)
        { 
            return await _hotelRepository.Exist(id);
        }
    }
}
