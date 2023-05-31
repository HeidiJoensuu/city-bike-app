using Api.Models.DTOs;
using Api.Models.Models;

namespace Api.Services
{
    public interface IStationService
    {
        /// <summary>
        /// Gets all stations depending on the parameters
        /// </summary>
        /// <param name="offset">Offset</param>
        /// <param name="limit">Limit</param>
        /// <param name="order">Order</param>
        /// <param name="search">Search</param>
        /// <param name="descending">Descending</param>
        /// <returns>IEnurable<Station></returns>
        Task<IEnumerable<Station>> GetStations(int offset, int limit, string order, string search, bool descending);

        /// <summary>
        /// Gets information of selected station depending on selected month
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="month">Month</param>
        /// <returns>StationInfoDTO</returns>
        Task<StationInfoDTO> GetStationInfo(int id, int month);

        /// <summary>
        /// Posts new station into database
        /// </summary>
        /// <param name="saveStationDto">New station - saveStationDto</param>
        /// <returns>Station</returns>
        Task<Station> CreateStation(Station saveStationDto);
        
        /// <summary>
        /// Gets count of all stations depending on search
        /// </summary>
        /// <param name="search">Search</param>
        /// <returns>int</returns>
        Task<int> GetStationCount(string search);

        /// <summary>
        /// Gets all station names as a list
        /// </summary>
        /// <returns>IEnumerable<StationShortDto></returns>
        Task<IEnumerable<StationShortDto>> GetStationNames();
    }
}
