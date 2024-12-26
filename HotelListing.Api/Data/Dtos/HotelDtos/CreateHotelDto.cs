using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Data.Dtos.HotelDtos
{
    public class CreateHotelDto
    {
        [Required]
        public string Name { get; set; }
        public decimal? Rating { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CountryId { get; set; }
    }
}
