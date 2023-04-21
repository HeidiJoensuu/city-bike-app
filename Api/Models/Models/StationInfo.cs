namespace Api.Models.Models
{
    public class StationInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public int Departures { get; set; }
        public int Returns { get; set; }
        public List<string> PopularReturns { get; set; }
        public List<string> PopularDepartures { get; set; }
    }
}
