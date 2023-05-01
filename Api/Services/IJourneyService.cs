using Api.Models.Models;
using System;

namespace Api.Services
{
    public interface IJourneyService
    {
        Task<IEnumerable<Journey>> GetJourneys(int offset, int limit,string order, string search, bool descending, int month);
        Task<Journey> CreateJourney(Journey journey);
    }
}
