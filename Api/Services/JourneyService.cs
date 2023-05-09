using Api.Exceptions;
using Api.Models;
using Api.Models.Models;
using Api.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Api.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly CityBikesDBContext _dBContext;

        public JourneyService(CityBikesDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<JourneyAbstract> CreateJourney(JourneyAbstract journey)
        {
            var departureStation = _dBContext.Stations.Where(station => station.Id == journey.Departure_station_id && station.Nimi == journey.Departure_station_name).FirstOrDefault();
            var returnStation = _dBContext.Stations.Where(station => station.Id == journey.Return_station_id && station.Nimi == journey.Return_station_name).FirstOrDefault();

            if (returnStation == null)
            {
                throw new InvalidInputException("Return_station", journey.Return_station_id, journey.Return_station_name);
            }
            if (departureStation == null)
            {
                throw new InvalidInputException("Departure_station", journey.Departure_station_id, journey.Departure_station_name);
            }
            if (journey.Covered_distance_m < 10)
            {
                throw new InvalidInputException("Covered_distance_m", journey.Covered_distance_m);
            }
            if (journey.Duration_sec < 10)
            {
                throw new InvalidInputException("Duration_sec", journey.Duration_sec);
            }
            if (journey.Departure.Year != 2021)
            {
                throw new InvalidInputException("Departure", journey.Departure.ToString());
            }
            if (journey.Return.Year != 2021 || DateTime.Compare(journey.Departure, journey.Return) >= 0)
            {
                throw new InvalidInputException("Departure", journey.Return.ToString());
            }

            switch (journey.Departure.Month)
            {
                case 5:
                    May may = JsonConvert.DeserializeObject<May>(JsonConvert.SerializeObject(journey));
                    await _dBContext.Mays.AddAsync(may);
                    await _dBContext.SaveChangesAsync();
                    return may;
                case 6:
                    June june = JsonConvert.DeserializeObject<June>(JsonConvert.SerializeObject(journey));
                    await _dBContext.Junes.AddAsync(june);
                    await _dBContext.SaveChangesAsync();
                    return june;
                case 7:
                    July july = JsonConvert.DeserializeObject<July>(JsonConvert.SerializeObject(journey));
                    await _dBContext.Julys.AddAsync(july);
                    await _dBContext.SaveChangesAsync();
                    return july;
                default:
                    throw new InvalidInputException("Departure", journey.Departure.ToString());
            }

        }

        public async Task<IEnumerable<JourneyAbstract>> GetJourneys(int offset, int limit, string order, string search, bool descending, int month)
        {
            switch (order)
            {
                case "Departure":
                    if (month == 5)
                    {
                        if (descending)
                        {
                            return  await _dBContext.Mays
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Mays
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else if (month == 6)
                    {
                        if (descending)
                        {
                            return await _dBContext.Junes
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Junes
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else
                    {
                        if (descending)
                        {
                            return await _dBContext.Julys
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Julys
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                case "Return":
                    if (month == 5)
                    {
                        if (descending)
                        {
                            return await _dBContext.Mays
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Return)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Mays
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Return)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else if (month == 6)
                    {
                        if (descending)
                        {
                            return await _dBContext.Junes
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Return)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Junes
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Return)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else
                    {
                        if (descending)
                        {
                            return await _dBContext.Julys
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Return)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Julys
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Return)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                case "Departure_station_name":
                    if (month == 5)
                    {
                        if (descending)
                        {
                            return await _dBContext.Mays
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Departure_station_name)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Mays
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Departure_station_name)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else if (month == 6)
                    {
                        if (descending)
                        {
                            return await _dBContext.Junes
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Departure_station_name)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Junes
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Departure_station_name)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else
                    {
                        if (descending)
                        {
                            return await _dBContext.Julys
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Departure_station_name)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Julys
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Departure_station_name)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                case "Return_station_name":
                    if (month == 5)
                    {
                        if (descending)
                        {
                            return await _dBContext.Mays
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Return_station_name)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Mays
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Return_station_name)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else if (month == 6)
                    {
                        if (descending)
                        {
                            return await _dBContext.Junes
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Return_station_name)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Junes
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Return_station_name)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else
                    {
                        if (descending)
                        {
                            return await _dBContext.Julys
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Return_station_name)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Julys
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Return_station_name)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                case "Covered_distance":
                    if (month == 5)
                    {
                        if (descending)
                        {
                            return await _dBContext.Mays
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Covered_distance_m)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Mays
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Covered_distance_m)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else if (month == 6)
                    {
                        if (descending)
                        {
                            return await _dBContext.Junes
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Covered_distance_m)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Junes
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Covered_distance_m)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else
                    {
                        if (descending)
                        {
                            return await _dBContext.Julys
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Covered_distance_m)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Julys
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Covered_distance_m)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                default:
                    if (month == 5)
                    {
                        if (descending)
                        {
                            return await _dBContext.Mays
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Duration_sec)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Mays
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Duration_sec)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else if (month == 6)
                    {
                        if (descending)
                        {
                            return await _dBContext.Junes
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Duration_sec)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Junes
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Duration_sec)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else
                    {
                        if (descending)
                        {
                            return await _dBContext.Julys
                                .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                                .OrderByDescending(journey => journey.Duration_sec)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Julys
                            .Where(journey => journey.Departure_station_name.ToLower().Contains(search.ToLower()) || journey.Return_station_name.ToLower().Contains(search.ToLower()))
                            .OrderBy(journey => journey.Duration_sec)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
            }
        }
    }
}
