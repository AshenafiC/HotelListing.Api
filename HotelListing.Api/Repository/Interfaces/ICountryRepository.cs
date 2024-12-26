using HotelListing.Api.Data;

namespace HotelListing.Api.Repository.Interfaces
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country> GetDetails(int id);
    }
}
