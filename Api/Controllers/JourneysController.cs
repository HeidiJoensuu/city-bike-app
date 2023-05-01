using Api.Models;
using Api.Models.DTOs;
using Api.Models.Models;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneysController : ControllerBase
    {
        private readonly IJourneyService _journeyService;
        private readonly IMapper _mapper;

        public JourneysController(IJourneyService journeyService, IMapper mapper)
        {
            _journeyService = journeyService;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Journey>>> GetJourneys(int offset, int limit, string order, string? search, bool descending, int month)
        {
            if (search == null)
            {
                search = "";
            }
            try
            {
                return Ok(_mapper.Map<IEnumerable<ModifiedJourney>>( await _journeyService.GetJourneys(offset, limit, order, search, descending , month)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Journey>> PostJourney(Journey journey)
        {
            try
            {
                await _journeyService.CreateJourney(journey);
                return CreatedAtAction(nameof(GetJourneys), journey);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
