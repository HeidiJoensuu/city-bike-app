using Api.Models.DTOs;
using Api.Models.Models;
using AutoMapper;

namespace Api.Profiles
{
    public class StationProfile: Profile
    {
        public StationProfile() 
        { 
            CreateMap<NewStationDto, Station>().ReverseMap();
        }
    }
}
