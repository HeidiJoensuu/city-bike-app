﻿using Microsoft.EntityFrameworkCore;

namespace Api.Models.Models.DTOs
{
    [Keyless]
    public class ModifiedJourney
    {
        public DateTime Departure { get; set; }
        public DateTime Return { get; set; }
        public int Departure_station_id { get; set; }
        public string Departure_station_name { get; set; }
        public int Return_station_id { get; set; }
        public string Return_station_name { get; set; }
        public float Covered_distance { get; set; }
        public int Duration { get; set; }
    }
}
