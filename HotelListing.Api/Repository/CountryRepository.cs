using HotelListing.Api.Data;
using HotelListing.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly HotelListingDbContext _context;
        public CountryRepository(HotelListingDbContext context) : base(context)
        {
               _context = context;
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _context.Countries.Include(c => c.Hotels)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
