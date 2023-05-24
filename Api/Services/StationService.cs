using Api.Models;
using Api.Models.DTOs;
using Api.Models.Models;
using Api.Utils;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using AutoMapper.Internal;
using System.Linq;
using AutoMapper;
using Api.Exceptions;
using System.Reflection;

namespace Api.Services
{
    public class StationService : IStationService
    {
        private readonly CityBikesDBContext _dBContext;

        public StationService(CityBikesDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public async Task<Station> CreateStation(Station saveStation)
        {
            foreach (PropertyInfo prop in saveStation.GetType().GetProperties())
            {
                Console.WriteLine($"{prop.Name}: {prop.GetValue(saveStation, null)}");
                if (prop.Name.ToString() != "Id" && prop.Name.ToString() != "may" && prop.Name.ToString() != "june" && prop.Name.ToString() != "july")
                {
                    if (prop.GetValue(saveStation, null) is (object?)"" or (object?)0 or null or (object?)"0")
                    {
                        throw new MissingInputsException(prop.Name.ToString());
                    }
                }
            }
            var dublicate = _dBContext.Stations.Select(station => new { nimi = station.Nimi, namn = station.Namn, name = station.Name })
                .Where(station => station.name == saveStation.Name || station.namn == saveStation.Namn || station.nimi == saveStation.Nimi).FirstOrDefault();
            if (await _dBContext.Stations.AnyAsync(station => station.Nimi == saveStation.Nimi))
            {
                throw new DuplicateException("Nimi", saveStation.Nimi);
            }
            if (await _dBContext.Stations.AnyAsync(station => station.Namn == saveStation.Namn))
            {
                throw new DuplicateException("Nimi", saveStation.Nimi);
            }
            if (await _dBContext.Stations.AnyAsync(station => station.Name == saveStation.Name))
            {
                throw new DuplicateException("Nimi", saveStation.Nimi);
            }

            var id = await _dBContext.Stations.MaxAsync(s => s.Id); //Because FID in Database is the primarykey and therefore auto incremented and Id is not
            saveStation.Id = id+1;

            await _dBContext.Stations.AddAsync(saveStation);
            await _dBContext.SaveChangesAsync();

            return saveStation;
        }

        public async Task<StationInfoDTO> GetStationInfo(int id, int month)
        {
            
            JourneyInfoDto journeyInfo = new JourneyInfoDto();
            List<string> popularReturns = null;
            List<string> popularDepartures = null;
            double? AverageDistanceDepartures = 0;
            double? AverageDistanceReturns = 0;
            int countOfDepartures = 0;
            int countOfReturns = 0;

            switch (month)
            {
                case 5:
                    popularReturns = (await _dBContext.Mays.Where(j => j.Departure_station_id == id).Select(j => j.Return_station_name).ToListAsync()).GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Select(j => j.name).Take(5).ToList();
                    popularDepartures = (await _dBContext.Mays.Where(j => j.Return_station_id == id).Select(j => j.Departure_station_name).ToListAsync()).GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Select(j => j.name).Take(5).ToList();
                    AverageDistanceDepartures = await _dBContext.Mays.Where(j => j.Departure_station_id == id).AverageAsync(x => x.Covered_distance_m);
                    AverageDistanceReturns = await _dBContext.Mays.Where(j => j.Return_station_id == id).AverageAsync(x => x.Covered_distance_m);
                    countOfDepartures = await _dBContext.Mays.Where(j => j.Departure_station_id == id).CountAsync();
                    countOfReturns = await _dBContext.Mays.Where(j => j.Return_station_id == id).CountAsync();
                    break;
                case 6:
                    popularReturns = (await _dBContext.Junes.Where(j => j.Departure_station_id == id).Select(j => j.Return_station_name).ToListAsync()).GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Select(j => j.name).Take(5).ToList();
                    popularDepartures = (await _dBContext.Junes.Where(j => j.Return_station_id == id).Select(j => j.Departure_station_name).ToListAsync()).GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Select(j => j.name).Take(5).ToList();
                    AverageDistanceDepartures = await _dBContext.Junes.Where(j => j.Departure_station_id == id).AverageAsync(x => x.Covered_distance_m);
                    AverageDistanceReturns = await _dBContext.Junes.Where(j => j.Return_station_id == id).AverageAsync(x => x.Covered_distance_m);
                    countOfDepartures = await _dBContext.Junes.Where(j => j.Departure_station_id == id).CountAsync();
                    countOfReturns = await _dBContext.Junes.Where(j => j.Return_station_id == id).CountAsync();
                    break;
                case 7:
                    popularReturns = (await _dBContext.Julys.Where(j => j.Departure_station_id == id).Select(j => j.Return_station_name).ToListAsync()).GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Select(j => j.name).Take(5).ToList();
                    popularDepartures = (await _dBContext.Julys.Where(j => j.Return_station_id == id).Select(j => j.Departure_station_name).ToListAsync()).GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Select(j => j.name).Take(5).ToList();
                    AverageDistanceDepartures = await _dBContext.Julys.Where(j => j.Departure_station_id == id).AverageAsync(x => x.Covered_distance_m);
                    AverageDistanceReturns = await _dBContext.Julys.Where(j => j.Return_station_id == id).AverageAsync(x => x.Covered_distance_m);
                    countOfDepartures = await _dBContext.Julys.Where(j => j.Departure_station_id == id).CountAsync();
                    countOfReturns = await _dBContext.Julys.Where(j => j.Return_station_id == id).CountAsync();
                    break;
                default:
                    var popularReturnsAnswer = (await _dBContext.Mays.Where(j => j.Departure_station_id == id).Select(j => j.Return_station_name).ToListAsync())
                        .Concat(await _dBContext.Junes.Where(j => j.Departure_station_id == id).Select(j => j.Return_station_name).ToListAsync())
                        .Concat(await _dBContext.Julys.Where(j => j.Departure_station_id == id).Select(j => j.Return_station_name).ToListAsync())
                        .GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Take(5).ToList();
                    if (popularReturnsAnswer != null)
                    {
                        popularReturns= popularReturnsAnswer.OrderByDescending(j => j.count).Select(j => j.name).ToList();
                    }

                    var popularDeparturesAnswer = (await _dBContext.Mays.Where(j => j.Return_station_id == id).Select(j => j.Departure_station_name).ToListAsync())
                        .Concat(await _dBContext.Junes.Where(j => j.Return_station_id == id).Select(j => j.Departure_station_name).ToListAsync())
                        .Concat(await _dBContext.Julys.Where(j => j.Return_station_id == id).Select(j => j.Departure_station_name).ToListAsync())
                        .GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Take(5).ToList();
                    if (popularDeparturesAnswer != null)
                    {
                        popularDepartures = popularDeparturesAnswer.OrderByDescending(j => j.count).Select(j => j.name).ToList();
                    }

                    AverageDistanceDepartures = (await _dBContext.Julys.Where(j => j.Departure_station_id == id).Select(j => j.Covered_distance_m).ToListAsync())
                        .Concat(await _dBContext.Junes.Where(j => j.Departure_station_id == id).Select(j => j.Covered_distance_m).ToListAsync())
                        .Concat(await _dBContext.Mays.Where(j => j.Departure_station_id == id).Select(j => j.Covered_distance_m).ToListAsync()).ToList().Average();

                    AverageDistanceReturns = (await _dBContext.Julys.Where(j => j.Return_station_id == id).Select(j => j.Covered_distance_m).ToListAsync())
                        .Concat(await _dBContext.Junes.Where(j => j.Return_station_id == id).Select(j => j.Covered_distance_m).ToListAsync())
                        .Concat(await _dBContext.Mays.Where(j => j.Return_station_id == id).Select(j => j.Covered_distance_m).ToListAsync()).ToList().Average();

                    countOfDepartures = await _dBContext.Julys.Where(j => j.Departure_station_id == id).CountAsync()
                         + await _dBContext.Junes.Where(j => j.Departure_station_id == id).CountAsync()
                         + await _dBContext.Mays.Where(j => j.Departure_station_id == id).CountAsync();

                    countOfReturns = await _dBContext.Julys.Where(j => j.Return_station_id == id).CountAsync()
                         + await _dBContext.Junes.Where(j => j.Return_station_id == id).CountAsync()
                         + await _dBContext.Mays.Where(j => j.Return_station_id == id).CountAsync();
                    break;
            }

            if (countOfDepartures != 0)
            {
                journeyInfo.PopularReturns = popularReturns;
                journeyInfo.AverageDistanceDepartures = Math.Round((decimal)(AverageDistanceDepartures / 1000), 2);
            }
            if (countOfReturns != 0)
            {
                journeyInfo.PopularDepartures = popularDepartures;
                journeyInfo.AverageDistanceReturns = Math.Round((decimal)(AverageDistanceReturns / 1000), 2);
            }
            journeyInfo.CountOfDepartures = countOfDepartures;
            journeyInfo.CountOfReturns = countOfReturns;

            return await _dBContext.Stations.Where( s => s.Id == id).Select(s => new StationInfoDTO
                {
                    Adress = s.Adress,
                    Name = s.Name,
                    Nimi = s.Nimi,
                    Namn = s.Namn,
                    Osoite = s.Osoite,
                    x = double.Parse(s.x, CultureInfo.InvariantCulture.NumberFormat),
                    y = double.Parse(s.y, CultureInfo.InvariantCulture.NumberFormat),
                    journeyInfo = journeyInfo
                }
            )
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Station>> GetStations(int offset, int limit, string order, string search, bool descending)
        {
            switch (order)
            {
                case "Nimi":
                    if (descending)
                    {
                        return await _dBContext.Stations
                            .Select(s => new Station { Id = s.Id, Nimi = s.Nimi, Namn = s.Namn, Name = s.Name, Osoite = s.Osoite, Adress = s.Osoite })
                            .Where(s =>  
                            s.Nimi.ToLower().Contains(search.ToLower()) 
                            || s.Namn.ToLower().Contains(search.ToLower()) 
                            || s.Name.ToLower().Contains(search.ToLower()) 
                            || s.Osoite.ToLower().Contains(search.ToLower()) 
                            || s.Adress.ToLower().Contains(search.ToLower()))
                            .OrderByDescending(s => s.Nimi).Skip(offset).Take(limit).ToListAsync();
                    }
                    else
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.Nimi.ToLower().Contains(search.ToLower())
                            || s.Namn.ToLower().Contains(search.ToLower())
                            || s.Name.ToLower().Contains(search.ToLower())
                            || s.Osoite.ToLower().Contains(search.ToLower())
                            || s.Adress.ToLower().Contains(search.ToLower()))
                            .OrderBy(s => s.Nimi).Skip(offset).Take(limit).ToListAsync();

                    }
                case "Namn":
                    if (descending)
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.Nimi.ToLower().Contains(search.ToLower())
                            || s.Namn.ToLower().Contains(search.ToLower())
                            || s.Name.ToLower().Contains(search.ToLower())
                            || s.Osoite.ToLower().Contains(search.ToLower())
                            || s.Adress.ToLower().Contains(search.ToLower()))
                            .OrderByDescending(s => s.Namn).Skip(offset).Take(limit).ToListAsync();
                    }
                    else
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.Nimi.ToLower().Contains(search.ToLower())
                            || s.Namn.ToLower().Contains(search.ToLower())
                            || s.Name.ToLower().Contains(search.ToLower())
                            || s.Osoite.ToLower().Contains(search.ToLower())
                            || s.Adress.ToLower().Contains(search.ToLower()))
                            .OrderBy(s => s.Namn).Skip(offset).Take(limit).ToListAsync();
                    }
                case "Name":
                    if (descending)
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.Nimi.ToLower().Contains(search.ToLower())
                            || s.Namn.ToLower().Contains(search.ToLower())
                            || s.Name.ToLower().Contains(search.ToLower())
                            || s.Osoite.ToLower().Contains(search.ToLower())
                            || s.Adress.ToLower().Contains(search.ToLower()))
                            .OrderByDescending(s => s.Name).Skip(offset).Take(limit).ToListAsync();
                    }
                    else
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.Nimi.ToLower().Contains(search.ToLower())
                            || s.Namn.ToLower().Contains(search.ToLower())
                            || s.Name.ToLower().Contains(search.ToLower())
                            || s.Osoite.ToLower().Contains(search.ToLower())
                            || s.Adress.ToLower().Contains(search.ToLower()))
                            .OrderBy(s => s.Name).Skip(offset).Take(limit).ToListAsync();
                    }
                case "Osoite":
                    if (descending)
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.Nimi.ToLower().Contains(search.ToLower())
                            || s.Namn.ToLower().Contains(search.ToLower())
                            || s.Name.ToLower().Contains(search.ToLower())
                            || s.Osoite.ToLower().Contains(search.ToLower())
                            || s.Adress.ToLower().Contains(search.ToLower()))
                            .OrderByDescending(s => s.Osoite).Skip(offset).Take(limit).ToListAsync();
                    }
                    else
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.Nimi.ToLower().Contains(search.ToLower())
                            || s.Namn.ToLower().Contains(search.ToLower())
                            || s.Name.ToLower().Contains(search.ToLower())
                            || s.Osoite.ToLower().Contains(search.ToLower())
                            || s.Adress.ToLower().Contains(search.ToLower()))
                            .OrderBy(s => s.Osoite).Skip(offset).Take(limit).ToListAsync();
                    }
                default:
                    if (descending)
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.Nimi.ToLower().Contains(search.ToLower())
                            || s.Namn.ToLower().Contains(search.ToLower())
                            || s.Name.ToLower().Contains(search.ToLower())
                            || s.Osoite.ToLower().Contains(search.ToLower())
                            || s.Adress.ToLower().Contains(search.ToLower()))
                            .OrderByDescending(s => s.Adress).Skip(offset).Take(limit).ToListAsync();
                    }
                    else
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.Nimi.ToLower().Contains(search.ToLower())
                            || s.Namn.ToLower().Contains(search.ToLower())
                            || s.Name.ToLower().Contains(search.ToLower())
                            || s.Osoite.ToLower().Contains(search.ToLower())
                            || s.Adress.ToLower().Contains(search.ToLower()))
                            .OrderBy(s => s.Adress).Skip(offset).Take(limit).ToListAsync();
                    }
            }
        }
        public async Task<int> GetStationCount(string search)
        {
            return await _dBContext.Stations.Where(s =>
                            s.Nimi.ToLower().Contains(search.ToLower())
                            || s.Namn.ToLower().Contains(search.ToLower())
                            || s.Name.ToLower().Contains(search.ToLower())
                            || s.Osoite.ToLower().Contains(search.ToLower())
                            || s.Adress.ToLower().Contains(search.ToLower())).CountAsync();
        }
    }
}
