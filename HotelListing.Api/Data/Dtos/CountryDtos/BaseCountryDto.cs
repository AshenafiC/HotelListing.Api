using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Data.Dtos.CountryDtos
{
    public abstract class BaseCountryDto
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
