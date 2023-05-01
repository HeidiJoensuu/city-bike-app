using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Models
{
    [Table("kaupunkipyoraasemat")]
    public class StationInfo
    {
        public int Id { get; set; }
        public int FID { get; set; }
        public string Nimi { get; set; }
        public string Namn { get; set; }
        public string Name { get; set; }
        public string Osoite { get; set; }
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
