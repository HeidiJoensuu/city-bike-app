using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Models
{
    public abstract class JourneyAbstract
    {
        public int id { get; set; }
        public DateTime departure { get; set; }
        public DateTime returntime { get; set; }
        public int departure_station_id { get; set; }
        public string departure_station_name { get; set; }
        public int return_station_id { get; set; }
        public string return_station_name { get; set; }
        
        public int? covered_distance_m { get; set; }
        
        public int duration_sec { get; set; }
    }
}
