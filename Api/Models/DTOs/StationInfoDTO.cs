﻿namespace Api.Models.DTOs
{
    public class StationInfoDTO
    {
        public string Nimi { get; set; }
        public string Namn { get; set; }
        public string Name { get; set; }
        public string Osoite { get; set; }
        public string Adress { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        
        public JourneyInfoDto? journeyInfo { get; set; }
    }
}
