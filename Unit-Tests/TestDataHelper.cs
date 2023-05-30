using Api.Models.Models;

namespace Unit_Tests
{
    public static class TestDataHelper
    {
        public static List<Station> GetFakeStations()
        {
            return new List<Station>()
            {
                new Station
                {
                    id = 1,
                    nimi = "Ensimmainen",
                    namn = "Forsta",
                    name = "First",
                    osoite = "Ensimmainen katu 1",
                    adress = "Fosta gatan 1",
                    y = 60.2504081726074,
                    x = 24.2486486878388
                },
                new Station
                {
                    id = 2,
                    nimi = "Toinen",
                    namn = "Andra",
                    name = "Second",
                    osoite = "Toinen katu 2",
                    adress = "Andra gatan 2",
                    y = 60.3504077926351,
                    x = 24.2386486878388
                },
                new Station
                {
                    id = 3,
                    nimi = "Kolmas",
                    namn = "Tredje",
                    name = "Third",
                    osoite = "Kolmas katu 3",
                    adress = "Tredje gatan 3",
                    y = 60.2504077926351,
                    x = 24.2286486878388
                },
                new Station
                {
                    id = 4,
                    nimi = "Neljas",
                    namn = "Fjarde",
                    name = "Fourth",
                    osoite = "Neljas katu 4",
                    adress = "Fjarde gatan 4",
                    y = 60.2504077926351,
                    x = 24.3486486878388
                },
                new Station
                {
                    id = 5,
                    nimi = "Viides",
                    namn = "Femte",
                    name = "Fifth",
                    osoite = "Viides katu 5",
                    adress = "Femte gatan 5",
                    y = 60.4504077926351,
                    x = 24.6486486878388
                },
                new Station
                {
                    id = 6,
                    nimi = "Kuudes",
                    namn = "Sjatte",
                    name = "Sixth",
                    osoite = "Kuudes katu 6",
                    adress = "Sjatte gatan 6",
                    y = 60.1504077926351,
                    x = 24.8486486878388
                },
                new Station
                {
                    id = 7,
                    nimi = "Seitsemas",
                    namn = "Sjunde",
                    name = "Seventh",
                    osoite = "Seitsemas katu 7",
                    adress = "Sjunde gatan 7",
                    y = 60.5504077926351,
                    x = 24.7486486878388
                },
                new Station
                {
                    id = 8,
                    nimi = "Kahdeksas",
                    namn = "Attonde",
                    name = "Eighth",
                    osoite = "Kahdeksas katu 8",
                    adress = "Attonde gatan 8",
                    y = 60.6504077926351,
                    x = 24.8986486878388
                },
                new Station
                {
                    id = 9,
                    nimi = "Yhdeksas",
                    namn = "Nionde",
                    name = "Ninth",
                    osoite = "Yhdeksas katu 9",
                    adress = "Nionde gatan 9",
                    y = 60.7504077926351,
                    x = 24.7986486878388
                }
            };
        }

        public static List<July> GetFakeJulyJourneys()
        {
            return new List<July>
            {
                new July
                {
                    departure = new DateTime(2021, 07, 25, 10, 30, 0),
                    returntime = new DateTime(2021, 07, 25, 11, 0, 0),
                    departure_station_id = 1,
                    departure_station_name = "Ensimmainen",
                    return_station_id = 3,
                    return_station_name = "Kolmas",
                    covered_distance_m = 6000,
                    duration_sec = 1800
                },
                new July
                {
                    departure = new DateTime(2021, 07, 25, 12, 45, 0),
                    returntime = new DateTime(2021, 07, 25, 13, 0, 0),
                    departure_station_id = 4,
                    departure_station_name = "Neljas",
                    return_station_id = 2,
                    return_station_name = "Toinen",
                    covered_distance_m = 3000,
                    duration_sec = 900
                },
                new July
                {
                    departure = new DateTime(2021, 07, 2, 08, 30, 0),
                    returntime = new DateTime(2021, 07, 2, 08, 35, 0),
                    departure_station_id = 5,
                    departure_station_name = "Viides",
                    return_station_id = 1,
                    return_station_name = "Ensimmainen",
                    covered_distance_m = 1650,
                    duration_sec = 300
                },
                new July
                {
                    departure = new DateTime(2021, 07, 25, 15, 0, 0),
                    returntime = new DateTime(2021, 07, 25, 16, 0, 0),
                    departure_station_id = 6,
                    departure_station_name = "Kuudes",
                    return_station_id = 2,
                    return_station_name = "Toinen",
                    covered_distance_m = 6300,
                    duration_sec = 3600
                },
                new July
                {
                    departure = new DateTime(2021, 07, 25, 10, 30, 45),
                    returntime = new DateTime(2021, 07, 25, 11, 5, 0),
                    departure_station_id = 1,
                    departure_station_name = "Ensimmainen",
                    return_station_id = 7,
                    return_station_name = "Seitsemas",
                    covered_distance_m = 16000,
                    duration_sec = 2055
                },
                new July
                {
                    departure = new DateTime(2021, 07, 24, 19, 32, 0),
                    returntime = new DateTime(2021, 07, 24, 19, 0, 0),
                    departure_station_id = 3,
                    departure_station_name = "Kolmas",
                    return_station_id = 1,
                    return_station_name = "Ensimmainen",
                    covered_distance_m = 6450,
                    duration_sec = 1680
                },
                new July
                {
                    departure = new DateTime(2021, 07, 24, 12, 02, 0),
                    returntime = new DateTime(2021, 07, 24, 12, 45, 0),
                    departure_station_id = 5,
                    departure_station_name = "Viides",
                    return_station_id = 2,
                    return_station_name = "Toinen",
                    covered_distance_m = 4230,
                    duration_sec = 1580
                },
                new July
                {
                    departure = new DateTime(2021, 07, 3, 08, 30, 0),
                    returntime = new DateTime(2021, 07, 3, 08, 34, 0),
                    departure_station_id = 1,
                    departure_station_name = "Ensimmainen",
                    return_station_id = 1,
                    return_station_name = "Ensimmainen",
                    covered_distance_m = 1350,
                    duration_sec = 240
                },
                new July
                {
                    departure = new DateTime(2021, 07, 12, 15, 2, 0),
                    returntime = new DateTime(2021, 07, 12, 15, 20, 0),
                    departure_station_id = 3,
                    departure_station_name = "Kolmas",
                    return_station_id = 2,
                    return_station_name = "Toinen",
                    covered_distance_m = 2780,
                    duration_sec = 1080
                },
                new July
                {
                    departure = new DateTime(2021, 07, 31, 23, 58, 0),
                    returntime = new DateTime(2021, 08, 01, 00, 30, 0),
                    departure_station_id = 6,
                    departure_station_name = "Kuudes",
                    return_station_id = 1,
                    return_station_name = "Ensimmainen",
                    covered_distance_m = 16500,
                    duration_sec = 2220
                },
            };
        }

