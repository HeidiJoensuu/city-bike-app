namespace Api.Models.DTOs
{
    public class StationInfoDTO
    {
        public string Nimi { get; set; }
        public string Namn { get; set; }
        public string Name { get; set; }
        public string Osoite { get; set; }
        public string Adress { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public int CountOfDepartures { get; set; }
        public int CountOfReturns { get; set; }
        public float AverageDistanseDepartures { get; set; }
        public float AverageDistanseReturns { get; set; }
        public List<string> PopularReturns { get; set; }
        public List<string> PopularDepartures { get; set; }
    }
}
