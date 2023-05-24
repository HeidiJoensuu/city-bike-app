namespace Api.Models.DTOs
{
    public class JourneyInfoDto
    {
        public int CountOfDepartures { get; set; }
        public int CountOfReturns { get; set; }
        public decimal AverageDistanceDepartures { get; set; }
        public decimal AverageDistanceReturns { get; set; }
        public List<string> PopularReturns { get; set; }
        public List<string> PopularDepartures { get; set; }
    }
}
