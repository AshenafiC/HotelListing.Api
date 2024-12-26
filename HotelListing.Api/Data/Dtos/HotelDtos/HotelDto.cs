namespace HotelListing.Api.Data.Dtos.HotelDtos
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; }
        public int CountryId { get; set; }
    }
}
