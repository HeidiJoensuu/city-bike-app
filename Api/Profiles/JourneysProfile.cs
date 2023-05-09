using Api.Models.Models;
using AutoMapper;
using Api.Models.DTOs;

namespace Api.Profiles
{
    public class JourneysProfile: Profile
    {
        public JourneysProfile() 
        {
            CreateMap<JourneyAbstract, ModifiedJourneyDto>()
                .ForMember(dto => dto.Covered_distance, options =>
                options.MapFrom(journey => Math.Round((float)journey.Covered_distance_m / 1000, 2)))
                .ForMember(dto => dto.Duration, options => 
                options.MapFrom(journey => journey.Duration_sec/60));
        }
    }
}
