using Api.Models;
using Api.Models.Models;
using Api.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Api.Services
{
    public class StationService : IStationService
    {
        private readonly CityBikesDBContext _dBContext;
        private readonly Utilities _utilities = new Utilities();

        public StationService(CityBikesDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public async Task<StationInfo> GetStationInfo(int id, int month)
        {
            if (month == 0)
            {
                return await _dBContext.StationsInfo
                    .FromSqlRaw($"SELECT [Name], [Osoite] as [Adress], x, y, " +
                     $"CountOfDepartures = (SELECT Count([Departure_station_id]) as CountOfDepartures FROM(SELECT[Departure_station_id] FROM[2021-07] UNION ALL SELECT[Departure_station_id] FROM[2021-06] UNION ALL SELECT[Departure_station_id] FROM[2021-05]) as tem  WHERE[Departure_station_id] = @id), " +
		             $"CountOfReturns = (SELECT Count([Return_station_id]) as CountOfReturns FROM(SELECT[Return_station_id] FROM[2021-07] UNION ALL SELECT[Return_station_id] FROM[2021-06] UNION ALL SELECT[Return_station_id] FROM[2021-05]) as tem  WHERE[Return_station_id] = @id), " +
		             $"AverageDistanseDepartures = (SELECT AVG(All[Covered_distance]) as AverageDistanseDepartures FROM(SELECT[Covered_distance], [Departure_station_id] FROM[2021-07] UNION ALL SELECT[Covered_distance], [Departure_station_id] FROM[2021-06] UNION ALL SELECT[Covered_distance], [Departure_station_id] FROM[2021-05]) as tem  WHERE[Departure_station_id] = @id),  " +
		             $"AverageDistanseReturns = (SELECT AVG(All[Covered_distance]) as AverageDistanseReturns FROM(SELECT[Covered_distance], [Return_station_id] FROM[2021-07] UNION ALL SELECT[Covered_distance], [Return_station_id] FROM[2021-06] UNION ALL SELECT[Covered_distance], [Return_station_id] FROM[2021-05]) as tem  WHERE[Return_station_id] = @id), " +
		             $"PopularReturns = (SELECT TOP(5)[Return_station_name] + ',' FROM(SELECT[Departure_station_id], [Return_station_name] FROM[2021-07] UNION ALL SELECT[Departure_station_id], [Return_station_name] FROM[2021-06] UNION ALL SELECT[Departure_station_id], [Return_station_name] FROM[2021-05]) as tem  WHERE[Departure_station_id] = @id GROUP BY[Return_station_name] ORDER BY COUNT([Return_station_name]) DESC FOR XML PATH('')),  " +
		             $"PopularDepartures = (SELECT TOP(5)[Departure_station_name] + ',' FROM(SELECT[Return_station_id], [Departure_station_name] FROM[2021-07] UNION ALL SELECT[Return_station_id], [Departure_station_name] FROM[2021-06] UNION ALL SELECT[Return_station_id], [Departure_station_name] FROM[2021-05]) as tem  WHERE[Return_station_id] = @id GROUP BY[Departure_station_name] ORDER BY COUNT([Departure_station_name]) DESC FOR XML PATH(''))  " +
		             $"FROM[kaupunkipyoraasemat] Where[ID] = @id"
                    , new SqlParameter("@id", id)).FirstOrDefaultAsync();
                //.FromSqlRaw($"SELECT  [Name], [Osoite] as [Adress], x, y  FROM [kaupunkipyoraasemat] Where [ID] = @id " //+
                //$"SELECT Count([Departure_station_id])  as CountOfDepartures FROM (SELECT [Departure_station_id] FROM [2021-07] UNION ALL SELECT [Departure_station_id] FROM [2021-06] UNION ALL SELECT [Departure_station_id] FROM [2021-05]) as tem WHERE[Departure_station_id] = @id " +
                //$"SELECT Count([Return_station_id])  as CountOfDepartures FROM (SELECT [Return_station_id] FROM [2021-07] UNION ALL SELECT [Return_station_id] FROM [2021-06] UNION ALL SELECT [Return_station_id] FROM [2021-05]) as tem WHERE[Return_station_id] = @id " +
                //$"SELECT AVG (All [Covered_distance]) as AverageDistanseDepartures FROM (SELECT [Covered_distance], [Departure_station_id] FROM [2021-07] UNION ALL SELECT [Covered_distance], [Departure_station_id] FROM [2021-06] UNION ALL SELECT [Covered_distance], [Departure_station_id] FROM [2021-05]) as tem WHERE[Departure_station_id] = @id " +
                //$"SELECT AVG (All [Covered_distance]) as AverageDistanseReturns FROM  (SELECT [Covered_distance], [Return_station_id] FROM [2021-07] UNION ALL SELECT [Covered_distance], [Return_station_id] FROM [2021-06] UNION ALL SELECT [Covered_distance], [Return_station_id] FROM [2021-05]) as tem WHERE[Return_station_id] = @id " +
                //$"SELECT TOP(5) [Return_station_name] as PopularReturns FROM (SELECT [Departure_station_id], [Return_station_name] FROM [2021-07] UNION ALL SELECT [Departure_station_id], [Return_station_name] FROM [2021-06] UNION ALL SELECT [Departure_station_id], [Return_station_name] FROM [2021-05])as tem WHERE[Departure_station_id] = 2 GROUP BY [Return_station_name] ORDER BY COUNT([Return_station_name]) DESC " +
                //$"SELECT TOP(5) [Departure_station_name] as PopularDepartures FROM (SELECT [Departure_station_id], [Departure_station_name] FROM [2021-07] UNION ALL SELECT [Departure_station_id], [Departure_station_name] FROM [2021-06] UNION ALL SELECT [Departure_station_id], [Departure_station_name] FROM [2021-05])as tem WHERE[Departure_station_id] = 2 GROUP BY [Departure_station_name] ORDER BY COUNT([Departure_station_name]) DESC"
                //, new SqlParameter("@id", id)).FirstOrDefaultAsync();
            }


            return await _dBContext.StationsInfo
                .FromSqlRaw($"SELECT [Name], [Osoite] as [Adress], x, y, " +
                $"CountOfDepartures = (SELECT Count([Departure_station_id]) as CountOfDepartures FROM [2021-05]  WHERE [Departure_station_id] = @id), " +
                $"CountOfReturns=(SELECT Count([Return_station_id]) as CountOfReturns FROM [2021-05]  WHERE [Return_station_id] = @id),  " +
                $"AverageDistanseDepartures=(SELECT AVG (All [Covered_distance]) as AverageDistanseDepartures FROM [2021-05]  WHERE [Departure_station_id] = @id),  " +
                $"AverageDistanseReturns=(SELECT AVG (All [Covered_distance]) as AverageDistanseReturns FROM [2021-05]  WHERE [Return_station_id] = @id), " +
                $"PopularReturns=(SELECT TOP(5) [Return_station_name]+',' FROM [2021-05]  WHERE [Departure_station_id] = @id GROUP BY [Return_station_name] ORDER BY COUNT([Return_station_name]) DESC FOR XML PATH('')),  " +
                $"PopularDepartures=(SELECT TOP(5) [Departure_station_name]+','FROM [2021-05]  WHERE [Return_station_id] = @id GROUP BY [Departure_station_name] ORDER BY COUNT([Departure_station_name]) DESC FOR XML PATH(''))  " +
                $"FROM [kaupunkipyoraasemat] Where [ID] = @id"
                , new SqlParameter("@id", id)).FirstOrDefaultAsync();


        }

        public async Task<IEnumerable<Station>> GetStations(int offset, int limit, string order, string search, bool descending)
        {
            string ascOrDesc = _utilities.AscOrDesc(descending);

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
