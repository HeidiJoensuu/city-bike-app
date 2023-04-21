using Api.Models;
using Api.Models.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly CityBikesDBContext _dBContext;

        public JourneyService(CityBikesDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<IEnumerable<Journey>> GetJourneys(int offset, int limit, string order, string search, bool descending)
        {

            var answer =  await _dBContext.Journeys
                .FromSqlRaw($"SELECT * FROM [2021-07] " +
                $"WHERE (LOWER([Departure_station_name]) LIKE '%' + @search +'%' OR LOWER([Return_station_name]) LIKE '%' + @search +'%') " +
                $"ORDER BY CASE @order " +
                $"WHEN 'Departure' THEN [Departure] " +
                $"WHEN 'Return' THEN [Return] " +
                $"WHEN 'Departure_station_name' THEN [Departure_station_name] " +
                $"WHEN 'Return_station_name' THEN [Return_station_name] " +
                $"WHEN 'Covered_distance' THEN [Covered_distance] " +
                $"WHEN 'Duration' THEN [Duration] " +
                $"END " +
                $"OFFSET @offset ROWS " +
                $"FETCH NEXT @limit ROWS ONLY"
                , new SqlParameter("@search", search.ToLower()), new SqlParameter("@order", order), new SqlParameter("@offset", offset), new SqlParameter("@limit", limit)).ToListAsync();

            return answer;
            

        }
    }
}
