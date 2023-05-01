using Api.Models.DTOs;
using Api.Models.Models;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationService _stationService;
        private readonly IMapper _mapper;

        public StationController(IStationService stationService, IMapper mapper)
        {
            _stationService = stationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Station>>> GetStations(int offset, int limit, string order, string? search, bool descending)
        {
            if (search == null)
            {
                search = "";
            }
            try
            {
                return Ok(await _stationService.GetStations(offset, limit, order, search, descending));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StationInfoDTO>> GetStationInfo(int id, int month)
        {
            Console.WriteLine(month);
            if (month == null)
            {
                month = 0;
            }
            try
            {
                return Ok(_mapper.Map<StationInfoDTO>(await _stationService.GetStationInfo(id, month)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Station>> PostStation(NewStationDto dto)
        {
            try
            {
                var station = _mapper.Map<Station>(await _stationService.CreateStation(dto));
                return CreatedAtAction(nameof(GetStations), new { id = station.Id }, station);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