        public static List<June> GetFakeJuneJourneys()
        {
            return new List<June>
            {
                new June
                {
                    departure = new DateTime(2021, 06, 25, 9, 10, 0),
                    returntime = new DateTime(2021, 06, 25, 10, 35, 0),
                    departure_station_id = 2,
                    departure_station_name = "Toinen",
                    return_station_id = 2,
                    return_station_name = "Toinen",
                    covered_distance_m = 10000,
                    duration_sec = 5100
                },
                new June
                {
                    departure = new DateTime(2021, 06, 25, 17, 45, 0),
                    returntime = new DateTime(2021, 06, 25, 18, 06, 0),
                    departure_station_id = 8,
                    departure_station_name = "Kahdeksas",
                    return_station_id = 4,
                    return_station_name = "Neljas",
                    covered_distance_m = 3500,
                    duration_sec = 1260
                },
                new June
                {
                    departure = new DateTime(2021, 06, 2, 7, 15, 0),
                    returntime = new DateTime(2021, 06, 2, 7, 50, 0),
                    departure_station_id = 5,
                    departure_station_name = "Viides",
                    return_station_id = 1,
                    return_station_name = "Ensimmainen",
                    covered_distance_m = 4500,
                    duration_sec = 2100
                },
                new June
                {
                    departure = new DateTime(2021, 06, 25, 15, 40, 0),
                    returntime = new DateTime(2021, 06, 25, 16, 0, 30),
                    departure_station_id = 5,
                    departure_station_name = "Viides",
                    return_station_id = 2,
                    return_station_name = "Toinen",
                    covered_distance_m = 4000,
                    duration_sec = 1230
                },
                new June
                {
                    departure = new DateTime(2021, 06, 25, 14, 25, 30),
                    returntime = new DateTime(2021, 06, 25, 14, 40, 0),
                    departure_station_id = 8,
                    departure_station_name = "Kahdeksas",
                    return_station_id = 9,
                    return_station_name = "Yhdeksas",
                    covered_distance_m = 2000,
                    duration_sec = 870
                },
            };
        }

        public static List<May> GetFakeMayJourneys()
        {
            return new List<May>
            {
                new May
                {
                    departure = new DateTime(2021, 05, 25, 10, 15, 0),
                    returntime = new DateTime(2021, 05, 25, 11, 0, 0),
                    departure_station_id = 1,
                    departure_station_name = "Ensimmainen",
                    return_station_id = 4,
                    return_station_name = "Neljas",
                    covered_distance_m = 8000,
                    duration_sec = 2700
                },
                new May
                {
                    departure = new DateTime(2021, 05, 25, 12, 43, 0),
                    returntime = new DateTime(2021, 05, 25, 12, 55, 0),
                    departure_station_id = 3,
                    departure_station_name = "Kolmas",
                    return_station_id = 5,
                    return_station_name = "Viides",
                    covered_distance_m = 3200,
                    duration_sec = 720
                },
                new May
                {
                    departure = new DateTime(2021, 05, 2, 08, 32, 0),
                    returntime = new DateTime(2021, 05, 2, 08, 35, 0),
                    departure_station_id = 6,
                    departure_station_name = "Kuudes",
                    return_station_id = 9,
                    return_station_name = "Yhdeksas",
                    covered_distance_m = 1600,
                    duration_sec = 180
                },
                new May
                {
                    departure = new DateTime(2021, 05, 25, 15, 27, 0),
                    returntime = new DateTime(2021, 05, 25, 15, 52, 0),
                    departure_station_id = 8,
                    departure_station_name = "Kahdeksas",
                    return_station_id = 2,
                    return_station_name = "Toinen",
                    covered_distance_m = 6400,
                    duration_sec = 1560
                },
                new May
                {
                    departure = new DateTime(2021, 05, 25, 10, 38, 0),
                    returntime = new DateTime(2021, 05, 25, 11, 0, 0),
                    departure_station_id = 6,
                    departure_station_name = "Kuudes",
                    return_station_id = 3,
                    return_station_name = "Kolmas",
                    covered_distance_m = 1230,
                    duration_sec = 1320
                },
            };
        }
        public static List<Journey> GetFakeJourneys()
        {
            return new List<Journey> { };
        }
    }
}
