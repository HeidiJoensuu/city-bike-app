namespace Api.Models.DTOs
{
    public class JourneyInfoDto
    {
        public int CountOfDepartures { get; set; }
        public int CountOfReturns { get; set; }
        public decimal AverageDistanseDepartures { get; set; }
        public decimal AverageDistanseReturns { get; set; }
        public List<string> PopularReturns { get; set; }
        public List<string> PopularDepartures { get; set; }
    }
}
