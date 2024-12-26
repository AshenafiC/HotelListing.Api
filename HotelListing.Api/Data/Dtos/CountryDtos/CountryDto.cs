using HotelListing.Api.Data.Dtos.HotelDtos;

namespace HotelListing.Api.Data.Dtos.CountryDtos
{
    public class CountryDto : BaseCountryDto
    {
        public int Id { get; set; }
        public List<HotelDto> Hotels { get; set; }
    }
}
