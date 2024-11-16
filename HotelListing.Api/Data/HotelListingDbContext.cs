using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext (DbContextOptions options) : base(options) 
        {
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country 
                { 
                    Id = 1,
                    Name="Ethiopia",
                    ShortName="ET"
                },
                new Country
                { 
                    Id=2,
                    Name ="Kenya",
                    ShortName="KN"
                },
                new Country 
                { 
                    Id=3,
                    Name="United Arab Emirates",
                    ShortName="UAE"
                }
            );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                { 
                    Id=1,
                    Name="Ilili Hotel",
                    Rating=5,
                    CountryId=1
                },
                new Hotel
                { 
                    Id=2,
                    Name="Radison Blue",
                    Rating=4,
                    CountryId=2
                },
                new Hotel
                { 
                    Id=3,
                    Name="Hilton Hotel",
                    Rating=5,
                    CountryId=3
                }
            );
        }
    }
}
