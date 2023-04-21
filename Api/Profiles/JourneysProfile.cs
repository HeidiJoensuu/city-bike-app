using Api.Models.Models.DTOs;
using Api.Models.Models;
using AutoMapper;

namespace Api.Profiles
{
    public class JourneysProfile: Profile
    {
        public JourneysProfile() 
        {
            CreateMap<Journey, ModifiedJourney>()
                .ForMember(dto => dto.Covered_distance, options =>
                options.MapFrom(j => Math.Round((float)j.Covered_distance / 1000, 2)))
                .ForMember(dto => dto.Duration, options => 
                options.MapFrom(j => j.Duration/60));
        }
    }
}
