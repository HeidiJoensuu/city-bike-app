using Api.Models.Models;
using System;

namespace Api.Services
{
    public interface IJourneyService
    {
        Task<IEnumerable<JourneyAbstract>> GetJourneys(int offset, int limit,string order, string search, bool descending, int month);
        Task<JourneyAbstract> CreateJourney(JourneyAbstract journey);
    }
}
