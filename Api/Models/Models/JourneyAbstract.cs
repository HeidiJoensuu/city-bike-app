using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Models
{
    public abstract class JourneyAbstract
    {
        public int Id { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Return { get; set; }
        public int Departure_station_id { get; set; }
        public string Departure_station_name { get; set; }
        public int Return_station_id { get; set; }
        public string Return_station_name { get; set; }
        
        public int? Covered_distance_m { get; set; }
        
        public int Duration_sec { get; set; }
    }
}
