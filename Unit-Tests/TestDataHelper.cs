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
                    Id = 1,
                    Nimi = "Ensimmainen",
                    Namn = "Forsta",
                    Name = "First",
                    Osoite = "Ensimmainen katu 1",
                    Adress = "Fosta gatan 1",
                    y = "60.2504081726074",
                    x = "24.2486486878388"
                },
                new Station
                {
                    Id = 2,
                    Nimi = "Toinen",
                    Namn = "Andra",
                    Name = "Second",
                    Osoite = "Toinen katu 2",
                    Adress = "Andra gatan 2",
                    y = "60.3504077926351",
                    x = "24.2386486878388"
                },
                new Station
                {
                    Id = 3,
                    Nimi = "Kolmas",
                    Namn = "Tredje",
                    Name = "Third",
                    Osoite = "Kolmas katu 3",
                    Adress = "Tredje gatan 3",
                    y = "60.2504077926351",
                    x = "24.2286486878388"
                },
                new Station
                {
                    Id = 4,
                    Nimi = "Neljas",
                    Namn = "Fjarde",
                    Name = "Fourth",
                    Osoite = "Neljas katu 4",
                    Adress = "Fjarde gatan 4",
                    y = "60.2504077926351",
                    x = "24.3486486878388"
                },
                new Station
                {
                    Id = 5,
                    Nimi = "Viides",
                    Namn = "Femte",
                    Name = "Fifth",
                    Osoite = "Viides katu 5",
                    Adress = "Femte gatan 5",
                    y = "60.4504077926351",
                    x = "24.6486486878388"
                },
                new Station
                {
                    Id = 6,
                    Nimi = "Kuudes",
                    Namn = "Sjatte",
                    Name = "Sixth",
                    Osoite = "Kuudes katu 6",
                    Adress = "Sjatte gatan 6",
                    y = "60.1504077926351",
                    x = "24.8486486878388"
                },
                new Station
                {
                    Id = 7,
                    Nimi = "Seitsemas",
                    Namn = "Sjunde",
                    Name = "Seventh",
                    Osoite = "Seitsemas katu 7",
                    Adress = "Sjunde gatan 7",
                    y = "60.5504077926351",
                    x = "24.7486486878388"
                },
                new Station
                {
                    Id = 8,
                    Nimi = "Kahdeksas",
                    Namn = "Attonde",
                    Name = "Eighth",
                    Osoite = "Kahdeksas katu 8",
                    Adress = "Attonde gatan 8",
                    y = "60.6504077926351",
                    x = "24.8986486878388",
                },
                new Station
                {
                    Id = 9,
                    Nimi = "Yhdeksas",
                    Namn = "Nionde",
                    Name = "Ninth",
                    Osoite = "Yhdeksas katu 9",
                    Adress = "Nionde gatan 9",
                    y ="60.7504077926351",
                    x = "24.7986486878388"
                }
            };
        }

        public static List<July> GetFakeJulyJourneys()
        {
            return new List<July>
            {
                new July
                {
                    Departure = new DateTime(2021, 07, 25, 10, 30, 0),
                    Return = new DateTime(2021, 07, 25, 11, 0, 0),
                    Departure_station_id = 1,
                    Departure_station_name = "Ensimmainen",
                    Return_station_id = 3,
                    Return_station_name = "Kolmas",
                    Covered_distance_m = 6000,
                    Duration_sec = 1800
                },
                new July
                {
                    Departure = new DateTime(2021, 07, 25, 12, 45, 0),
                    Return = new DateTime(2021, 07, 25, 13, 0, 0),
                    Departure_station_id = 4,
                    Departure_station_name = "Neljas",
                    Return_station_id = 2,
                    Return_station_name = "Toinen",
                    Covered_distance_m = 3000,
                    Duration_sec = 900
                },
                new July
                {
                    Departure = new DateTime(2021, 07, 2, 08, 30, 0),
                    Return = new DateTime(2021, 07, 2, 08, 35, 0),
                    Departure_station_id = 5,
                    Departure_station_name = "Viides",
                    Return_station_id = 1,
                    Return_station_name = "Ensimmainen",
                    Covered_distance_m = 1650,
                    Duration_sec = 300
                },
                new July
                {
                    Departure = new DateTime(2021, 07, 25, 15, 0, 0),
                    Return = new DateTime(2021, 07, 25, 16, 0, 0),
                    Departure_station_id = 6,
                    Departure_station_name = "Kuudes",
                    Return_station_id = 2,
                    Return_station_name = "Toinen",
                    Covered_distance_m = 6300,
                    Duration_sec = 3600
                },
                new July
                {
                    Departure = new DateTime(2021, 07, 25, 10, 30, 45),
                    Return = new DateTime(2021, 07, 25, 11, 5, 0),
                    Departure_station_id = 1,
                    Departure_station_name = "Ensimmainen",
                    Return_station_id = 7,
                    Return_station_name = "Seitsemas",
                    Covered_distance_m = 16000,
                    Duration_sec = 2055
                },
                new July
                {
                    Departure = new DateTime(2021, 07, 24, 19, 32, 0),
                    Return = new DateTime(2021, 07, 24, 19, 0, 0),
                    Departure_station_id = 3,
                    Departure_station_name = "Kolmas",
                    Return_station_id = 1,
                    Return_station_name = "Ensimmainen",
                    Covered_distance_m = 6450,
                    Duration_sec = 1680
                },
                new July
                {
                    Departure = new DateTime(2021, 07, 24, 12, 02, 0),
                    Return = new DateTime(2021, 07, 24, 12, 45, 0),
                    Departure_station_id = 5,
                    Departure_station_name = "Viides",
                    Return_station_id = 2,
                    Return_station_name = "Toinen",
                    Covered_distance_m = 4230,
                    Duration_sec = 1580
                },
                new July
                {
                    Departure = new DateTime(2021, 07, 3, 08, 30, 0),
                    Return = new DateTime(2021, 07, 3, 08, 34, 0),
                    Departure_station_id = 1,
                    Departure_station_name = "Ensimmainen",
                    Return_station_id = 1,
                    Return_station_name = "Ensimmainen",
                    Covered_distance_m = 1350,
                    Duration_sec = 240
                },
                new July
                {
                    Departure = new DateTime(2021, 07, 12, 15, 2, 0),
                    Return = new DateTime(2021, 07, 12, 15, 20, 0),
                    Departure_station_id = 3,
                    Departure_station_name = "Kolmas",
                    Return_station_id = 2,
                    Return_station_name = "Toinen",
                    Covered_distance_m = 2780,
                    Duration_sec = 1080
                },
                new July
                {
                    Departure = new DateTime(2021, 07, 31, 23, 58, 0),
                    Return = new DateTime(2021, 08, 01, 00, 30, 0),
                    Departure_station_id = 6,
                    Departure_station_name = "Kuudes",
                    Return_station_id = 1,
                    Return_station_name = "Ensimmainen",
                    Covered_distance_m = 16500,
                    Duration_sec = 2220
                },
            };
        }

        public static List<June> GetFakeJuneJourneys()
        {
            return new List<June>
            {
                new June
                {
                    Departure = new DateTime(2021, 06, 25, 9, 10, 0),
                    Return = new DateTime(2021, 06, 25, 10, 35, 0),
                    Departure_station_id = 2,
                    Departure_station_name = "Toinen",
                    Return_station_id = 2,
                    Return_station_name = "Toinen",
                    Covered_distance_m = 10000,
                    Duration_sec = 5100
                },
                new June
                {
                    Departure = new DateTime(2021, 06, 25, 17, 45, 0),
                    Return = new DateTime(2021, 06, 25, 18, 06, 0),
                    Departure_station_id = 8,
                    Departure_station_name = "Kahdeksas",
                    Return_station_id = 4,
                    Return_station_name = "Neljas",
                    Covered_distance_m = 3500,
                    Duration_sec = 1260
                },
                new June
                {
                    Departure = new DateTime(2021, 06, 2, 7, 15, 0),
                    Return = new DateTime(2021, 06, 2, 7, 50, 0),
                    Departure_station_id = 5,
                    Departure_station_name = "Viides",
                    Return_station_id = 1,
                    Return_station_name = "Ensimmainen",
                    Covered_distance_m = 4500,
                    Duration_sec = 2100
                },
                new June
                {
                    Departure = new DateTime(2021, 06, 25, 15, 40, 0),
                    Return = new DateTime(2021, 06, 25, 16, 0, 30),
                    Departure_station_id = 5,
                    Departure_station_name = "Viides",
                    Return_station_id = 2,
                    Return_station_name = "Toinen",
                    Covered_distance_m = 4000,
                    Duration_sec = 1230
                },
                new June
                {
                    Departure = new DateTime(2021, 06, 25, 14, 25, 30),
                    Return = new DateTime(2021, 06, 25, 14, 40, 0),
                    Departure_station_id = 8,
                    Departure_station_name = "Kahdeksas",
                    Return_station_id = 9,
                    Return_station_name = "Yhdeksas",
                    Covered_distance_m = 2000,
                    Duration_sec = 870
                },
            };
        }

        public static List<May> GetFakeMayJourneys()
        {
            return new List<May>
            {
                new May
                {
                    Departure = new DateTime(2021, 05, 25, 10, 15, 0),
                    Return = new DateTime(2021, 05, 25, 11, 0, 0),
                    Departure_station_id = 1,
                    Departure_station_name = "Ensimmainen",
                    Return_station_id = 4,
                    Return_station_name = "Neljas",
                    Covered_distance_m = 8000,
                    Duration_sec = 2700
                },
                new May
                {
                    Departure = new DateTime(2021, 05, 25, 12, 43, 0),
                    Return = new DateTime(2021, 05, 25, 12, 55, 0),
                    Departure_station_id = 3,
                    Departure_station_name = "Kolmas",
                    Return_station_id = 5,
                    Return_station_name = "Viides",
                    Covered_distance_m = 3200,
                    Duration_sec = 720
                },
                new May
                {
                    Departure = new DateTime(2021, 05, 2, 08, 32, 0),
                    Return = new DateTime(2021, 05, 2, 08, 35, 0),
                    Departure_station_id = 6,
                    Departure_station_name = "Kuudes",
                    Return_station_id = 9,
                    Return_station_name = "Yhdeksas",
                    Covered_distance_m = 1600,
                    Duration_sec = 180
                },
                new May
                {
                    Departure = new DateTime(2021, 05, 25, 15, 27, 0),
                    Return = new DateTime(2021, 05, 25, 15, 52, 0),
                    Departure_station_id = 8,
                    Departure_station_name = "Kahdeksas",
                    Return_station_id = 2,
                    Return_station_name = "Toinen",
                    Covered_distance_m = 6400,
                    Duration_sec = 1560
                },
                new May
                {
                    Departure = new DateTime(2021, 05, 25, 10, 38, 0),
                    Return = new DateTime(2021, 05, 25, 11, 0, 0),
                    Departure_station_id = 6,
                    Departure_station_name = "Kuudes",
                    Return_station_id = 3,
                    Return_station_name = "Kolmas",
                    Covered_distance_m = 1230,
                    Duration_sec = 1320
                },
            };
        }
        public static List<Journey> GetFakeJourneys()
        {
            return new List<Journey> { };
        }
    }
}
