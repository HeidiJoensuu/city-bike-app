using Api.Models;
using Api.Models.Models;
using Api.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly CityBikesDBContext _dBContext;
        
        private readonly Utilities _utilities = new Utilities();

        public JourneyService(CityBikesDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<IEnumerable<Journey>> GetJourneys(int offset, int limit, string order, string search, bool descending, int month)
        {

            string ascOrDesc = _utilities.AscOrDesc(descending);
            string wantedMonth = _utilities.WantedMonts(month);

            return await _dBContext.Journeys
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
                , new SqlParameter("@search", search.ToLower()), new SqlParameter("@order", order), new SqlParameter("@offset", offset), new SqlParameter("@limit", limit))
                .ToListAsync();
        }
    }
}
