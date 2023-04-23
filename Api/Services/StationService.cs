using Api.Models;
using Api.Models.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class StationService : IStationService
    {
        private readonly CityBikesDBContext _dBContext;

        public StationService(CityBikesDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public Task<Station> GetStationInfo(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Station>> GetStations(int offset, int limit, string order, string search, bool descending)
        {
            string ascOrDesc = "ASC";
            if (descending)
            {
                ascOrDesc = "DESC";
            }

            return await _dBContext.Stations
                .FromSqlRaw($"SELECT [Id], [Name],[Osoite] as [Adress]  FROM [kaupunkipyoraasemat] " +
                $"ORDER BY " +
                $"(CASE WHEN @order = 'Name' then [Name] END) " + ascOrDesc + ", " +
                $"(CASE WHEN @order = 'Adress' then [Osoite] END) " + ascOrDesc + " " +
                $"OFFSET @offset ROWS " +
                $"FETCH NEXT @limit ROWS ONLY"
                , new SqlParameter("@search", search.ToLower()), new SqlParameter("@order", order), new SqlParameter("@offset", offset), new SqlParameter("@limit", limit))
            .ToListAsync();
        }
    }
}
