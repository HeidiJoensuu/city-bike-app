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
                return base.Ok(_mapper.Map<IEnumerable<StationDto>>(await _stationService.GetStations(offset, limit, order, search, descending)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StationInfoDTO>> GetStationInfo(int id, int month)
        {
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
        public async Task<ActionResult<StationShortDto>> PostStation(NewStationDto dto)
        {
            try
            {
                var saveStation = _mapper.Map<Station>(dto);
                var station = _mapper.Map<StationShortDto>(await _stationService.CreateStation(saveStation));
                return CreatedAtAction(nameof(GetStationNames), new { id = station.Id }, station);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetStationCount(string? search)
        {
            if (search == null)
            {
                search = "";
            }
            try
            {
                return Ok(await _stationService.GetStationCount(search));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("names")]
        public async Task<ActionResult<StationShortDto>> GetStationNames()
        {
            try
            {
                return Ok(await _stationService.GetStationNames());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
