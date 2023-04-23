using Api.Models.Models;

namespace Api.Services
{
    public interface IStationService
    {
        Task<IEnumerable<Station>> GetStations(int offset, int limit, string order, string search, bool descending);
        Task<Station> GetStationInfo(int id);
    }
}
