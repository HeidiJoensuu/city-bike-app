using Api.Models.DTOs;
using Api.Models.Models;
using AutoMapper;
using System.Globalization;

namespace Api.Profiles
{
    public class StationProfile: Profile
    {
        public StationProfile() 
        { 
            CreateMap<NewStationDto, Station>()
                .ForMember(dto => dto.x, options => 
                options.MapFrom(station => station.x.ToString()))
                .ForMember(dto => dto.y , options =>
                options.MapFrom(station => station.y.ToString()));
            CreateMap<Station, StationDto>()
                .ForMember(dto => dto.x, options =>
                options.MapFrom(station => double.Parse(station.x, CultureInfo.InvariantCulture.NumberFormat)))
                .ForMember(dto => dto.y, options =>
                options.MapFrom(station => double.Parse(station.y, CultureInfo.InvariantCulture.NumberFormat)));
        }
    }
}
