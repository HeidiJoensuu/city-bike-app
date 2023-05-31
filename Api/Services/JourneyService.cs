using Api.Exceptions;
using Api.Models;
using Api.Models.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            var departureStation = _dBContext.Stations.Where(station => station.id == journey.departure_station_id && station.nimi == journey.departure_station_name).FirstOrDefault();
            var returnStation = _dBContext.Stations.Where(station => station.id == journey.return_station_id && station.nimi == journey.return_station_name).FirstOrDefault();

            if (returnStation == null)
            {
                throw new InvalidInputException("Return_station", journey.return_station_id, journey.return_station_name);
            }
            if (departureStation == null)
            {
                throw new InvalidInputException("Departure_station", journey.departure_station_id, journey.departure_station_name);
            }
            if (journey.covered_distance_m < 10)
            {
                throw new InvalidInputException("Covered_distance_m", journey.covered_distance_m);
            }
            if (journey.duration_sec < 10)
            {
                throw new InvalidInputException("Duration_sec", journey.duration_sec);
            }
            if (journey.departure.Year != 2021)
            {
                throw new InvalidInputException("Departure", journey.departure.ToString());
            }
            if (journey.returntime.Year != 2021 || DateTime.Compare(journey.departure, journey.returntime) >= 0)
            {
                throw new InvalidInputException("Departure", journey.returntime.ToString());
            }

            switch (journey.departure.Month)
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
                    throw new InvalidInputException("Departure", journey.departure.ToString());
            }
        }

        public async Task<IEnumerable<JourneyAbstract>> GetJourneys(int offset, int limit, string order, string search, bool descending, int month, string departure, string returnTime, double distanceMin, double distanceMax, int durationMin, int durationMax)
        {
            Console.WriteLine($"{durationMin} | {durationMax}");

            switch (order)
            {
                case "Departure":
                    if (month == 5)
                    {
                        if (descending)
                        {
                            return  await _dBContext.Mays
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Mays
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else if (month == 6)
                    {
                        if (descending)
                        {
                            return await _dBContext.Junes
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Junes
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else
                    {
                        if (descending)
                        {
                            return await _dBContext.Julys
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Julys
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                case "Returntime":
                    if (month == 5)
                    {
                        if (descending)
                        {
                            return await _dBContext.Mays
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.returntime)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Mays
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.returntime)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else if (month == 6)
                    {
                        if (descending)
                        {
                            return await _dBContext.Junes
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.returntime)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Junes
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.returntime)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else
                    {
                        if (descending)
                        {
                            return await _dBContext.Julys
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .OrderByDescending(journey => journey.returntime)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Julys
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.returntime)
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
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.departure_station_name)
                                .ThenBy(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Mays
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.departure_station_name)
                            .ThenBy(journey => journey.departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else if (month == 6)
                    {
                        if (descending)
                        {
                            return await _dBContext.Junes
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.departure_station_name)
                                .ThenBy(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Junes
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.departure_station_name)
                            .ThenBy(journey => journey.departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else
                    {
                        if (descending)
                        {
                            return await _dBContext.Julys
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.departure_station_name)
                                .ThenBy(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Julys
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.departure_station_name)
                            .ThenBy(journey => journey.departure)
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
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.return_station_name)
                                .ThenBy(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Mays
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.return_station_name)
                            .ThenBy(journey => journey.departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else if (month == 6)
                    {
                        if (descending)
                        {
                            return await _dBContext.Junes
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.return_station_name)
                                .ThenBy(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Junes
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.return_station_name)
                            .ThenBy(journey => journey.departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else
                    {
                        if (descending)
                        {
                            return await _dBContext.Julys
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.return_station_name)
                                .ThenBy(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Julys
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.return_station_name)
                            .ThenBy(journey => journey.departure)
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
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= (int)distanceMin * 1000)
                                .Where(journey => journey.covered_distance_m <= (int)distanceMax * 1000)
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.covered_distance_m)
                                .ThenBy(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Mays
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.covered_distance_m)
                            .ThenBy(journey => journey.departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else if (month == 6)
                    {
                        if (descending)
                        {
                            return await _dBContext.Junes
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.covered_distance_m)
                                .ThenBy(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Junes
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.covered_distance_m)
                            .ThenBy(journey => journey.departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else
                    {
                        if (descending)
                        {
                            return await _dBContext.Julys
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.covered_distance_m)
                                .ThenBy(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Julys
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.covered_distance_m)
                            .ThenBy(journey => journey.departure)
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
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.duration_sec)
                                .ThenBy(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Mays
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.duration_sec)
                            .ThenBy(journey => journey.departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else if (month == 6)
                    {
                        if (descending)
                        {
                            return await _dBContext.Junes
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.duration_sec)
                                .ThenBy(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Junes
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.duration_sec)
                            .ThenBy(journey => journey.departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
                    else
                    {
                        if (descending)
                        {
                            return await _dBContext.Julys
                                .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                                .Where(journey => journey.departure >= DateTime.Parse(departure))
                                .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                                .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                                .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                                .Where(journey => journey.duration_sec >= durationMin)
                                .Where(journey => journey.duration_sec <= durationMax)
                                .OrderByDescending(journey => journey.duration_sec)
                                .ThenBy(journey => journey.departure)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
                        }
                        return await _dBContext.Julys
                            .Where(journey => journey.departure_station_name.ToLower().Contains(search.ToLower()) || journey.return_station_name.ToLower().Contains(search.ToLower()))
                            .Where(journey => journey.departure >= DateTime.Parse(departure))
                            .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                            .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                            .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                            .Where(journey => journey.duration_sec >= durationMin)
                            .Where(journey => journey.duration_sec <= durationMax)
                            .OrderBy(journey => journey.duration_sec)
                            .ThenBy(journey => journey.departure)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
                    }
            }
        }

        public async Task<int> GetJourneysCount(string search, int month, string departure, string returnTime, double distanceMin, double distanceMax, int durationMin, int durationMax)
        {
            switch(month)
            {
                case 5:
                    return await _dBContext.Mays.Where(
                        journey => journey.departure_station_name.ToLower().Contains(search.ToLower())
                        || journey.return_station_name.ToLower().Contains(search.ToLower()))
                        .Where(journey => journey.departure >= DateTime.Parse(departure))
                        .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                        .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                        .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                        .Where(journey => journey.duration_sec >= durationMin)
                        .Where(journey => journey.duration_sec <= durationMax)
                        .CountAsync();
                case 6:
                    return await _dBContext.Junes.Where(
                        journey => journey.departure_station_name.ToLower().Contains(search.ToLower())
                        || journey.return_station_name.ToLower().Contains(search.ToLower()))
                        .Where(journey => journey.departure >= DateTime.Parse(departure))
                        .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                        .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                        .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                        .Where(journey => journey.duration_sec >= durationMin)
                        .Where(journey => journey.duration_sec <= durationMax)
                        .CountAsync();
                default:
                    return await _dBContext.Julys.Where(
                        journey => journey.departure_station_name.ToLower().Contains(search.ToLower())
                        || journey.return_station_name.ToLower().Contains(search.ToLower()))
                        .Where(journey => journey.departure >= DateTime.Parse(departure))
                        .Where(journey => journey.returntime <= DateTime.Parse(returnTime))
                        .Where(journey => journey.covered_distance_m >= ((int)distanceMin * 1000))
                        .Where(journey => journey.covered_distance_m <= ((int)distanceMax * 1000))
                        .Where(journey => journey.duration_sec >= durationMin)
                        .Where(journey => journey.duration_sec <= durationMax)
                        .CountAsync();
            }
        }
    }
}
