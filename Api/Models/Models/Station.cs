using Api.Models.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Models
{
    [Table("kaupunkiPyoraAsemat")]
    public class Station
    {
        public int? Id { get; set; }
        public string Nimi { get; set; }
        public string Namn { get; set; }
        public string Name { get; set; }
        public string Osoite { get; set; }
        public string Adress { get; set; }
        public string? x { get; set; }

        public string? y { get; set; }
        public int? Kapasiteet { get; set; }

        [NotMapped]
        public May? may { get; set; }
        [NotMapped]
        public June? june { get; set; }
        [NotMapped]
        public July? july { get; set; }
    }
}
