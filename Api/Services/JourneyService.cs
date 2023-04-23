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

        public async Task<IEnumerable<Journey>> GetJourneys(int offset, int limit, string order, string search, bool descending, int month)
        {

            string ascOrDesc = "ASC";
            if (descending)
            {
                ascOrDesc = "DESC";
            }

            string wantedMonth = month switch
            {
                5 => "[2021-05] ",
                6 => "[2021-06] ",
                _ => "[2021-07] ",
            };

            var answer = await _dBContext.Journeys
                .FromSqlRaw($"SELECT * FROM  " + wantedMonth +
                $"WHERE (LOWER([Departure_station_name]) LIKE '%' + @search +'%' OR LOWER([Return_station_name]) LIKE '%' + @search +'%') " +
                $"ORDER BY " +
                $"(CASE WHEN @order = 'Departure' THEN [Departure] END) " + ascOrDesc + ", " +
                $"(CASE WHEN @order = 'Return' THEN [Return] END) " + ascOrDesc + ", " +
                $"(CASE WHEN @order = 'Departure_station_name' THEN [Departure_station_name] END) " + ascOrDesc + ", " +
                $"(CASE WHEN @order = 'Return_station_name' THEN [Return_station_name] END) " + ascOrDesc + ", " +
                $"(CASE WHEN @order = 'Covered_distance' THEN [Covered_distance] END) " + ascOrDesc + ", " +
                $"(CASE WHEN @order = 'Duration' THEN [Duration] END) " + ascOrDesc + " " +
                $"OFFSET @offset ROWS " +
                $"FETCH NEXT @limit ROWS ONLY"
                , new SqlParameter("@search", search.ToLower()), new SqlParameter("@order", order), new SqlParameter("@order", ascOrDesc), new SqlParameter("@offset", offset), new SqlParameter("@limit", limit))
                .ToListAsync();

            return answer;

        }
    }
}
