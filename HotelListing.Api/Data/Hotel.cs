using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Api.Data
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; }

        [ForeignKey(nameof(CountryId))]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
