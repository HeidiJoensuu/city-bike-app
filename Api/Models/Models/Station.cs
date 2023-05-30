using Api.Models.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Models
{
    [Table("kaupunkipyoraasemat")]
    public class Station
    {
        public int? id { get; set; }
        public string nimi { get; set; }
        public string namn { get; set; }
        public string name { get; set; }
        public string osoite { get; set; }
        public string adress { get; set; }
        public double x { get; set; }

        public double y { get; set; }
        public int? kapasiteet { get; set; }
        public string? kaupunki { get; set; }

        [NotMapped]
        public May? may { get; set; }
        [NotMapped]
        public June? june { get; set; }
        [NotMapped]
        public July? july { get; set; }
    }
}
