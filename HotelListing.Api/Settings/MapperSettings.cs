using AutoMapper;
using HotelListing.Api.Data;
using HotelListing.Api.Data.Dtos.CountryDto;
using HotelListing.Api.Data.Dtos.CountryDtos;
using HotelListing.Api.Data.Dtos.HotelDtos;

namespace HotelListing.Api.Settings
{
    public class MapperSettings : Profile
    {
        public MapperSettings()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();


            CreateMap<Hotel, GetHotelDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>().ReverseMap();
        }
    }
}
