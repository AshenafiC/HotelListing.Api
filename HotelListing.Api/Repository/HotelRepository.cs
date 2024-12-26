using HotelListing.Api.Data;
using HotelListing.Api.Repository.Interfaces;

namespace HotelListing.Api.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelListingDbContext context) : base(context)
        {
        }
    }
}
