using Api.Models.Models;
using System;

namespace Api.Services
{
    public interface IJourneyService
    {
        Task<IEnumerable<JourneyAbstract>> GetJourneys(int offset, int limit,string order, string search, bool descending, int month, string departure, string returnTime, double distanceMin, double distanceMax, int durationMin, int durationMax);
        Task<JourneyAbstract> CreateJourney(JourneyAbstract journey);

        Task<int> GetJourneysCount(string search, int month, string departure, string returnTime, double distanceMin, double distanceMax, int durationMin, int durationMax);
    }
}
