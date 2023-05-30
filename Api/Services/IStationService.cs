using Api.Models.DTOs;
using Api.Models.Models;

namespace Api.Services
{
    public interface IStationService
    {
        Task<IEnumerable<Station>> GetStations(int offset, int limit, string order, string search, bool descending);
        Task<StationInfoDTO> GetStationInfo(int id, int month);
        Task<Station> CreateStation(Station saveStationDto);
        Task<int> GetStationCount(string search);
        Task<IEnumerable<StationShortDto>> GetStationNames();
    }
}
