using Api.Models;
using Api.Models.DTOs;
using Api.Models.Models;
using Api.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<NewStationDto> CreateStation(NewStationDto newStationDto)
        {
            var fid = await _dBContext.StationsInfo.MaxAsync(s => s.Id); //Because FID in Database is the primarykey and therefore auto incremented
            await _dBContext.Database
                .ExecuteSqlRawAsync($"INSERT INTO [kaupunkipyoraasemat] (ID, Nimi, Namn, Name, Osoite, Adress, x, y, Kapasiteet) VALUES (@ID, @Nimi, @Namn, @Name, @Osoite, @Adress, @x, @y, @Kapasiteet)"
                , new SqlParameter("@ID", (fid+1))
                , new SqlParameter("@Nimi", newStationDto.Nimi)
                , new SqlParameter("@Name", newStationDto.Name)
                , new SqlParameter("@Namn", newStationDto.Namn)
                , new SqlParameter("@Osoite", newStationDto.Osoite)
                , new SqlParameter("@Adress", newStationDto.Adress)
                , new SqlParameter("@x", newStationDto.x.ToString())
                , new SqlParameter("@y", newStationDto.y.ToString())
                , new SqlParameter("@Kapasiteet", newStationDto.Kapasiteet));

            return newStationDto;
        }

        public async Task<StationInfo> GetStationInfo(int id, int month)
        {
            if (month == 0)
            {
                return await _dBContext.StationsInfo
                    .FromSqlRaw($"SELECT [Nimi], [Namn], [Name], [Osoite], [Adress], x, y, " +
                     $"CountOfDepartures = (SELECT Count([Departure_station_id]) as CountOfDepartures FROM(SELECT[Departure_station_id] FROM[2021-07] UNION ALL SELECT[Departure_station_id] FROM[2021-06] UNION ALL SELECT[Departure_station_id] FROM[2021-05]) as tem  WHERE[Departure_station_id] = @id), " +
		             $"CountOfReturns = (SELECT Count([Return_station_id]) as CountOfReturns FROM(SELECT[Return_station_id] FROM[2021-07] UNION ALL SELECT[Return_station_id] FROM[2021-06] UNION ALL SELECT[Return_station_id] FROM[2021-05]) as tem  WHERE[Return_station_id] = @id), " +
		             $"AverageDistanseDepartures = (SELECT AVG(All[Covered_distance]) as AverageDistanseDepartures FROM(SELECT[Covered_distance], [Departure_station_id] FROM[2021-07] UNION ALL SELECT[Covered_distance], [Departure_station_id] FROM[2021-06] UNION ALL SELECT[Covered_distance], [Departure_station_id] FROM[2021-05]) as tem  WHERE[Departure_station_id] = @id),  " +
		             $"AverageDistanseReturns = (SELECT AVG(All[Covered_distance]) as AverageDistanseReturns FROM(SELECT[Covered_distance], [Return_station_id] FROM[2021-07] UNION ALL SELECT[Covered_distance], [Return_station_id] FROM[2021-06] UNION ALL SELECT[Covered_distance], [Return_station_id] FROM[2021-05]) as tem  WHERE[Return_station_id] = @id), " +
		             $"PopularReturns = (SELECT TOP(5)[Return_station_name] + ',' FROM(SELECT[Departure_station_id], [Return_station_name] FROM[2021-07] UNION ALL SELECT[Departure_station_id], [Return_station_name] FROM[2021-06] UNION ALL SELECT[Departure_station_id], [Return_station_name] FROM[2021-05]) as tem  WHERE[Departure_station_id] = @id GROUP BY[Return_station_name] ORDER BY COUNT([Return_station_name]) DESC FOR XML PATH('')),  " +
		             $"PopularDepartures = (SELECT TOP(5)[Departure_station_name] + ',' FROM(SELECT[Return_station_id], [Departure_station_name] FROM[2021-07] UNION ALL SELECT[Return_station_id], [Departure_station_name] FROM[2021-06] UNION ALL SELECT[Return_station_id], [Departure_station_name] FROM[2021-05]) as tem  WHERE[Return_station_id] = @id GROUP BY[Departure_station_name] ORDER BY COUNT([Departure_station_name]) DESC FOR XML PATH(''))  " +
		             $"FROM[kaupunkipyoraasemat] Where[ID] = @id"
                    , new SqlParameter("@id", id)).FirstOrDefaultAsync();
            }
            string wantedMonth = _utilities.WantedMonts(month);

            return await _dBContext.StationsInfo
                .FromSqlRaw($"SELECT [Nimi], [Namn], [Name], [Osoite], [Adress], x, y, " +
                $"CountOfDepartures = (SELECT Count([Departure_station_id]) as CountOfDepartures FROM {wantedMonth}  WHERE [Departure_station_id] = @id), " +
                $"CountOfReturns=(SELECT Count([Return_station_id]) as CountOfReturns FROM {wantedMonth}  WHERE [Return_station_id] = @id),  " +
                $"AverageDistanseDepartures=(SELECT AVG (All [Covered_distance]) as AverageDistanseDepartures FROM {wantedMonth}  WHERE [Departure_station_id] = @id),  " +
                $"AverageDistanseReturns=(SELECT AVG (All [Covered_distance]) as AverageDistanseReturns FROM {wantedMonth}  WHERE [Return_station_id] = @id), " +
                $"PopularReturns=(SELECT TOP(5) [Return_station_name]+',' FROM {wantedMonth}  WHERE [Departure_station_id] = @id GROUP BY [Return_station_name] ORDER BY COUNT([Return_station_name]) DESC FOR XML PATH('')),  " +
                $"PopularDepartures=(SELECT TOP(5) [Departure_station_name]+','FROM {wantedMonth}  WHERE [Return_station_id] = @id GROUP BY [Departure_station_name] ORDER BY COUNT([Departure_station_name]) DESC FOR XML PATH(''))  " +
                $"FROM [kaupunkipyoraasemat] Where [ID] = @id"
                , new SqlParameter("@id", id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Station>> GetStations(int offset, int limit, string order, string search, bool descending)
        {
            string ascOrDesc = _utilities.AscOrDesc(descending);

            return await _dBContext.Stations
                .FromSqlRaw($"SELECT [Id],  [Nimi], [Namn], [Name], [Osoite], [Adress] FROM [kaupunkipyoraasemat] " +
                $"ORDER BY " +
                $"(CASE WHEN @order = 'Name' then [Name] END) " + ascOrDesc + ", " +
                $"(CASE WHEN @order = 'Adress' then [Adress] END) " + ascOrDesc + ", " +
                $"(CASE WHEN @order = 'Nimi' then [Nimi] END) " + ascOrDesc + ", " +
                $"(CASE WHEN @order = 'Osoite' then [Osoite] END) " + ascOrDesc + ", " +
                $"(CASE WHEN @order = 'Namn' then [Namn] END) " + ascOrDesc + " " +
                $"OFFSET @offset ROWS " +
                $"FETCH NEXT @limit ROWS ONLY"
                , new SqlParameter("@search", search.ToLower()), new SqlParameter("@order", order), new SqlParameter("@offset", offset), new SqlParameter("@limit", limit))
            .ToListAsync();
        }
    }
}
