using Api.Models;
using Api.Models.Models;
using Api.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

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

        public async Task<Journey> CreateJourney(Journey journey)
        {
            var jtn = _dBContext.Journeys.Where(j => j
                .Departure_station_id == journey.Departure_station_id)
                .Where(j => j.Departure_station_name == journey.Departure_station_name)
                .Where(j => j.Return_station_id == journey.Return_station_id)
                .Where(j => j.Return_station_name == journey.Return_station_name).FirstOrDefault();
            
            if (jtn == null)
            {
                throw new Exception();
            }

            var month = _utilities.WantedMonts(journey.Departure.Month);

            await _dBContext.Database
                .ExecuteSqlRawAsync($"INSERT INTO {month} (Departure, [Return], Departure_station_id, Departure_station_name, Return_station_id, Return_station_name, Covered_distance, Duration) " +
                $"VALUES (@Departure, @Return, @Departure_station_id, @Departure_station_name, @Return_station_id, @Return_station_name, @Covered_distance, @Duration)"
                , new SqlParameter("@Departure", journey.Departure)
                , new SqlParameter("@Return", journey.Return)
                , new SqlParameter("@Departure_station_id", journey.Departure_station_id)
                , new SqlParameter("@Departure_station_name", journey.Departure_station_name)
                , new SqlParameter("@Return_station_id", journey.Return_station_id)
                , new SqlParameter("@Return_station_name", journey.Return_station_name)
                , new SqlParameter("@Covered_distance", (int)journey.Covered_distance)
                , new SqlParameter("@Duration", (long)journey.Duration));

            return journey;
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
