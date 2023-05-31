using Api.Models;
using Api.Models.DTOs;
using Api.Models.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.Internal;
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
                if (prop.Name.ToString() != "id" && prop.Name.ToString() != "may" && prop.Name.ToString() != "june" && prop.Name.ToString() != "july" && prop.Name.ToString() != "kaupunki")
                {
                    if (prop.GetValue(saveStation, null).ToString() is "" or null or "0")
                    {
                        throw new MissingInputsException(prop.Name.ToString());
                    }
                }
            }
            var dublicate = _dBContext.Stations.Select(station => new { nimi = station.nimi, namn = station.namn, name = station.name })
                .Where(station => station.name == saveStation.name || station.namn == saveStation.namn || station.nimi == saveStation.nimi).FirstOrDefault();
            if (await _dBContext.Stations.AnyAsync(station => station.nimi == saveStation.nimi))
            {
                throw new DuplicateException("Nimi", saveStation.nimi);
            }
            if (await _dBContext.Stations.AnyAsync(station => station.namn == saveStation.namn))
            {
                throw new DuplicateException("Nimi", saveStation.nimi);
            }
            if (await _dBContext.Stations.AnyAsync(station => station.name == saveStation.name))
            {
                throw new DuplicateException("Nimi", saveStation.nimi);
            }

            var id = await _dBContext.Stations.MaxAsync(s => s.id); //Because FID in Database is the primarykey and therefore auto incremented and Id is not
            saveStation.id = id+1;

            await _dBContext.Stations.AddAsync(saveStation);
            await _dBContext.SaveChangesAsync();

            return saveStation;
        }

        public async Task<StationInfoDTO> GetStationInfo(int id, int month)
        {
            //Setting default information for StationInfoDTO
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
                    popularReturns = (await _dBContext.Mays.Where(j => j.departure_station_id == id).Select(j => j.return_station_name).ToListAsync()).GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Select(j => j.name).Take(5).ToList();
                    popularDepartures = (await _dBContext.Mays.Where(j => j.return_station_id == id).Select(j => j.departure_station_name).ToListAsync()).GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Select(j => j.name).Take(5).ToList();
                    AverageDistanceDepartures = await _dBContext.Mays.Where(j => j.departure_station_id == id).AverageAsync(x => x.covered_distance_m);
                    AverageDistanceReturns = await _dBContext.Mays.Where(j => j.return_station_id == id).AverageAsync(x => x.covered_distance_m);
                    countOfDepartures = await _dBContext.Mays.Where(j => j.departure_station_id == id).CountAsync();
                    countOfReturns = await _dBContext.Mays.Where(j => j.return_station_id == id).CountAsync();
                    break;
                case 6:
                    popularReturns = (await _dBContext.Junes.Where(j => j.departure_station_id == id).Select(j => j.return_station_name).ToListAsync()).GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Select(j => j.name).Take(5).ToList();
                    popularDepartures = (await _dBContext.Junes.Where(j => j.return_station_id == id).Select(j => j.departure_station_name).ToListAsync()).GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Select(j => j.name).Take(5).ToList();
                    AverageDistanceDepartures = await _dBContext.Junes.Where(j => j.departure_station_id == id).AverageAsync(x => x.covered_distance_m);
                    AverageDistanceReturns = await _dBContext.Junes.Where(j => j.return_station_id == id).AverageAsync(x => x.covered_distance_m);
                    countOfDepartures = await _dBContext.Junes.Where(j => j.departure_station_id == id).CountAsync();
                    countOfReturns = await _dBContext.Junes.Where(j => j.return_station_id == id).CountAsync();
                    break;
                case 7:
                    popularReturns = (await _dBContext.Julys.Where(j => j.departure_station_id == id).Select(j => j.return_station_name).ToListAsync()).GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Select(j => j.name).Take(5).ToList();
                    popularDepartures = (await _dBContext.Julys.Where(j => j.return_station_id == id).Select(j => j.departure_station_name).ToListAsync()).GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Select(j => j.name).Take(5).ToList();
                    AverageDistanceDepartures = await _dBContext.Julys.Where(j => j.departure_station_id == id).AverageAsync(x => x.covered_distance_m);
                    AverageDistanceReturns = await _dBContext.Julys.Where(j => j.return_station_id == id).AverageAsync(x => x.covered_distance_m);
                    countOfDepartures = await _dBContext.Julys.Where(j => j.departure_station_id == id).CountAsync();
                    countOfReturns = await _dBContext.Julys.Where(j => j.return_station_id == id).CountAsync();
                    break;
                default: //All three months together
                    var popularReturnsAnswer = (await _dBContext.Mays.Where(j => j.departure_station_id == id).Select(j => j.return_station_name).ToListAsync())
                        .Concat(await _dBContext.Junes.Where(j => j.departure_station_id == id).Select(j => j.return_station_name).ToListAsync())
                        .Concat(await _dBContext.Julys.Where(j => j.departure_station_id == id).Select(j => j.return_station_name).ToListAsync())
                        .GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Take(5).ToList();
                    if (popularReturnsAnswer != null)
                    {
                        popularReturns= popularReturnsAnswer.OrderByDescending(j => j.count).Select(j => j.name).ToList();
                    }

                    var popularDeparturesAnswer = (await _dBContext.Mays.Where(j => j.return_station_id == id).Select(j => j.departure_station_name).ToListAsync())
                        .Concat(await _dBContext.Junes.Where(j => j.return_station_id == id).Select(j => j.departure_station_name).ToListAsync())
                        .Concat(await _dBContext.Julys.Where(j => j.return_station_id == id).Select(j => j.departure_station_name).ToListAsync())
                        .GroupBy(g => g).Select(g => new { count = g.Count(), name = g.Key }).ToList().OrderByDescending(j => j.count).Take(5).ToList();
                    if (popularDeparturesAnswer != null)
                    {
                        popularDepartures = popularDeparturesAnswer.OrderByDescending(j => j.count).Select(j => j.name).ToList();
                    }

                    AverageDistanceDepartures = (await _dBContext.Julys.Where(j => j.departure_station_id == id).Select(j => j.covered_distance_m).ToListAsync())
                        .Concat(await _dBContext.Junes.Where(j => j.departure_station_id == id).Select(j => j.covered_distance_m).ToListAsync())
                        .Concat(await _dBContext.Mays.Where(j => j.departure_station_id == id).Select(j => j.covered_distance_m).ToListAsync()).ToList().Average();

                    AverageDistanceReturns = (await _dBContext.Julys.Where(j => j.return_station_id == id).Select(j => j.covered_distance_m).ToListAsync())
                        .Concat(await _dBContext.Junes.Where(j => j.return_station_id == id).Select(j => j.covered_distance_m).ToListAsync())
                        .Concat(await _dBContext.Mays.Where(j => j.return_station_id == id).Select(j => j.covered_distance_m).ToListAsync()).ToList().Average();

                    countOfDepartures = await _dBContext.Julys.Where(j => j.departure_station_id == id).CountAsync()
                         + await _dBContext.Junes.Where(j => j.departure_station_id == id).CountAsync()
                         + await _dBContext.Mays.Where(j => j.departure_station_id == id).CountAsync();

                    countOfReturns = await _dBContext.Julys.Where(j => j.return_station_id == id).CountAsync()
                         + await _dBContext.Junes.Where(j => j.return_station_id == id).CountAsync()
                         + await _dBContext.Mays.Where(j => j.return_station_id == id).CountAsync();
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

            return await _dBContext.Stations.Where( s => s.id == id).Select(s => new StationInfoDTO
                {
                    Adress = s.adress,
                    Name = s.name,
                    Nimi = s.nimi,
                    Namn = s.namn,
                    Osoite = s.osoite,
                    x = s.x,
                    y = s.y,
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
                            .Select(s => new Station { id = s.id, nimi = s.nimi, namn = s.namn, name = s.name, osoite = s.osoite, adress = s.osoite })
                            .Where(s =>  
                            s.nimi.ToLower().Contains(search.ToLower()) 
                            || s.namn.ToLower().Contains(search.ToLower()) 
                            || s.name.ToLower().Contains(search.ToLower()) 
                            || s.osoite.ToLower().Contains(search.ToLower()) 
                            || s.adress.ToLower().Contains(search.ToLower()))
                            .OrderByDescending(s => s.nimi).Skip(offset).Take(limit).ToListAsync();
                    }
                    else
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.nimi.ToLower().Contains(search.ToLower())
                            || s.namn.ToLower().Contains(search.ToLower())
                            || s.name.ToLower().Contains(search.ToLower())
                            || s.osoite.ToLower().Contains(search.ToLower())
                            || s.adress.ToLower().Contains(search.ToLower()))
                            .OrderBy(s => s.nimi).Skip(offset).Take(limit).ToListAsync();

                    }
                case "Namn":
                    if (descending)
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.nimi.ToLower().Contains(search.ToLower())
                            || s.namn.ToLower().Contains(search.ToLower())
                            || s.name.ToLower().Contains(search.ToLower())
                            || s.osoite.ToLower().Contains(search.ToLower())
                            || s.adress.ToLower().Contains(search.ToLower()))
                            .OrderByDescending(s => s.namn).Skip(offset).Take(limit).ToListAsync();
                    }
                    else
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.nimi.ToLower().Contains(search.ToLower())
                            || s.namn.ToLower().Contains(search.ToLower())
                            || s.name.ToLower().Contains(search.ToLower())
                            || s.osoite.ToLower().Contains(search.ToLower())
                            || s.adress.ToLower().Contains(search.ToLower()))
                            .OrderBy(s => s.namn).Skip(offset).Take(limit).ToListAsync();
                    }
                case "Name":
                    if (descending)
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.nimi.ToLower().Contains(search.ToLower())
                            || s.namn.ToLower().Contains(search.ToLower())
                            || s.name.ToLower().Contains(search.ToLower())
                            || s.osoite.ToLower().Contains(search.ToLower())
                            || s.adress.ToLower().Contains(search.ToLower()))
                            .OrderByDescending(s => s.name).Skip(offset).Take(limit).ToListAsync();
                    }
                    else
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.nimi.ToLower().Contains(search.ToLower())
                            || s.namn.ToLower().Contains(search.ToLower())
                            || s.name.ToLower().Contains(search.ToLower())
                            || s.osoite.ToLower().Contains(search.ToLower())
                            || s.adress.ToLower().Contains(search.ToLower()))
                            .OrderBy(s => s.name).Skip(offset).Take(limit).ToListAsync();
                    }
                case "Osoite":
                    if (descending)
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.nimi.ToLower().Contains(search.ToLower())
                            || s.namn.ToLower().Contains(search.ToLower())
                            || s.name.ToLower().Contains(search.ToLower())
                            || s.osoite.ToLower().Contains(search.ToLower())
                            || s.adress.ToLower().Contains(search.ToLower()))
                            .OrderByDescending(s => s.osoite).Skip(offset).Take(limit).ToListAsync();
                    }
                    else
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.nimi.ToLower().Contains(search.ToLower())
                            || s.namn.ToLower().Contains(search.ToLower())
                            || s.name.ToLower().Contains(search.ToLower())
                            || s.osoite.ToLower().Contains(search.ToLower())
                            || s.adress.ToLower().Contains(search.ToLower()))
                            .OrderBy(s => s.osoite).Skip(offset).Take(limit).ToListAsync();
                    }
                default:
                    if (descending)
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.nimi.ToLower().Contains(search.ToLower())
                            || s.namn.ToLower().Contains(search.ToLower())
                            || s.name.ToLower().Contains(search.ToLower())
                            || s.osoite.ToLower().Contains(search.ToLower())
                            || s.adress.ToLower().Contains(search.ToLower()))
                            .OrderByDescending(s => s.adress).Skip(offset).Take(limit).ToListAsync();
                    }
                    else
                    {
                        return await _dBContext.Stations.Where(s =>
                            s.nimi.ToLower().Contains(search.ToLower())
                            || s.namn.ToLower().Contains(search.ToLower())
                            || s.name.ToLower().Contains(search.ToLower())
                            || s.osoite.ToLower().Contains(search.ToLower())
                            || s.adress.ToLower().Contains(search.ToLower()))
                            .OrderBy(s => s.adress).Skip(offset).Take(limit).ToListAsync();
                    }
            }
        }
        public async Task<int> GetStationCount(string search)
        {
            return await _dBContext.Stations.Where(s =>
                s.nimi.ToLower().Contains(search.ToLower())
                || s.namn.ToLower().Contains(search.ToLower())
                || s.name.ToLower().Contains(search.ToLower())
                || s.osoite.ToLower().Contains(search.ToLower())
                || s.adress.ToLower().Contains(search.ToLower()))
                .CountAsync();
        }

        public async Task<IEnumerable<StationShortDto>> GetStationNames()
        {
            return await _dBContext.Stations.Select(station => new StationShortDto { Id = station.id, Nimi =station.nimi, Namn = station.namn, Name = station.name, Kaupunki = station.kaupunki}).ToListAsync();
        }
    }
}
