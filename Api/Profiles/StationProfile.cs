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
            CreateMap<NewStationDto, Station>().ReverseMap();
            CreateMap<Station, StationDto>().ReverseMap();
            CreateMap<Station, StationShortDto>().ReverseMap();
        }
    }
}
