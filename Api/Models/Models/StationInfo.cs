using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Models
{
    [Keyless]
    public class StationInfo
    {
        
        public string Name { get; set; }
        
        public string Adress { get; set; }
        
        public string x { get; set; } //Having problems with floats in sql server
        
        public string y { get; set; }
        
        public int? CountOfDepartures { get; set; }
        
        public int? CountOfReturns { get; set; }
        
        public int? AverageDistanseDepartures { get; set; }
        
        public int? AverageDistanseReturns { get; set; }
        
        public string? PopularReturns { get; set; }
        
        public string? PopularDepartures { get; set; }
    }
}
