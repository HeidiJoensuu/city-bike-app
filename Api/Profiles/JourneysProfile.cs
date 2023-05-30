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
                options.MapFrom(journey => Math.Round((float)journey.covered_distance_m / 1000, 2)))
                .ForMember(dto => dto.Duration, options => 
                options.MapFrom(journey => journey.duration_sec >= 3600 
                    ? journey.duration_sec >= 86400
                        ? string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", TimeSpan.FromSeconds(journey.duration_sec).Days, TimeSpan.FromSeconds(journey.duration_sec).Hours, TimeSpan.FromSeconds(journey.duration_sec).Minutes, TimeSpan.FromSeconds(journey.duration_sec).Seconds)
                        : string.Format("{0}:{1:D2}:{2:D2}", TimeSpan.FromSeconds(journey.duration_sec).Hours, TimeSpan.FromSeconds(journey.duration_sec).Minutes, TimeSpan.FromSeconds(journey.duration_sec).Seconds)
                    : string.Format("{0}:{1:D2}", TimeSpan.FromSeconds(journey.duration_sec).Minutes, TimeSpan.FromSeconds(journey.duration_sec).Seconds)
                ));
        }
    }
}
