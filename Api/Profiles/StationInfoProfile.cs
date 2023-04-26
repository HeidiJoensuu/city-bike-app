using Api.Models.DTOs;
using Api.Models.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Api.Profiles
{
    public class StationInfoProfile : Profile
    {
        public StationInfoProfile() 
        {
            CreateMap<StationInfo, StationInfoDTO>()
                .ForMember(dto => dto.x, options =>
                options.MapFrom(s => float.Parse(s.x, CultureInfo.InvariantCulture.NumberFormat)))
                .ForMember(dto => dto.y, options =>
                options.MapFrom(s => float.Parse(s.y, CultureInfo.InvariantCulture.NumberFormat)))
                .ForMember(dto => dto.AverageDistanseDepartures, options =>
                options.MapFrom(s => Math.Round((float)s.AverageDistanseDepartures / 1000, 2)))
                .ForMember(dto => dto.AverageDistanseReturns, options =>
                options.MapFrom(s => Math.Round((float)s.AverageDistanseReturns / 1000, 2)))
                .ForMember(dto => dto.PopularReturns, options => 
                options.MapFrom(s => s.PopularReturns.ToString().TrimEnd(',').Split(',', System.StringSplitOptions.None)))
                .ForMember(dto => dto.PopularDepartures, options =>
                options.MapFrom(s => s.PopularDepartures.ToString().TrimEnd(',').Split(',', System.StringSplitOptions.None)));
        }
    }
}
