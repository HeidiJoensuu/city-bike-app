using Api.Models.Models;
using System;

namespace Api.Services
{
    public interface IJourneyService
    {
        /// <summary>
        /// Gets all journeys depending on the parameters
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="order"></param>
        /// <param name="search"></param>
        /// <param name="descending"></param>
        /// <param name="month"></param>
        /// <param name="departure"></param>
        /// <param name="returnTime"></param>
        /// <param name="distanceMin"></param>
        /// <param name="distanceMax"></param>
        /// <param name="durationMin"></param>
        /// <param name="durationMax"></param>
        /// <returns></returns>
        Task<IEnumerable<JourneyAbstract>> GetJourneys(int offset, int limit,string order, string search, bool descending, int month, string departure, string returnTime, double distanceMin, double distanceMax, int durationMin, int durationMax);

        /// <summary>
        /// Posts new journey into database
        /// </summary>
        /// <param name="journey">New journey - journey</param>
        /// <returns></returns>
        Task<JourneyAbstract> CreateJourney(JourneyAbstract journey);

        /// <summary>
        /// Gets count of all journeys depending on parameters
        /// </summary>
        /// <param name="search"></param>
        /// <param name="month"></param>
        /// <param name="departure"></param>
        /// <param name="returnTime"></param>
        /// <param name="distanceMin"></param>
        /// <param name="distanceMax"></param>
        /// <param name="durationMin"></param>
        /// <param name="durationMax"></param>
        /// <returns></returns>
        Task<int> GetJourneysCount(string search, int month, string departure, string returnTime, double distanceMin, double distanceMax, int durationMin, int durationMax);
    }
}
