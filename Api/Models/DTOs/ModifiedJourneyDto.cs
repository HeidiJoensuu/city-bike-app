﻿using Microsoft.EntityFrameworkCore;

namespace Api.Models.DTOs
{
    [Keyless]
    public class ModifiedJourneyDto
    {
        public DateTime Departure { get; set; }
        public DateTime Returntime { get; set; }
        public int Departure_station_id { get; set; }
        public string Departure_station_name { get; set; }
        public int Return_station_id { get; set; }
        public string Return_station_name { get; set; }
        public float Covered_distance { get; set; }
        public string Duration { get; set; }
    }
}
