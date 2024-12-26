using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Api.Data.Dtos.HotelDtos
{
    public class GetHotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; }
    }
}
